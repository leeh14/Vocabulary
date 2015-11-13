using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
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
