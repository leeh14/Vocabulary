using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GlowingScales : GenericArmor {
	public GlowingScales()
	{
		name = "Glowing Scales Armor";
		Armor = 10;
		InsigniaNum = 2;
		healthmodifier = 10f;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}