using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleLost : MonoBehaviour {
	private GameObject Master;
	// Use this for initialization
	void Start () {
		Master = GameObject.FindGameObjectWithTag("GameMaster");
		//add button on click events to do 
		Button[] b = gameObject.GetComponentsInChildren<Button> ();
		foreach (Button button in b) {
			if(button.name == "Redo")
			{
				button.onClick.AddListener(() => Master.GetComponent<GameMaster_Control>().RedoBattle());
			}
			else if(button.name == "Quit")
			{
				button.onClick.AddListener(() => Master.GetComponent<GameMaster_Control>().LoadMenu());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
