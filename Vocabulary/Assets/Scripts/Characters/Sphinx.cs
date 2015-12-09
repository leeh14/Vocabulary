using UnityEngine;
using System.Collections;

public class Sphinx : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 2f;
		name = "Sphinx";
		
		Droppable.Add("Curious Claw");
		Droppable.Add("Luscious Mane");
	}

}
