﻿using UnityEngine;
using System.Collections;

public class Ghost : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 4f;
		name = "Ghost";
		//creating item drop list
		Droppable.Add("Shorn Pages");
		Droppable.Add("Dark Wisps");
		Droppable.Add("Sphinx Drop");
		Droppable.Add("Forgotten Drop");
	}
	

}
