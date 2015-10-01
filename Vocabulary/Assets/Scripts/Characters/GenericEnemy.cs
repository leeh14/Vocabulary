using UnityEngine;
using System.Collections;

public class GenericEnemy : MonoBehaviour {
	public GameObject master;
    public int Health;
    public int Armor;
    public int Damage;
	public bool Alive = true;
	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");
		Debug.Log("update");
	}
	public void SetMaster(GameObject mas)
	{
		master = mas;
	}
	public void OnMouseOver()
	{
		if (Input.GetButton ("Fire1")) {
			Debug.Log("works" + gameObject.name);
			master.GetComponent<GameMaster_Control>().CreateBattle(gameObject.name);
		}
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
