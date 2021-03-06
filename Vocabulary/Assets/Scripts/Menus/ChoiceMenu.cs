﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceMenu : MonoBehaviour {
    private GameObject master;
	// Use this for initialization
	void Start () {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        //add listnener
        Button[] Start = gameObject.GetComponentsInChildren<Button>();
        //the format to add the onclick function to it
		foreach (Button b in Start) {
			if (b.name == "Battle")
			{
				b.onClick.AddListener(() =>master.GetComponent<GameMaster_Control>().LoadMap());
			}
			else if (b.name == "Inven")
			{
				//for now just go to inventory scene

				b.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().LoadInventory());
			}
			else if (b.name == "Dictionary"){
				b.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().LoadDictionary());
			}
		}
        
		//begin battle takes in a list of enemiers
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
