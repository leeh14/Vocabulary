using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster_Control : MonoBehaviour{
    public GameObject CurrentMenu;
	public GameObject TextMenu;
	public GameObject Enemies;
	//public GameObject Enemies2;
	private GameObject CurrentEnemy;
	private List<GameObject> AvailableEnemies = new List<GameObject>();
	private GameObject Background;
	public GameObject player;
	public Mesh livemesh;
	public List<string[]> questions;
	public QuestionData CurrentQuestion;
	public bool TurnContinue = false;
	public Inventory Inventory = new Inventory();

	public bool InBattle =false;
	private GenericWeapon DebugWeapon;
    // Use this for initialization
    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
        
        //debugging purposes in scene just auto generate the choice menu
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
		//for now just set it to be the 1 answer questions
		questions = GenerateEasyQuestions ();
		DebugWeapon = new Axe();
		player.GetComponent<Player>().SetWeapon(DebugWeapon);
		GenericArmor DebugArmor = new WoodArmor();
		player.GetComponent<Player>().SetArmor(DebugArmor);

		Background = GameObject.FindGameObjectWithTag("bg");

		this.transform.GetComponent<DataBase> ().Load ();
    }

	// on game exit
	void OnApplicationQuit(){
		this.transform.GetComponent<DataBase> ().Save ();
	}

    // Update is called once per frame
    void Update() {
//		if(Input.GetKey(KeyCode.A))
//		{
//			Debug.Log("switch to axe");
//			DebugWeapon = new Axe();
//			player.GetComponent<Player>().SetWeapon(DebugWeapon);
//		}else if (Input.GetKey(KeyCode.S))
//		{
//			Debug.Log("switch tp spear");
//			DebugWeapon = new Spear();
//			player.GetComponent<Player>().SetWeapon(DebugWeapon);
//		}
//		else if(Input.GetKey(KeyCode.W))
//		{
//			GenericArmor DebugArmor = new WoodArmor();
//			player.GetComponent<Player>().SetArmor(DebugArmor);
//		}
//		else if(Input.GetKey(KeyCode.C))
//		{
//			GenericArmor DebugArmor = new CrystalArmor();
//			player.GetComponent<Player>().SetArmor(DebugArmor);
//		}
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
		if(CurrentEnemy != null)
		{
			CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
		}
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
	public void ClearMenu()
	{
		Destroy(CurrentMenu);
		Destroy (TextMenu);

	}
	public void ClearEnemies()
	{
		for(int i = 0; i < AvailableEnemies.Count; i ++)
		{
			Destroy(AvailableEnemies[i]);
		}
		AvailableEnemies.Clear();
	}
    public void LoadInventory()
    {
        ClearMenu();
        gameObject.GetComponent<InventoryScreen>().StartInventory();
    }
	public void LoadMap()
	{
		ClearMenu();
		gameObject.GetComponent<MapScreen>().StartMap();
	}
	#endregion
	#region Combat
	//future add argument a list of enemiers
	public void BeginBattle(List<string> Enemy)
    {
		//load the background image
		Background.GetComponent<Background>().LoadCombatBG();
        ClearMenu();
        //CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
		//Iterate through list of enemies and generate corresponding prefabs

		//generate the enemy canvas
//		Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Testingshizuku"));
//
//		Enemies.transform.position = new Vector3 (4.3f, 16.71f, 1f);
//		Enemies.transform.localScale = new Vector3(.38f,1f,.33f);
//		Enemies.name = "Goblin1";
//		Enemies.GetComponent<Goblin>().SetMaster(gameObject);
//
//		//second enemy
//		Enemies2 = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Testingshizuku"));
//
//		Enemies2.transform.position = new Vector3 (-3.5f, 16.71f, 1f);
//		Enemies2.transform.localScale = new Vector3(.38f,1f,.33f);
//		Enemies2.name = "Goblin2";
//		Enemies2.GetComponent<Goblin>().SetMaster(gameObject);
//		AvailableEnemies.Add (Enemies);
//		AvailableEnemies.Add (Enemies2);
		for (int m = 0 ; m < Enemy.Count; m ++)
		{
			Debug.Log(Enemy[m]);
			if (Enemy[m] == "Slime")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Slime"));
				if(Enemy.Count > 1)
				{
					if(m ==  0)
					{
						Enemies.transform.position = new Vector3 (4.3f, 13f, 1f);
						Enemies.transform.localScale = new Vector3(.38f,1f,.33f);
					}
					else 
					{
						Enemies.transform.position = new Vector3 (-3.5f, 13f, 1f);
						Enemies.transform.localScale = new Vector3(.38f,1f,.33f);
					}
				}
			}
			Enemies.name = Enemies.name + m;
			Enemies.GetComponent<Slime>().SetMaster(gameObject);
			AvailableEnemies.Add(Enemies);
		}


    }
	public void CreateBattle(string name)
	{
		Debug.Log("create");
		InBattle = true;
		Background.GetComponent<Background>().LoadQuestionbg1();
		foreach (GameObject ene in AvailableEnemies)
		{
			if(ene.name == name)
			{
				CurrentEnemy = ene;
			}
		}
//		if (name == "Goblin1") {
//			CurrentEnemy = Enemies;
//		} else {
//			CurrentEnemy = Enemies2;
//		}
		ClearMenu();

		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
	}
    //the multiple choices for combat
    public void BeginCombat(string[] data)
    {
		Debug.Log("combat");
		Background.GetComponent<Background>().LoadQuestionbg1();
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


		if (correct) {

			StartCoroutine(WaitTurn(player.GetComponent<Player>().DealDamage()));

			CurrentEnemy.GetComponent<GenericEnemy> ().ReceiveDamage (player.GetComponent<Player>().DealDamage());
		
			CurrentEnemy.GetComponent<LAppModelProxy>().model.GetDamaged();

			if (CurrentEnemy.GetComponent<GenericEnemy> ().Alive == false) {
				//go through list and remove enemy
				//StartCoroutine(DropItem("Apple"));
				Debug.Log("dead");
				//StartCoroutine(WaitItem());
				AvailableEnemies.Remove(CurrentEnemy);

				if(AvailableEnemies.Count ==  0)
				{
					Debug.Log("end");
					//go back to main menu after killing enemies
					ClearMenu ();
					LoadMenu ();
					//need to drop random item
					Inventory.AddItem(2,"apples",1);
					//adding the definition of the word into learned dictionary
					player.GetComponent<Player> ().WordDict.Add (CurrentQuestion.Answer, CurrentQuestion.definition);
					return;
				}
				ClearMenu();

			}


		} else {
			player.GetComponent<Player> ().ReceiveDamage (2);
			if (player.GetComponent<Player> ().Alive == false) {
				//player is dead
				ClearMenu ();
				CurrentMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/BattleLost"));

				return;
			}

		}
		//Let the enemy go and attack
//		if (TurnContinue == true) {
//			StartCoroutine (EnemyTurn ());
//		}


	}
	#region coroutines
	public IEnumerator WaitTurn(int dmg)
	{
		yield return StartCoroutine (PlayerTurn (dmg, CurrentEnemy.GetComponent<GenericEnemy>().name));
	}
	public IEnumerator WaitItem()
	{
		ClearMenu();
		yield return StartCoroutine(DropItem("apple"));
	}
	public IEnumerator PlayerTurn(int damage, string enemyname)
	{
		TurnContinue = true;
		TextMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/CombatText"));
		Text combat = TextMenu.GetComponentInChildren<Text>();
		combat.verticalOverflow = VerticalWrapMode.Overflow;
		combat.text = "You have dealt " + damage + " to " + enemyname;

		yield return new WaitForSeconds(2f);
		//TurnContinue = false;
		TurnContinue = false;
		Destroy (TextMenu);
		StartCoroutine (EnemyTurn ());
	}
	public IEnumerator EnemyTurn()
	{
		if(TurnContinue == false)
		{
			if(CurrentEnemy != null)
			{
				TextMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/CombatText"));
				Text combat = TextMenu.GetComponentInChildren<Text>();
				combat.verticalOverflow = VerticalWrapMode.Overflow;
				combat.text = CurrentEnemy.GetComponent<GenericEnemy>().name +" deals " + CurrentEnemy.GetComponent<GenericEnemy>().Damage +" damage to you.";
				player.GetComponent<Player>().ReceiveDamage(CurrentEnemy.GetComponent<GenericEnemy>().Damage );

			}else
			{
				StartCoroutine(DropItem("apple"));
			}

			yield return new WaitForSeconds(2f);
			InBattle = false;
			StopAllCoroutines ();
			//reset 

			RedoBattle ();
		}
	}
	public IEnumerator DropItem(string itemname)
	{

		TextMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/CombatText"));
		Text combat = TextMenu.GetComponentInChildren<Text>();
		combat.verticalOverflow = VerticalWrapMode.Overflow;
		combat.text = CurrentEnemy.GetComponent<GenericEnemy>().name +" drops " + itemname;
		yield return new WaitForSeconds(2f);
	}
	#endregion
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
		string[] question = {"Best Pokemon?" , "Charmander + Pikachu", "Greninja", "Magikarp", "Suicune"};
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
