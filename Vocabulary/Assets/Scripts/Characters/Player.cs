using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour {
    public int Health;
	public int MaxHealth;
    public GenericArmor CurrentArmor;
	public int Damage =1;
	public GenericWeapon CurrentWeapon;
	public bool Alive = true;
	public Dictionary<string, string> WordDict = new Dictionary<string, string>();

    // Use this for initialization
    void Start() {
		Health = 3;
		MaxHealth = 20;
		//CurrentArmor = 1;
    }
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
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
	public void SetWeapon(GenericWeapon weapon)
	{

		CurrentWeapon = weapon;
		//Debug.Log("seweapon" + CurrentWeapon.Damage);
	}
	public void SetArmor(GenericArmor arm)
	{
		CurrentArmor = arm;
	}
	public int DealDamage()
	{
		return Damage + CurrentWeapon.Damage;
	}
	public void ReceiveDamage(int damage)
	{
		damage -= CurrentArmor.Armor;
		//armor is greater than damage
		if(damage > 0 )
		{
			Health -= damage;
		}

		if (Health <= 0) {
			Alive = false;
		}
	}

}
