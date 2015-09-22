using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceMenu : MonoBehaviour {
    private GameObject master;
	// Use this for initialization
	void Start () {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        //add listnener
        Button Start = gameObject.GetComponentInChildren<Button>();
        //the format to add the onclick function to it
        Start.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().BeginBattle());
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
