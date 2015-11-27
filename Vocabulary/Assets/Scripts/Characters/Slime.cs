using UnityEngine;
using System.Collections;

public class Slime : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10f;
		MaxHealth = 10f;
		Armor = 0f;
		Damage = 2f;
		name = "Slime";
		//creating item drop list
		Droppable.Add("Broken Spearhead");
		Droppable.Add("Congealed Plasma");
		Droppable.Add("Resonant Ooze");
		Droppable.Add("Shorn Pages");
		Droppable.Add("Dark Wisps");
		Droppable.Add("Sphinx Drop");
		Droppable.Add("Forgotten Drop");
	}
	

}
