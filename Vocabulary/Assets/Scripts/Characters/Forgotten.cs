using UnityEngine;
using System.Collections;

public class Forgotten : GenericEnemy {

	// Use this for initialization
	void Start () {
		Health = 30f;
		MaxHealth = 30f;
		Armor = 0f;
		Damage = 5f;
		name = "Forgotten";
		//creating item drop list
		//Droppable.Add("Broken Spearhead");
		Droppable.Add("Hollow of the Forgotten");
	}
	
	// Update is called once per frame
	void Update () {
		if(spinner1 != null)
		{
			spinner1.transform.RotateAround(spinner1.gameObject.transform.position, new Vector3(0,0,1), 20* Time.deltaTime);
			spinner2.transform.RotateAround(spinner1.gameObject.transform.position, new Vector3(0,0,1), -20* Time.deltaTime);
		}
		healthtxt.text =  Health + " / " + MaxHealth; 
	}
}
