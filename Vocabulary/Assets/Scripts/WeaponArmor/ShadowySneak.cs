using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class ShadowySneak : GenericArmor {
	public ShadowySneak()
	{
		name = "Shadowy Sneak";
		Armor = 10;
		InsigniaNum = 3;
		misschance = .25f;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
