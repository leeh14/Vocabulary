using UnityEngine;
using System.Collections;

public class Goblin : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 5f;
		MaxHealth = 5f;
		Armor = 0f;
		Damage = 2f;
		name = "Goblin";

		Droppable.Add("Broken Spearhead");
	}

}
