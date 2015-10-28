using UnityEngine;
using System.Collections;

public class Slime : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 10;
		Armor = 0;
		Damage = 1;
		name = "Slime";
	}
	
	// Update is called once per frame
	void Update () {
	}
}
