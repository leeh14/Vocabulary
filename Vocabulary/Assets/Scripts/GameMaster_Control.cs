using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster_Control : MonoBehaviour{
    GameObject CurrentMenu;
	GameObject Enemies;
	private GameObject player;
	public Mesh livemesh;
	public List<string[]> questions;
	public QuestionData CurrentQuestion;
    // Use this for initialization
    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
        //debugging purposes in scene just auto generate the choice menu
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
		//for now just set it to be the 1 answer questions
		questions = GenerateEasyQuestions ();
    }

    // Update is called once per frame
    void Update() {

    }
    //keep the data here
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
	#region Helpers
	//reload to main menu
	public void LoadMenu()
	{
		ClearMenu ();
		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
	}
	public void RedoBattle()
	{
		ClearMenu();
		//reset everything
		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
		Refresh ();
	}
	public void Refresh()
	{
		//resets player's and enemies health back to full etc..
	}
	//call a different set of questions
	void OnLevelWasLoaded(int level)
	{
		//depending on the level load a different set of questions
		if (level == 1) {
			questions = GenerateEasyQuestions ();
		} else if (level == 2) {
			questions = GenerateMediumQuestions ();
		} else if (level == 3) {
			questions = GenerateHardQuestions();
		}
	}
	//remove the current menu on the screen
	void ClearMenu()
	{
		Destroy(CurrentMenu);
	}
	#endregion
	#region Combat
	//future add argument a list of enemiers
    public void BeginBattle()
    {
        ClearMenu();
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
		//Iterate through list of enemies and generate corresponding prefabs

		//generate the enemy canvas
		Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Testingshizuku"));
		//MeshFilter m = Enemies.GetComponent<MeshFilter> ();
		//m.mesh = livemesh;
		//MeshRenderer mr = Enemies.GetComponent<MeshRenderer> ();
		//mr.material = new Material(Shader.Find("Legacy Shaders/Transparent/Diffuse"));
		Enemies.transform.position = new Vector3 (18, 15, 2);

    }
    //the multiple choices for combat
    public void BeginCombat(string[] data)
    {
        ClearMenu();
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/CombatQuestion"));
        //add the questions to the buttons
		CurrentQuestion = new QuestionData(data);
		//Debug.Log (question);
		CurrentMenu.GetComponent<CombatQuestion>().CreateQuestions(CurrentQuestion);
	}
	
	//after determining answer do corresponding action
	public void ChangeTurn(QuestionData obj , string playerchoice)
	{
		bool correct = obj.CheckAnswer (playerchoice);
		ClearMenu ();
		//Debug.Log("changin" + correct + "  " + obj.Answer + "df" + playerchoice);
		//CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Answer"));
		//Text[] ans = CurrentMenu.GetComponentsInChildren<Text>();
		if(correct)
		{
			//attack the enemy
			Enemies.GetComponent<Goblin>().ReceiveDamage(2);
			if(Enemies.GetComponent<Goblin>().Alive == false)
			{
				//go back to main menu after killing enemies
				ClearMenu();
				LoadMenu();

				//adding the definition of the word into learned dictionary
				player.GetComponent<Player>().WordDict.Add(CurrentQuestion.Answer,CurrentQuestion.definition);

				return;
			}
		}
		else
		{

			player.GetComponent<Player>().ReceiveDamage(2);
			if(player.GetComponent<Player>().Alive == false)
			{
				ClearMenu();
				CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleLost"));
				return;
			}
		}

		//reset 
		RedoBattle ();
	}
	#endregion
	#region GenerateQuestions
	//Generating the questions 
	public List<string[]> GenerateEasyQuestions()
	{
		List<string[]> temp = new List<string[]>();

		string[] question = {"Best Onesie?" , "Charmander", "Greninja", "Magikarp", "Suicune"};
		temp.Add (question);
		question = new string[]{"Greatest onsie created?", "Charmander", "Greninja", "Magikarp", "Suicune"};
		temp.Add (question);
		question = new string[]{"Greatest pokemon onsie created?", "Charmander", "Greninja", "Magikarp", "Suicune"};
		temp.Add (question);
		return temp;
	}
	public List<string[]> GenerateMediumQuestions()
	{
		List<string[]> temp = new List<string[]>();
		string[] question = {"Best Pokemon?" , "Charmander", "Greninja", "Magikarp", "Suicune"};
		temp.Add (question);
		return temp;
	}
	public List<string[]> GenerateHardQuestions()
	{
		List<string[]> temp = new List<string[]>();
		string[] question = {"Best Starter?" , "Charmander", "Greninja", "Magikarp", "Marshtomp"};
		temp.Add (question);
		return temp;
	}
	public List<string[]> Generate2AnsEasyQ()
	{
		//placeholder
		List<string[]> temp = new List<string[]>();
		string[] question = {"Best Starter?" , "Charmander", "Greninja", "Magikarp", "Marshtomp"};
		temp.Add (question);
		return temp;
	}
	#endregion
}
