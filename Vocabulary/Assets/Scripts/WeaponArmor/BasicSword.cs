using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class BasicSword : GenericWeapon {
	public BasicSword()
	{
		name = "Basic Sword";
		Damage = 1f;
		description = "An ordinary sword.";
	}
}
