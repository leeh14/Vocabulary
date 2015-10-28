using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GenericWeapon : MonoBehaviour {
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
}
