using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GenericArmor : IComparable<GenericArmor>{
	public string name;
	public int Armor;
	public int InsigniaNum;
	public float healthmodifier = 0f;
	public string description = "";
	public bool Miss = false;
	public float misschance;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// compareTo method
	public int CompareTo(GenericArmor other){
		if(other == null){
			return 1;
		}
		if (this.name == other.name && this.Armor == other.Armor) {
			return 0;
		} else {
			return 1;
		}
	}
}
