using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GenericEnemy : MonoBehaviour {
	public GameObject master;
    public float Health;
	public float MaxHealth;
    public float Armor;
    public float Damage;
	public string name;
	public bool Alive = true;
	public bool Active = false;
	public List<string> Droppable = new List<string>();

	//health bars of the enemies
	public Text healthtxt;
	public Image healthbar;
	public Canvas HealthGui;
	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");

		//HealthGui = gameObject.GetComponentInChildren<Canvas>();
		//healthtxt.transform.position = new Vector3(147f, 480f, transform.position.z);


	}
	
	// Update is called once per frame
	void Update () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");
		//Debug.Log("aa");
		if(gameObject.transform.position.x < -.30f)
		{
			//Enemey is on the left 
		
			//healthtxt.transform.localPosition = new Vector3(90f, 770f,  -1);
			//healthbar.transform.localPosition = new Vector3(15f, 780f, gameObject.transform.position.z);
			healthtxt.transform.position = new Vector3(70f, 265f, -1f);
			healthbar.transform.position = new Vector3(40f,265f,-1f);
		}
		else 
		{
			//enemy is on the right
			//healthtxt.transform.localPosition = new Vector3(315f, 770f, gameObject.transform.position.z);
			//healthbar.transform.localPosition = new Vector3(240f, 780f, gameObject.transform.position.z);
			healthtxt.transform.position = new Vector3(155f, 265f, -1f);
			healthbar.transform.position = new Vector3(125f,265f,-1f);
		}
		healthbar.transform.localScale = new Vector3(1f, .5f,1f);
		healthtxt.text =  Health + " / " + MaxHealth; 
		healthtxt.transform.localScale = new Vector3(1.5f, 1.5f,1f);
		if(HealthGui == null)
		{
			//HealthGui = gameObject.GetComponentInChildren<Canvas>();
			//healthtxt = gameObject.GetComponentInChildren<Text>();
		}
		//display current health
	}
	public void SetMaster(GameObject mas)
	{
		master = mas;
	}
	public void OnMouseOver()
	{
		if (Input.GetButton ("Fire1") && master.GetComponent<GameMaster_Control>().InBattle == false) {
			//Debug.Log("works" + gameObject.name);
//            if (gameObject.name == "Goblin2")
//            {
//                master.GetComponent<GameMaster_Control>().questions = master.GetComponent<GameMaster_Control>().GenerateMediumQuestions();
//            }
			master.GetComponent<GameMaster_Control>().CreateBattle(gameObject.name);
		}
	}
	public void ReceiveDamage(float damage)
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
