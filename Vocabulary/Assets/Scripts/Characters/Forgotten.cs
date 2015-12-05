using UnityEngine;
using System.Collections;

public class Forgotten : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 30f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 5f;
		name = "Forgotten";
		//creating item drop list
		//Droppable.Add("Broken Spearhead");
		Droppable.Add("Hollow of the Forgotten");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
