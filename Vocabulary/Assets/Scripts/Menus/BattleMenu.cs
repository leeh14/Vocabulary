using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleMenu : MonoBehaviour {
    private GameObject master;
    private Button[] options;
    // Use this for initialization
    void Start() {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        options = gameObject.GetComponentsInChildren<Button>();
		//update the buttons onclick events
		UpdateButtons ();
    }

    // Update is called once per frame
    void Update() {

    }

	void UpdateButtons()
	{
		foreach (Button choice in options) {
			if(choice.name == "Mattk"){

				//choose random question
				string[] question =  master.GetComponent<GameMaster_Control>().questions[Random.Range(0,  master.GetComponent<GameMaster_Control>().questions.Count)];
				//depending on enemy = differenet type of question
				choice.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginCombat(question));
			}
			else if(choice.name == "PhyAttk")
			{
			}
			else if (choice.name == "Items")
			{
				//open inventory and use an item
			}
			else if (choice.name == "Flee")
			{
				//run away from battle end it
			}
		}
	}
	void UpdateMagic(Button magicattk)
	{

	}

    
}
