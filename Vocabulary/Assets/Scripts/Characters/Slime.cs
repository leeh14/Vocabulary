using UnityEngine;
using System.Collections;

public class Slime : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10;
		Armor = 0;
		Damage = 1;
		name = "Slime";
		//creating item drop list
		Droppable.Add("Congealed Plasma");
		Droppable.Add("Resonant Ooze");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
