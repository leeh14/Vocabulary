using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Player : MonoBehaviour {
    public float Health;
	public float MaxHealth;
    public GenericArmor CurrentArmor;
	public float Damage =1f;
	public GenericWeapon CurrentWeapon;
	public bool Alive = true;
	public float healthModifier =0f;
	public Dictionary<string, string> WordDict = new Dictionary<string, string>();
	public bool attkboost = false;
	public bool mgattkboost = false;
    // Use this for initialization
    void Start() {
		Health = 20f;
		MaxHealth = 20f;
		//CurrentArmor = 1;
    }
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

 	public void IncreaseMaxHealth(float max)
	{
		healthModifier = max;
	}
	public void SetWeapon(GenericWeapon weapon)
	{

		CurrentWeapon = weapon;
		//Debug.Log("seweapon" + CurrentWeapon.Damage);
	}
	public void AttackBoost()
	{
		attkboost = true;
	}
	public void MgattkBoost()
	{
		mgattkboost = true;
	}
	public void PotionCD()
	{
		attkboost = false;
		mgattkboost = false;
	}
	void Update()
	{
		if (Health <= 0) {
			Alive = false;
		}
	}
	public void SetArmor(GenericArmor arm)
	{

		CurrentArmor = arm;
		healthModifier = arm.healthmodifier;

	}
	public float DealDamage()
	{
		return Damage + CurrentWeapon.Damage;
	}
	public void ReceiveDamage(float damage)
	{
		damage -= CurrentArmor.Armor;
		//armor is greater than damage
		if(damage > 0 )
		{
			Health -= damage;
		}
		if(Health > MaxHealth)
		{
			Health = MaxHealth;
		}
		if (Health <= 0) {
			Alive = false;
		}
	}

}
