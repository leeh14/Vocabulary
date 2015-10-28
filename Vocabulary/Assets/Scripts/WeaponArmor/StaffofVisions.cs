using UnityEngine;
using System.Collections;

public class StaffofVisions : GenericWeapon {
	public StaffofVisions()
	{
		Debug.Log("create damadge");
		name = "StaffofVisions";
		Damage = 3;
		AffectAnswers = true;
		Special = true;
	}
	public override void SpecialMove (int dmg, System.Collections.Generic.List<GameObject> Enemies)
	{

	}
}
