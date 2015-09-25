using UnityEngine;
using System.Collections;

public class GenericEnemy : MonoBehaviour {
    public int Health;
    public int Armor;
    public int Damage;
	public bool Alive = true;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ReceiveDamage(int damage)
	{
		damage -= Armor;
		Health -= damage;
		Debug.Log ("Enemy "+Health);
		if(Health <= 0)
		{
			Alive = false;
			//destroy/stop rendering enemy
			Destroy(gameObject);
		}
	}
}
