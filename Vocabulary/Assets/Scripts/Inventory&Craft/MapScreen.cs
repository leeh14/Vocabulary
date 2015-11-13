﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapScreen : MonoBehaviour {

	public GameObject canvas;
	public GameObject MenuListPanel;
	public GameObject ContentPanel;
	public GameObject BackButton;
	
	// prefabs
	public GameObject CanvasPf;
	public GameObject MenuListPanelPf;
	public GameObject SampleMapButtonPf;
	public GameObject SampleMonsterButtonPf;

	public LevelData lastLevelData;
	public MapData lastMapData;
	public bool battle = false;

	// Start up the map menu
	public void StartMap(){
		if(canvas == null)
		{
			canvas = Instantiate (CanvasPf) as GameObject;
			CreateListPanel ();
		}

		PopulateMapButtons ();
	}

	// Create Item List Panel
	void CreateListPanel(){
		MenuListPanel = Instantiate (MenuListPanelPf) as GameObject;
		MenuListPanel.GetComponent<MapPanelScript> ()._MS = this;
		ContentPanel = MenuListPanel.GetComponent<MapPanelScript> ().contentPanel;
		MenuListPanel.transform.SetParent (canvas.transform, false);
	}

	// Populate Map buttons
	void PopulateMapButtons(){
		MenuListPanel.GetComponent<MapPanelScript> ().current = 0;
		LevelData last = null;
		foreach (LevelData ld in MapLevels._Levels) {
			GameObject newButton = Instantiate(SampleMapButtonPf) as GameObject;
			SampleMapButton sb = newButton.GetComponent<SampleMapButton>();
			sb.nameLable.text = ld.name;
			sb.CurrentLv.text = ld.currentLevel + "/" + ld.monsters.Count;
			sb.levelData = ld;
			newButton.transform.SetParent(ContentPanel.transform, false);
			UnityEngine.Events.UnityAction ClickMap = () => {
				lastLevelData = sb.levelData;
				this.MapClick(sb.levelData);};
			newButton.GetComponent<Button>().onClick.AddListener(ClickMap);
			newButton.transform.SetParent(ContentPanel.transform, false);

			if(last != null && !last.complete){
				newButton.GetComponent<Button>().interactable = false;
			}
			last = ld;
		}
	}

	// load the given name level
	void PopulateLevelButtons(LevelData ld){
		//Debug.Log("here");
		MenuListPanel.GetComponent<MapPanelScript> ().current = 1;
		int i = 0;
		//create monster
//		foreach(string s in ld.monsters)
//		{
//			Debug.Log("dsf" +s);
//		}
		MenuListPanel.GetComponent<MapPanelScript> ().TurnOffHeader ();
		gameObject.GetComponent<GameMaster_Control>().BeginBattle(ld.monsters);
//		foreach (string mon in ld.monsters) {
//			GameObject newButton = Instantiate(SampleMonsterButtonPf) as GameObject;
//			SampleMonsterButton mb = newButton.GetComponent<SampleMonsterButton>();
//			mb.nameLable.text = mon;
//			newButton.transform.SetParent(contentPanel, false);
//			UnityEngine.Events.UnityAction ClickLevel = () => {
//				this.LevelClick(mb.name);};
//			newButton.GetComponent<Button>().onClick.AddListener(ClickLevel);
//			newButton.transform.SetParent(contentPanel, false);
//
//			if(i > ld.currentLevel && i != 0){
//				newButton.GetComponent<Button>().interactable = false;
//			}
//			i++;
//		}

//		BackButton = Instantiate (BackButtonPf) as GameObject;
//		BackButton.transform.SetParent (canvas, false);
//		UnityEngine.Events.UnityAction ClickBack = () => {
//			this.BackToMap();};
//		BackButton.GetComponent<Button>().onClick.AddListener(ClickBack);
	}

	// when a map is clicked, load the levels
	void MapClick(LevelData ld){
		ClearList ();
		//change to battle sequqn
		PopulateLevelButtons (ld);
	}

	// when a level is clicked, load the level
	void LevelClick(string name){
		ClearCanvas ();
		battle = true;
	}

	// move back to map option	
	public void BackToMap(){
		ClearList ();
		gameObject.GetComponent<GameMaster_Control>().ClearEnemies();
		gameObject.GetComponent<GameMaster_Control>().ClearMenu();
		MenuListPanel.GetComponent<MapPanelScript> ().TurnOnHeader ();
		PopulateMapButtons ();
	}

	// move back to main menu
	public void BackToMain(){
		Destroy (canvas);
		GetComponent<GameMaster_Control> ().LoadMenu ();
	}

	// clear current list
	void ClearList(){
		var children = new List<GameObject> ();
		foreach (Transform child in ContentPanel.transform) {
			children.Add (child.gameObject);
		}
		children.ForEach (child => Destroy (child));
	}

	// Clear everything in Canvas
	void ClearCanvas(){
		var children = new List<GameObject> ();
		foreach (Transform child in canvas.transform) {
			if(child.name != "EventSystem"){
				children.Add (child.gameObject);
			}
		}
		children.ForEach (child => Destroy (child));
	}

	// win the battle
	public void BattleWin(){
		battle = false;
		if(!lastLevelData.complete){
			lastLevelData.currentLevel++;
		if (lastLevelData.currentLevel == lastLevelData.monsters.Count) {
				lastLevelData.complete = true;
			}
		}
		//CreateListPanel ();
		//PopulateLevelButtons (lastLevelData);
		//gameObject.GetComponent<GameMaster_Control>().LoadMap();
	}

	// lose the battle
	public void BattleLose(){
		battle = false;
		CreateListPanel ();
		PopulateLevelButtons (lastLevelData);
	}
}
