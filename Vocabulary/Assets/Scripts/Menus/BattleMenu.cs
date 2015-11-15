﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleMenu : MonoBehaviour {
    private GameObject master;
    private Button[] options;
	private GameObject[] Insignias;
	private GameObject HealthBar;
	private GameObject player;
	private string text;
	public float ScaleHealth;
    // Use this for initialization
    void Start() {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        options = gameObject.GetComponentsInChildren<Button>();
		player = GameObject.FindGameObjectWithTag("Player");
		Insignias = GameObject.FindGameObjectsWithTag("Insignia");
		HealthBar = GameObject.FindGameObjectWithTag("HealthBar");
		ScaleHealth = 1.2f;
		int num = player.GetComponent<Player>().CurrentArmor.InsigniaNum;
		//int num = 2;
		Sprite  temp;
		//change insignia based on armor
		foreach (GameObject img in Insignias)
		{
			temp = Resources.Load<Sprite>("UI Sprites/UI_Insigniaa" + num);
			Debug.Log(temp.name);
			img.GetComponent<Image>().sprite = temp;
			//img.sprite = temp;
			
		}

		//update the buttons onclick events
		UpdateButtons ();
    }

    // Update is called once per frame
    void Update() {

    }

	public void UpdateButtons()
	{
		//updateing the  text

		text = player.GetComponent<Player>().Health + " / " + player.GetComponent<Player>().MaxHealth;
		float percentage = player.GetComponent<Player>().Health / (1.0f  * player.GetComponent<Player>().MaxHealth);
		float healthpercentage = percentage * 1.2f;
		Text[] combat = gameObject.GetComponentsInChildren<Text>();

		Vector3 temp = new Vector3(healthpercentage,.077f,1f);
		HealthBar.transform.localScale = temp;

		foreach(Text c in combat)
		{
			if(c.name == "Health")
			{
				c.text = text;
			}
		}
		foreach (Button choice in options) {
			if(choice.name == "Mattk"){

				//choose random question
				//string[] question =  master.GetComponent<GameMaster_Control>().questions[Random.Range(0,  master.GetComponent<GameMaster_Control>().questions.Count)];
				//depending on enemy = differenet type of question
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginCombat(1));
			}
			else if(choice.name == "PhyAttk")
			{
				//choose random question
				//string[] question =  master.GetComponent<GameMaster_Control>().questions[Random.Range(0,  master.GetComponent<GameMaster_Control>().questions.Count)];
				//depending on enemy = differenet type of question
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginCombat(0));
			}
			else if (choice.name == "Items")
			{
				//open inventory and use an item
				choice.onClick.AddListener(() => master.GetComponent<InventoryScreen>().OpenBattleInventory());
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().Hide());
			}
		}
	}
	void UpdateMagic(Button magicattk)
	{

	}
}
