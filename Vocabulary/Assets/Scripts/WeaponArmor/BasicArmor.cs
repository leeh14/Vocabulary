using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class BasicArmor : GenericArmor {
	public BasicArmor()
	{
		name = "Basic Armor";
		Armor = 1;
		InsigniaNum = 1;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
