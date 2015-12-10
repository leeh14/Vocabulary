using UnityEngine;
using System.Collections;

public class Lich : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 20f;
		MaxHealth = 20f;
		Armor = 0f;
		Damage = 4f;
		name = "Lich";
		
		Droppable.Add("Bloodied Broach");
		Droppable.Add("Unhallowed Skull");
	}

}
