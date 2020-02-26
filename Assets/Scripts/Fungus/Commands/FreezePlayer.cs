﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("*DearBrother", "Freeze Player", "Freeze or unfreeze the player")]
[AddComponentMenu("")]
public class FreezePlayer : Command
{
	public bool freezePlayer = true;

	public override void OnEnter()
	{
		base.OnEnter();

		PlayerController.get.Freeze(freezePlayer);

		Continue();
	}
}