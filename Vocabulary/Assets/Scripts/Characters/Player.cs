using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour {
    public int Health;
    public int CurrentArmor;
	public int Damage;
	public bool Alive = true;
	public Dictionary<string, string> WordDict = new Dictionary<string, string>();

    // Use this for initialization
    void Start() {
		Health = 3;
		CurrentArmor = 1;
    }

    // Update is called once per frame
    void Update() {
        
    }
    //Generate the prefab combat question and fill it with corresponding values
    public void MagicAttack()
    {
    }
    //Generate the prefab comabt question and fil it with physical attack questions
    public void PhysicalAttack()
    {

    }
    //creates the battle menu
    public void CreateBattle()
    {
    }

	public void ReceiveDamage(int damage)
	{
		damage -= CurrentArmor;
		Health -= damage;
		Debug.Log ("Player" + Health);
		if (Health <= 0) {
			Alive = false;
		}
	}

}
