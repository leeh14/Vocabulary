using UnityEngine;
using System.Collections;

public class Spear : GenericWeapon {
	public Spear()
	{
		Start();
	}
	// Use this for initialization
	void Start () {
		name = "Spear";
		Damage = 2;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
