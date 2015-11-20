using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class GenericWeapon : IComparable<GenericWeapon> {
	public string name;
	public float Damage;
	public string Attribute;
	public bool Special = false;
	public bool AffectAnswers = false;
	public float AttackModifier = 1f;
	public float dmgdealt = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public virtual void DealDamage(float dmg, List<GameObject> Enemies, int questiontype)
	{
		foreach(GameObject enemy in Enemies)
		{
			enemy.GetComponent<GenericEnemy>().ReceiveDamage(dmg);
		}
	}
	public virtual void SpecialMove(float dmg, List<GameObject> Enemies, int questiontype)
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
