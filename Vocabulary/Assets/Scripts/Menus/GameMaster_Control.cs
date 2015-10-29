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
	//public Inventory Inventory = new Inventory();
	public bool HideAnswer = false;
	public bool InBattle =false;
	private GenericWeapon DebugWeapon;
	private string CurrentEnemyName = "";
	private bool RoundOver = false;
	private bool loadback = false;
    // Use this for initialization
    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
        
        //debugging purposes in scene just auto generate the choice menu
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
		//for now just set it to be the 1 answer questions
		questions = GenerateEasyQuestions ();
		DebugWeapon = new PatchworkScimitar();
		player.GetComponent<Player>().SetWeapon(DebugWeapon);
		Debug.Log("se");
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
		if(Input.GetKey(KeyCode.A))
		{
			Debug.Log("switch to schimat");
			DebugWeapon = new PatchworkScimitar();
			player.GetComponent<Player>().SetWeapon(DebugWeapon);
		}else if (Input.GetKey(KeyCode.S))
		{
			Debug.Log("switch tp staff");
			DebugWeapon = new StaffofVisions();
			player.GetComponent<Player>().SetWeapon(DebugWeapon);
		}
		if(loadback == true)
		{
			LoadMap();
			loadback = false;
		}
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
		Debug.Log("loading");
		Background.GetComponent<Background>().LoadStart();
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
	public void Hide()
	{
		CurrentMenu.gameObject.SetActive(false);
		foreach(GameObject g in AvailableEnemies)
		{
			g.SetActive(false);
		}
	}
	public void Show()
	{
		CurrentMenu.gameObject.SetActive(true);
		foreach(GameObject g in AvailableEnemies)
		{
			g.SetActive(true);
		}
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
		InBattle = false;
		Destroy(CurrentMenu);
		Destroy (TextMenu);

	}
	//Clear the enemies
	public void ClearEnemies()
	{
		for(int i = 0; i < AvailableEnemies.Count; i ++)
		{
			Destroy(AvailableEnemies[i]);
		}
		AvailableEnemies.Clear();
	}
	//go to the inventory
    public void LoadInventory()
    {
        ClearMenu();
        gameObject.GetComponent<InventoryScreen>().StartInventory();
    }
	//goes to map menu
	public void LoadMap()
	{
		ClearMenu();
		gameObject.GetComponent<MapScreen>().StartMap();
	}

	#endregion
	#region Combat
	//loads the enemies onto the screen
	public void BeginBattle(List<string> Enemy)
    {
		Debug.Log("yeah");
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
						Enemies.transform.position = new Vector3 (4.5f, 13f, 1f);
						Enemies.transform.localScale = new Vector3(.38f,1f,.33f);
					}
					else 
					{
						Enemies.transform.position = new Vector3 (-1.5f, 13f, 1f);
						Enemies.transform.localScale = new Vector3(.38f,1f,.33f);
					}
				}
			}
			Enemies.name = Enemies.name + m;
			Enemies.GetComponent<Slime>().SetMaster(gameObject);
			AvailableEnemies.Add(Enemies);
		}


    }
	//loading the battle menu
	public void CreateBattle(string name)
	{
		InBattle = true;
		Background.GetComponent<Background>().LoadQuestionbg1();
		foreach (GameObject ene in AvailableEnemies)
		{
			if(ene.name == name)
			{
				CurrentEnemy = ene;
			}
		}
		ClearMenu();

		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
	}
    //the multiple choices for combat
    public void BeginCombat( int type)
    {
		Background.GetComponent<Background>().LoadQuestionbg1();
        ClearMenu();
		//disable vision of enemies 
		foreach (GameObject en in AvailableEnemies)
		{
			en.SetActive(false);
		}
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/CombatQuestion"));
        
		//Debug.Log (question);
		if(HideAnswer == true)
		{
			CurrentMenu.GetComponent<CombatQuestion>().RemoveAnswer();
			HideAnswer = false;
		}
		Question data = gameObject.GetComponent<QuestionBank>().GetQuestion(type, 0);
//		//add the questions to the buttons
//		CurrentQuestion = new QuestionData(data);

		//change this to generateing questions
		CurrentMenu.GetComponent<CombatQuestion>().CreateQuestions(data);
	}
	
	//after determining answer do corresponding action
	public void ChangeTurn(Question obj , string playerchoice)
	{
		bool correct = obj.CheckAnswer (playerchoice);
		ClearMenu ();
		//reenable vision of enemies 
		foreach (GameObject en in AvailableEnemies)
		{
			en.SetActive(true);
		}

		if (correct) {

			StartCoroutine(WaitTurn(player.GetComponent<Player>().DealDamage()));
			//determine type of weapon and then deal dmg accordinly

//			CurrentEnemy.GetComponent<GenericEnemy> ().ReceiveDamage (player.GetComponent<Player>().DealDamage());
//		
//			CurrentEnemy.GetComponent<LAppModelProxy>().model.GetDamaged();
			if(player.GetComponent<Player>().CurrentWeapon.Special  == true && player.GetComponent<Player>().CurrentWeapon.AffectAnswers == true )
			{
				HideAnswer = true;
			}
			if (CurrentEnemy.GetComponent<GenericEnemy> ().Alive == false) {
				//go through list and remove enemy
				//StartCoroutine(DropItem("Apple"));
				//StartCoroutine(WaitItem());

				RoundOver = true;

				AvailableEnemies.Remove(CurrentEnemy);
				//iterate thorught the rest of enemies in case of cleave damage
				for(int i = 0; i < AvailableEnemies.Count; i ++)
				{
					if(AvailableEnemies[i].GetComponent<GenericEnemy>().Alive == false)
					{
						AvailableEnemies.RemoveAt(i);
					}
				}
				if(AvailableEnemies.Count ==  0)
				{
					RoundOver = true;
					Debug.Log("end");
					//ClearMenu ();
					//LoadMenu ();
					return;
				}
				//ClearMenu();

			}


		} else {
			player.GetComponent<Player> ().ReceiveDamage (CurrentEnemy.GetComponent<GenericEnemy>().Damage);
			//determine if player is alive
			if (player.GetComponent<Player> ().Alive == false) {
				//player is dead
				ClearMenu ();
				CurrentMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/BattleLost"));
				return;
			}
			//show enemy dealing damage
			TurnContinue = false;
			StartCoroutine(EnemyTurn());

		}


	}
	#region coroutines
	public IEnumerator WaitTurn(int dmg)
	{
		yield return StartCoroutine (PlayerTurn (dmg, CurrentEnemy.GetComponent<GenericEnemy>().name));
	}
	public IEnumerator PlayerTurn(int damage, string enemyname)
	{
		CurrentEnemyName = enemyname;
		TurnContinue = true;
		bool dealt = false;
		//deal damage to enemies
		string combattext = "";
		if(player.GetComponent<Player>().CurrentWeapon.Special == true && player.GetComponent<Player>().CurrentWeapon.AffectAnswers == false)
		{
			player.GetComponent<Player>().CurrentWeapon.SpecialMove(damage, AvailableEnemies);
			dealt = true;
		}
		else
		{
			List<GameObject> temp = new List<GameObject>();
			temp.Add(CurrentEnemy);
			player.GetComponent<Player>().CurrentWeapon.DealDamage(damage, temp);
		}
		if(dealt == true)
		{
			foreach(GameObject e in AvailableEnemies)
			{
				combattext += "You have dealt " + damage + " to " + e.GetComponent<GenericEnemy>().name + "\n";
				e.GetComponent<LAppModelProxy>().model.GetDamaged();
			}
		}
		else
		{
			combattext = "You have dealt " + damage + " to " + enemyname;
			CurrentEnemy.GetComponent<LAppModelProxy>().model.GetDamaged();
		}

		//combat text
		TextMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/CombatText"));
		Text combat = TextMenu.GetComponentInChildren<Text>();
		combat.verticalOverflow = VerticalWrapMode.Overflow;
		combat.text = combattext;

		yield return new WaitForSeconds(3f);
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
				//attack animation
				CurrentEnemy.GetComponent<LAppModelProxy>().model.AttackAnimation();

				//combat text
				TextMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/CombatText"));
				Text combat = TextMenu.GetComponentInChildren<Text>();
				combat.verticalOverflow = VerticalWrapMode.Overflow;
				combat.text = CurrentEnemy.GetComponent<GenericEnemy>().name +" deals " + CurrentEnemy.GetComponent<GenericEnemy>().Damage +" damage to you.";
				player.GetComponent<Player>().ReceiveDamage(CurrentEnemy.GetComponent<GenericEnemy>().Damage );

			}
			else
			{
				StartCoroutine(DropItem("apple"));

			}

			yield return new WaitForSeconds(2f);
			InBattle = false;
			//disable vision of enemies 
			foreach (GameObject en in AvailableEnemies)
			{
				if(en != null)
				{
					en.SetActive(true);
			
				}
			}
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
		combat.text = CurrentEnemyName +" drops " + itemname;
		yield return new WaitForSeconds(2f);
		//Debug.Log("round" + RoundOver);
		if(RoundOver == true)
		{

			//go back to main menu after killing enemies
			//Debug.Log("inside");
			//ClearMenu ();
			//LoadMenu ();
			//CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
			//need to drop random item
			Inventory.AddItem(2,"apples", 1);
			//Inventory.AddItem(2,"apples",1);
			//adding the definition of the word into learned dictionary
			player.GetComponent<Player> ().WordDict.Add (CurrentQuestion.Answer, CurrentQuestion.definition);
			RoundOver = false;
			loadback = true;
		}
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
