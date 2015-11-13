using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class Axe : GenericWeapon {

	public Axe()
	{
		Start();
	}
	// Use this for initialization
	void Start () {
		//Debug.Log("create damadge");
		name = "Axe";
		Damage = 3;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
