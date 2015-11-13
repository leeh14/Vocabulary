using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class CrystalArmor : GenericArmor {
	public CrystalArmor()
	{
		name = "Crystal Armor";
		Armor = 10;
		InsigniaNum = 1;
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
