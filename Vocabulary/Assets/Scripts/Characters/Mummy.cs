using UnityEngine;
using System.Collections;

public class Mummy : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 8f;
		MaxHealth = 8f;
		Armor = 0f;
		Damage = 2f;
		name = "Mummy";
		
		Droppable.Add("Tightened Wrap");
		Droppable.Add("Mummy's Eye");
	}
	

}
