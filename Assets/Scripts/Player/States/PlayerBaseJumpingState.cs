﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseJumpingState : PlayerState
{
	public float initialSpeed = 20f;
	public float gravity = 50f;
    [Tooltip("When releasing the jump button, the player's upwards speed is set to this (if it's not alredy lower)")]
	public float stopSpeed = 2;
	public float horizontalAcceleration = 20;
	public float verticalDeceleration = 10;
    [Tooltip("If the player just taps the jump button, they will still not start falling/stop before this time has passed.\nYou can test this by ticking the Is Current State box.")]
    public float minimumDuration = 0.2f;

    [HideInInspector] public float minimumDurationOverride;

    private float jumpTimer = 0;

	public override void Enter()
	{
		base.Enter();

        jumpTimer = 0;
		player.velocity.y = initialSpeed;
		player.ResetJumpInputBuffer();
        minimumDurationOverride = minimumDuration;
	}

	public override void Exit()
	{
		base.Exit();
	}

	public override void FixedUpdate()
	{
		base.FixedUpdate();

		player.MoveHorizontally(player.walkingState.speed, horizontalAcceleration, verticalDeceleration);

		player.velocity.y = Mathf.MoveTowards(player.velocity.y, 0, gravity * Time.deltaTime);

        jumpTimer += Time.deltaTime;

		if (!player.isJumpInputHeld && player.velocity.y > stopSpeed && jumpTimer > minimumDurationOverride)
		{
			player.velocity.y = stopSpeed;
		}

		if (player.velocity.y > 0 && player.CheckBoxcast(Vector2.up))
		{
			player.velocity.y = 0;
		}
	}

	public override void Start()
	{
		base.Start();
	}

	public override void Update()
	{
		base.Update();


		if (player.velocity.y <= 0)
		{
			player.TransitionState(player.fallingState);
		}
        else if (player.isGrappleButtonHeld && player.grappleDetection.grapplePoint != null)
        {
            player.TransitionState(player.grappleState);
        }
    }

	public override void OnValidate()
	{
		base.OnValidate();
	}
}
