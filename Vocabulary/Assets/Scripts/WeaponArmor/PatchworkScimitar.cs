using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class PatchworkScimitar : GenericWeapon {
	public PatchworkScimitar()
	{
		name = "Patchwork Scimitar";
		Damage = 2f;
		AttackModifier = 1.25f;
		Special = true;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void DealDamage(float dmg, List<GameObject> Enemies, int questiontype)
	{
		//physical attack
		if(questiontype == 0)
		{
			dmg *= AttackModifier;
			dmgdealt = dmg;


		}else 
		{
			dmgdealt = dmg;
		}
		foreach(GameObject enemy in Enemies)
		{
			enemy.GetComponent<GenericEnemy>().ReceiveDamage(dmg);
		}

	}
	public override void SpecialMove(float dmg, List<GameObject> Enemies, int questiontype)
	{
		//physical attack
		if(questiontype == 0)
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
