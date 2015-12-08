using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster_Control : MonoBehaviour{
    public GameObject CurrentMenu;
	public AudioClip inventorymusic;
	public AudioClip spiremusic;
	public AudioClip Dungeon;
	public AudioSource Music;
	public GameObject TextMenu;
	public GameObject Enemies;
	//public GameObject Enemies2;
	private GameObject CurrentEnemy;
	private List<GameObject> AvailableEnemies = new List<GameObject>();
	private GameObject Background;
	public GameObject player;
	public Mesh livemesh;
	//public QuestionData CurrentQuestion;
	public bool TurnContinue = false;
	//public Inventory Inventory = new Inventory();
	public bool HideAnswer = false;
	public bool InBattle =false;
	private GenericWeapon DebugWeapon;
	private GenericArmor DebugArmor;
	private string CurrentEnemyName = "";
	private bool RoundOver = false;
	private bool loadback = false;
	private List<string> MultipleAnswers = new List<string>();
	private string droppeditemname;
	private int questiontype;
	private string CurrentLevel;
    // Use this for initialization
    void Start() {
		player = GameObject.FindGameObjectWithTag("Player");

        //debugging purposes in scene just auto generate the choice menu
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Title"));

		//for now just set it to be the 1 answer questions
		DebugWeapon = new BasicSword();
		player.GetComponent<Player>().SetWeapon(DebugWeapon);
		Inventory.ChangeWeapon ("Basic Weapon");
		DebugArmor = new BasicArmor();
		player.GetComponent<Player>().SetArmor(DebugArmor);
		Inventory.ChangeArmor ("Basic Armor");

		Background = GameObject.FindGameObjectWithTag("bg");
    }

	// on game exit
	void OnApplicationQuit(){
		this.transform.GetComponent<DataBase> ().Save ();
	}

    // Update is called once per frame
    void Update() {
		if(loadback == true)
		{
			ClearMenu();
			//Load game over game and can reset
			//CurrentMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/Winning"));
			LoadMap();
			loadback = false;
		}
    }
    //keep the data here
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
		this.transform.GetComponent<DataBase> ().Load ();
    }
	#region Helpers
	//reload to main menu
	public void LoadMenu()
	{
		ClearMenu ();
		if (AvailableEnemies.Count > 0)
		{
			//can't make it all the way back to start screen
			gameObject.GetComponent<MapScreen>().DestroyCanvas();
			//clear the enemies
			foreach (GameObject g in AvailableEnemies)
			{
				Destroy(g);
			}
			AvailableEnemies.Clear();
		}
		Background.GetComponent<Background>().LoadStart();
//		Music.clip = inventorymusic;
//		Music.Play();
		Background.GetComponent<Background>().overlay.SetActive(false);
		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
	}
	public void RedoBattle()
	{
		ClearMenu();
		Background.GetComponent<Background>().overlay.SetActive(true);
		//reset everything
		if(CurrentEnemy != null)
		{
			CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
			InBattle = false;
		}
		//Refresh ();
		//InBattle = false;
	}
	public void Hide()
	{
		CurrentMenu.gameObject.SetActive(false);
		foreach(GameObject g in AvailableEnemies)
		{
			g.GetComponent<GenericEnemy>().hide();
			g.SetActive(false);

		}
	}
	public void Show()
	{
		CurrentMenu.gameObject.SetActive(true);
		foreach(GameObject g in AvailableEnemies)
		{

			g.SetActive(true);
			g.GetComponent<GenericEnemy>().Show();
		}
		if (CurrentMenu.GetComponent<BattleMenu> () != null) {
			CurrentMenu.GetComponent<BattleMenu> ().UpdateButtons ();
		}
	}
	//remove the current menu on the screen
	public void ClearMenu()
	{
		//InBattle = false;
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
	public void BackToLvlMenu()
	{
		Background.GetComponent<Background>().LoadStart();
		Music.clip = inventorymusic;
		Music.Play();
		Background.GetComponent<Background>().overlay.SetActive(false);
		InBattle = false;

	}
	//goes to map menu
	public void LoadMap()
	{
		Background.GetComponent<Background>().overlay.SetActive(false);
		ClearMenu();
		gameObject.GetComponent<MapScreen>().StartMap();
	}
	public void LoadLvlBackground()
	{
		Background.GetComponent<Background>().overlay.SetActive(true);
		if(CurrentLevel == "Level_1")
		{
			//dungeon
			if(Music.clip != Dungeon)
			{
				Music.clip = Dungeon;
				Music.Play();
			}
			Background.GetComponent<Background>().LoadCombatBG();
		}
		else if(CurrentLevel == "Level_2")
		{
			//library
			if(Music.clip != Dungeon)
			{
				Music.clip = Dungeon;
				Music.Play();
			}
			Background.GetComponent<Background>().LoadLibrary();
		}
		else if(CurrentLevel == "Level_3")
		{
			//spire
			if(Music.clip != spiremusic)
			{
				Music.clip = spiremusic;
				Music.Play();
			}
			Background.GetComponent<Background>().LoadTower();
		}
	}

	public void LoadDictionary(){
		ClearMenu ();
		gameObject.GetComponent<Dictionary> ().OpenDictionary ();
	}
	#endregion

	#region Combat
	//loads the enemies onto the screen
	public void BeginBattle(List<string> Enemy, string level)
    {
		CurrentLevel = level;
		//load the background image
		LoadLvlBackground();
		//Background.GetComponent<Background>().LoadCombatBG();
        ClearMenu();

		for (int m = 0 ; m < Enemy.Count; m ++)
		{
			Debug.Log(Enemy[m]);
			bool shiftleft = false;
			bool shiftup = false;
			//positions
			float x = 6f;
			float y = 4f;
			float xl = -.31f;
			if (Enemy[m] == "Slime")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Slime"));
			}
			else if(Enemy[m] == "SlimeB")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/SlimeB"));
				shiftup = true;
			}
			else if(Enemy[m] == "SlimeR")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/SlimeR"));
				shiftleft = true;
			}
			else if(Enemy[m] == "SlimeP")
			{
				shiftup = true;
				shiftleft = true;
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/SlimeP"));
			}
			else if (Enemy[m] == "Ghost")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Ghost"));
				y +=4;
			}
			else if (Enemy[m] == "Shade")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Shade"));
				xl -=3;
				x -=3;
				y +=4;
			}
			else if (Enemy[m] == "Forgotten")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Forgotten"));
				y+=6;
			}
			else if (Enemy[m] == "Tome")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Tome"));
				xl -=3;
				x -=3;
				y +=1.5f;
			}
			else if (Enemy[m] == "Goblin")
			{
				Enemies = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Enemies/Goblin"));
				x-=2;
				xl-=2;
				y+=3;
			}

			if(Enemy.Count > 1)
			{
				if(shiftleft == true)
				{
					x -= 4f;
					xl -= 5f;
				}
				if(shiftup == true)
				{
					y +=2.45f;
				}
				if(Enemy[m] == "Ghost")
				{
					Enemies.transform.localScale = new Vector3(.36f,1f,.53f);
					x -=2f;
					xl -=2f;
				}
				else if(Enemy[m] == "Goblin")
				{
					Enemies.transform.localScale = new Vector3(.42f,1f,.49f);
				}
				else
				{
					Enemies.transform.localScale = new Vector3(.7f,1f,.57f);
				}
				if(m ==  0)
				{
					//Enemies.GetComponent<GenericEnemy>().healthbar.transform.position = new Vector3(2 ,12 ,1f);
					Enemies.transform.position = new Vector3 ( x,y, 1f);
				}
				else 
				{
					Enemies.transform.position = new Vector3 (xl, y, 1f);
				}

			}

			Enemies.name = Enemies.name + m;
			Enemies.GetComponentInChildren<GenericEnemy>().SetMaster(gameObject);
			AvailableEnemies.Add(Enemies);
		}


    }
	//loading the battle menu
	public void CreateBattle(string name)
	{
		InBattle = true;
		//determine background
		LoadLvlBackground();
		//Background.GetComponent<Background>().LoadCombatBG();
		foreach (GameObject ene in AvailableEnemies)
		{
			if(ene.name == name)
			{
				CurrentEnemy = ene;
			}
			else
			{
				//disable the shader
				ene.GetComponent<GenericEnemy>().RevertMaterial();
			}
		}
		ClearMenu();

		CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
	}
    //the multiple choices for combat
    public void BeginCombat( int type)
    {
		questiontype = type;
		Background.GetComponent<Background>().LoadQuestionbg1();
        ClearMenu();
		//disable vision of enemies 
		foreach (GameObject en in AvailableEnemies)
		{
			en.GetComponent<GenericEnemy>().hide();
			en.SetActive(false);
		}
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/CombatQuestion"));
		Background.GetComponent<Background>().overlay.SetActive(false);
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
		CurrentMenu.GetComponent<CombatQuestion>().CreateQuestions(data, type);
	}
	
	//after determining answer do corresponding action
	public void ChangeTurn(Question obj , string playerchoice, int type)
	{
		bool correct = obj.CheckAnswer (playerchoice);
		//Debug.Log("play " + playerchoice + "type" + type);
		if(type == 1)
		{
			foreach(string q in MultipleAnswers)
			{
				//Debug.Log(q + "dsf" + playerchoice);
				if (q == playerchoice)
				{
					return;
				}
			}
			MultipleAnswers.Add(playerchoice);
			if(MultipleAnswers.Count >= 2)
			{
				//go through each answer and make sure all is correct
				foreach(string s in MultipleAnswers)
				{

					correct = obj.CheckAnswer(s);
					//if not correct leace and continue function with wrong answer
					if(!correct)
					{
						break;
					}
				}
			}
			//only have 1 or less
			else
			{
				return;
			}
		
		}
		//resset the multiple answers
		MultipleAnswers.Clear();
		ClearMenu ();
		//reload tge battle screen
		LoadLvlBackground();
		//Background.GetComponent<Background>().LoadCombatBG();
		//reenable vision of enemies 
		foreach (GameObject en in AvailableEnemies)
		{

			en.SetActive(true);
			en.GetComponent<GenericEnemy>().Show();
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

				//RoundOver = true;

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
					//set up to next level

					RoundOver = true;
					//Debug.Log("end");
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
				player.GetComponent<Player>().Health = player.GetComponent<Player>().MaxHealth;
				CurrentMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/BattleLost"));
				return;
			}
			//show enemy dealing damage
			TurnContinue = false;
			StartCoroutine(EnemyTurn(false));
		}
	}
	#region coroutines
	public IEnumerator WaitTurn(float dmg)
	{
		yield return StartCoroutine (PlayerTurn (dmg, CurrentEnemy.GetComponent<GenericEnemy>().name));
	}
	public IEnumerator PlayerTurn(float damage, string enemyname)
	{

		CurrentEnemyName = enemyname;
		//generate item drop
		int itemdrop = (int)Random.Range(0f, CurrentEnemy.GetComponent<GenericEnemy>().Droppable.Count);
		droppeditemname = CurrentEnemy.GetComponent<GenericEnemy>().Droppable[itemdrop];
		TurnContinue = true;
		bool dealt = false;
		//deal damage to enemies
		//Attack boost and question type attack
		if(player.GetComponent<Player>().attkboost == true && questiontype == 0)
		{
			player.GetComponent<Player>().CurrentWeapon.AttackModifier = 1.25f;
		}
		//magic attack with boost potion
		else if(player.GetComponent<Player>().mgattkboost == true && questiontype == 1)
		{
			player.GetComponent<Player>().CurrentWeapon.AttackModifier = 1.25f;
		}
		string combattext = "";
		if(player.GetComponent<Player>().CurrentWeapon.Special == true && player.GetComponent<Player>().CurrentWeapon.AffectAnswers == false)
		{
			player.GetComponent<Player>().CurrentWeapon.SpecialMove(damage, AvailableEnemies, questiontype);
			dealt = true;
		}
		else
		{
			List<GameObject> temp = new List<GameObject>();
			temp.Add(CurrentEnemy);
			player.GetComponent<Player>().CurrentWeapon.DealDamage(damage, temp, questiontype);
		}
		//attack damge

		float dmgdealt = player.GetComponent<Player>().CurrentWeapon.dmgdealt;
		if(dealt == true)
		{
			foreach(GameObject e in AvailableEnemies)
			{
				combattext += "You have dealt " + dmgdealt + " to " + e.GetComponent<GenericEnemy>().name + "\n";
				e.GetComponent<LAppModelProxy>().model.GetDamaged();
			}
		}
		else
		{
			combattext = "You have dealt " + dmgdealt + " to " + enemyname;
			CurrentEnemy.GetComponent<LAppModelProxy>().model.GetDamaged();
		}
		//disable potion effect 
		if(player.GetComponent<Player>().attkboost == true || player.GetComponent<Player>().mgattkboost == true)
		{
			player.GetComponent<Player>().CurrentWeapon.AttackModifier = 1f;
			player.GetComponent<Player>().PotionCD();
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
		StartCoroutine (EnemyTurn (true));
	}
	public IEnumerator EnemyTurn(bool playerattacked)
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
				combat.text = "";
				if(playerattacked == false)
				{
					//feedback for player missed
					combat.text += "Incorrect answer you missed \n";
				}
				//run chance on armor to miss an attack
				if(player.GetComponent<Player>().CurrentArmor.Miss)
				{
					float ran = Random.Range(0f,1f);
					if(ran < player.GetComponent<Player>().CurrentArmor.misschance)
					{
						//missed the chance
						combat.text += CurrentEnemy.GetComponent<GenericEnemy>().name + " misses";
					}
					else
					{
						combat.text += CurrentEnemy.GetComponent<GenericEnemy>().name +" deals " + CurrentEnemy.GetComponent<GenericEnemy>().Damage +" damage to you.";
						player.GetComponent<Player>().ReceiveDamage(CurrentEnemy.GetComponent<GenericEnemy>().Damage );
					}
				}
				else
				{
					combat.text += CurrentEnemy.GetComponent<GenericEnemy>().name +" deals " + CurrentEnemy.GetComponent<GenericEnemy>().Damage +" damage to you.";
					player.GetComponent<Player>().ReceiveDamage(CurrentEnemy.GetComponent<GenericEnemy>().Damage );
				}




				if (player.GetComponent<Player> ().Alive == false) {
					//player is dead
					ClearMenu ();
					player.GetComponent<Player>().Health = player.GetComponent<Player>().MaxHealth;
					CurrentMenu = (GameObject)GameObject.Instantiate (Resources.Load ("Prefabs/BattleLost"));
					StopAllCoroutines ();
					//return;
				}

			}
			else
			{
				//determing the random item drop
				StartCoroutine(DropItem(droppeditemname));

			}
			yield return new WaitForSeconds(2f);
			//InBattle = false;
			//enable vision of enemies 
			foreach (GameObject en in AvailableEnemies)
			{
				if(en != null)
				{
					en.SetActive(true);
					en.GetComponent<GenericEnemy>().Show();
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
			//need to drop random item
			Inventory.AddItem(itemname, 1);
			//Inventory.AddItem(2,"apples",1);
			//adding the definition of the word into learned dictionary
			//player.GetComponent<Player> ().WordDict.Add (CurrentQuestion.Answer, CurrentQuestion.definition);
			RoundOver = false;

			loadback = true;
			Background.GetComponent<Background>().LoadStart();
			Music.clip = inventorymusic;
			Music.Play();

			gameObject.GetComponent<MapScreen>().BattleWin();
		}
	}
	#endregion
	#endregion

}
