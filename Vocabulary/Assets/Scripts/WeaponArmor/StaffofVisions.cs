using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class StaffofVisions : GenericWeapon {
	public StaffofVisions()
	{
		Debug.Log("create damadge");
		name = "Staff of Visions";
		Damage = 3f;
		AffectAnswers = true;
		Special = true;
	}
	public override void SpecialMove(float dmg, List<GameObject> Enemies, int questiontype)
	{
		//physical attack
		if(questiontype == 1)
		{
			dmg *= AttackModifier;
			dmgdealt = dmg;
		}
		else
		{
			dmgdealt = dmg;
		}
		foreach(GameObject enemy in Enemies)
		{
			enemy.GetComponent<GenericEnemy>().ReceiveDamage(dmg);
		}
		
	}
}
