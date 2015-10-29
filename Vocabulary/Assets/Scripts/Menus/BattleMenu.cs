using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleMenu : MonoBehaviour {
    private GameObject master;
    private Button[] options;
	private GameObject player;
	private string text;
    // Use this for initialization
    void Start() {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        options = gameObject.GetComponentsInChildren<Button>();
		player = GameObject.FindGameObjectWithTag("Player");

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
		Text[] combat = gameObject.GetComponentsInChildren<Text>();
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
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginCombat(0));
			}
			else if(choice.name == "PhyAttk")
			{
				//choose random question
				//string[] question =  master.GetComponent<GameMaster_Control>().questions[Random.Range(0,  master.GetComponent<GameMaster_Control>().questions.Count)];
				//depending on enemy = differenet type of question
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginCombat( 1));
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

	public void UpdateText(){
		text = player.GetComponent<Player>().Health + " / " + player.GetComponent<Player>().MaxHealth;
	}
}
