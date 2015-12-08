using UnityEngine;
using System.Collections;

public class Tome : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 5f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 2f;
		name = "Tome";

		//dop items
		Droppable.Add("Weathered Hide");
		Droppable.Add("Shorn Pages");
	}
	

}
