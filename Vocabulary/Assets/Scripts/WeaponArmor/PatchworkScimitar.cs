using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PatchworkScimitar : GenericWeapon {
	public PatchworkScimitar()
	{
		Debug.Log("create damadge");
		name = "Patchwork Scimitar";
		Damage = 4;
		Special = true;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public override void SpecialMove(int dmg, List<GameObject> Enemies)
	{
		foreach(GameObject enemy in Enemies)
		{
			enemy.GetComponent<GenericEnemy>().ReceiveDamage(dmg);
		}
	}
}
