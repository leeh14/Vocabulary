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
	public GameObject healthGUI;
	public Material highlighting;
	public Material NonHighlighting;
	//for the forggoten
	public GameObject spinner1;
	public GameObject spinner2;
	//health bars of the enemies
	public Text healthtxt;
	public Image healthbar;
	// Use this for initialization
	void Start () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");

		//HealthGui = gameObject.GetComponentInChildren<Canvas>();
		//healthtxt.transform.position = new Vector3(147f, 480f, transform.position.z);


	}
	void OnDestroy()
	{
		Destroy(healthtxt);
		Destroy (healthbar);
		Destroy(healthGUI);
		if(spinner1 != null)
		{
			Destroy(spinner1);
			Destroy(spinner2);
		}
	}
	public void Show()
	{
		healthGUI.SetActive(true);
		if(spinner1 != null)
		{
			spinner1.SetActive(true);
			spinner2.SetActive(true);
		}
	}
	public void hide()
	{
		healthGUI.SetActive(false);
		if(spinner1 != null)
		{
			spinner1.SetActive(false);
			spinner2.SetActive(false);
		}
	}
	// Update is called once per frame
	void Update () {
		master = GameObject.FindGameObjectWithTag ("GameMaster");
//		//Debug.Log("aa");
//		if(gameObject.transform.position.x < -.30f)
//		{
//			//Enemey is on the left 
//		
//			//healthtxt.transform.localPosition = new Vector3(-20f, 320f,  -1);
//			//healthbar.transform.localPosition = new Vector3(-20f, 325f, gameObject.transform.position.z);
//			//healthtxt.transform.position = new Vector3(70f, 270f, -1f);
//			//healthbar.transform.position = new Vector3(40f,270f,-1f);
//		}
//		else 
//		{
//			//enemy is on the right
//			//healthtxt.transform.localPosition = new Vector3(315f, 770f, gameObject.transform.position.z);
//			//healthbar.transform.localPosition = new Vector3(240f, 780f, gameObject.transform.position.z);
//			//healthtxt.transform.position = new Vector3(155f, 270f, -1f);
//			//healthbar.transform.position = new Vector3(125f,270f,-1f);
//		}
//		healthbar.transform.localScale = new Vector3(.45f, .08f,1f);
		healthtxt.text =  Health + " / " + MaxHealth; 

		//healthtxt.transform.localScale = new Vector3(1.5f, 1.5f,1f);

		//display current health
	}
	public void SetMaster(GameObject mas)
	{
		master = mas;
		healthGUI = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/healthObj"));
		Image[] health = healthGUI.GetComponentsInChildren<Image>();
		Text[] healthtext = healthGUI.GetComponentsInChildren<Text>();
		foreach(Text i in healthtext)
		{
			
			if(gameObject.transform.position.x < -.30f)
			{
				//Enemey is on the left 
				i.transform.position = new Vector3(66.5f,261f,-1f);

			}
			else 
			{
				//enemy is on the right
				i.transform.position = new Vector3(152.5f,261f,-1f);
			}
			healthtxt = i;
		}
		foreach(Image i in health)
		{

			if(gameObject.transform.position.x < -.30f)
			{
				//Enemey is on the left 
				
				//healthtxt.transform.localPosition = new Vector3(-20f, 320f,  -1);
				//healthbar.transform.localPosition = new Vector3(-20f, 325f, gameObject.transform.position.z);
				//i.transform.position = new Vector3(70f, 270f, -1f);
				i.transform.position = new Vector3(53.5f,270f,-1f);
			}
			else 
			{
				//enemy is on the right
				//healthtxt.transform.localPosition = new Vector3(315f, 770f, gameObject.transform.position.z);
				//healthbar.transform.localPosition = new Vector3(240f, 780f, gameObject.transform.position.z);
				//healthtxt.transform.position = new Vector3(155f, 270f, -1f);
				i.transform.position = new Vector3(138.5f,270f,-1f);
			}
			healthbar = i;
		}
		//if the forgottone
		if(gameObject.name== "Forgotten(Clone)0")
		{
			spinner1 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Forgottonspinner1"));
			spinner2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/ForgottonSpinner2"));
			spinner1.transform.localScale = new Vector3 (1.4f,1.3f,1);
			spinner2.transform.localScale = new Vector3 (1.4f,1.3f,1);
		}
		//healthGUI.transform.position =
	}
	public void OnMouseOver()
	{
		if (Input.GetButton ("Fire1") && master.GetComponent<GameMaster_Control>().InBattle == false) {
			//Debug.Log("works" + gameObject.name);
//            if (gameObject.name == "Goblin2")
//            {
//                master.GetComponent<GameMaster_Control>().questions = master.GetComponent<GameMaster_Control>().GenerateMediumQuestions();
//            }
			MeshRenderer currentren = gameObject.GetComponent<MeshRenderer>();
			//change on selection
			currentren.material= highlighting;
			master.GetComponent<GameMaster_Control>().CreateBattle(gameObject.name);
		}
	}
	public void RevertMaterial()
	{
		MeshRenderer currentren = gameObject.GetComponent<MeshRenderer>();
		//change on selection
		currentren.material= NonHighlighting;
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
