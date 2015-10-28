using UnityEngine;
using System.Collections;

public class GenericEnemy : MonoBehaviour {
	public GameObject master;
    public int Health;
    public int Armor;
    public int Damage;
	public string name;
	public bool Alive = true;
	public bool Active = false;
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
		if (Input.GetButton ("Fire1") && master.GetComponent<GameMaster_Control>().InBattle == false) {
			Debug.Log("works" + gameObject.name);
            if (gameObject.name == "Goblin2")
            {
                master.GetComponent<GameMaster_Control>().questions = master.GetComponent<GameMaster_Control>().GenerateMediumQuestions();
            }
			master.GetComponent<GameMaster_Control>().CreateBattle(gameObject.name);
		}
	}
	public void ReceiveDamage(int damage)
	{
		damage -= Armor;
		Health -= damage;
		//Debug.Log ("Enemy "+Health);
		if(Health <= 0)
		{
			Alive = false;
			//destroy/stop rendering enemy
			Destroy(gameObject);
		}
	}
}
