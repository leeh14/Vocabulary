using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GenericWeapon : IComparable<GenericWeapon> {
	public string name;
	public int Damage;
	public string Attribute;
	public bool Special = false;
	public bool AffectAnswers = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void DealDamage(int dmg, List<GameObject> Enemies)
	{
		foreach(GameObject enemy in Enemies)
		{
			enemy.GetComponent<GenericEnemy>().ReceiveDamage(dmg);
		}
	}
	public virtual void SpecialMove(int dmg, List<GameObject> Enemies)
	{
	
	}

	// compareTo method
	public int CompareTo(GenericWeapon other){
		if(other == null){
			return 1;
		}
		if (this.name == other.name) {
			return 0;
		} else {
			return 1;
		}
	}
}
