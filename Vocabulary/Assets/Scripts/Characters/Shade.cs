using UnityEngine;
using System.Collections;

public class Shade : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 3f;
		name = "Shade";
		//creating item drop list
		//Droppable.Add("Broken Spearhead");
		Droppable.Add("Utter Ink");
		Droppable.Add("Dark Wisps");
	}

}
