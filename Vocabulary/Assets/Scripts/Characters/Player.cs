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

    // Use this for initialization
    void Start() {
		Health = 3f;
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

		if (Health <= 0) {
			Alive = false;
		}
	}

}
