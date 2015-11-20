using UnityEngine;
using System.Collections;

public class Goblin : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10f;
		Armor = 0f;
		Damage = 1f;
		name = "Goblin";

		Droppable.Add("Broken Spearhead");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
