using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapScreen : MonoBehaviour {

	public GameObject Canvas;
	public Transform canvas;
	public Transform contentPanel;
	public GameObject MenuListPanel;
	public GameObject contentPanelObj;
	public GameObject BackButton;


	// prefabs
	public GameObject CanvasPf;
	public GameObject MenuListPanelPf;
	public GameObject SampleMapButtonPf;
	public GameObject SampleMonsterButtonPf;
	public GameObject BackButtonPf;

	public LevelData lastLevelData;
	public MapData lastMapData;
	public bool battle = false;

	// Use this for initialization
	void Start () {
		StartMap ();
	}

	// Start up the map menu
	void StartMap(){
		Canvas = Instantiate (CanvasPf) as GameObject;
		canvas = Canvas.transform;
		CreateListPanel ();
		PopulateMapButtons ();
	}

	// Create Item List Panel
	void CreateListPanel(){
		MenuListPanel = Instantiate (MenuListPanelPf) as GameObject;
		MenuListPanel.transform.SetParent (canvas, false);
		contentPanelObj = GameObject.Find ("ContentPanel");
		contentPanel = contentPanelObj.transform;
	}

	// Populate Map buttons
	void PopulateMapButtons(){
		LevelData last = null;
		foreach (LevelData ld in MapLevels._Levels) {
			GameObject newButton = Instantiate(SampleMapButtonPf) as GameObject;
			SampleMapButton sb = newButton.GetComponent<SampleMapButton>();
			sb.nameLable.text = ld.name;
			sb.CurrentLv.text = ld.currentLevel + "/" + ld.monsters.Count;
			sb.levelData = ld;
			newButton.transform.SetParent(contentPanel, false);
			UnityEngine.Events.UnityAction ClickMap = () => {
				lastLevelData = sb.levelData;
				this.MapClick(sb.levelData);};
			newButton.GetComponent<Button>().onClick.AddListener(ClickMap);
			newButton.transform.SetParent(contentPanel, false);

			if(last != null && !last.complete){
				newButton.GetComponent<Button>().interactable = false;
			}
			last = ld;
		}
	}

	// load the given name level
	void PopulateLevelButtons(LevelData ld){
		int i = 0;
		foreach (string mon in ld.monsters) {
			GameObject newButton = Instantiate(SampleMonsterButtonPf) as GameObject;
			SampleMonsterButton mb = newButton.GetComponent<SampleMonsterButton>();
			mb.nameLable.text = mon;
			newButton.transform.SetParent(contentPanel, false);
			UnityEngine.Events.UnityAction ClickLevel = () => {
				this.LevelClick(mb.name);};
			newButton.GetComponent<Button>().onClick.AddListener(ClickLevel);
			newButton.transform.SetParent(contentPanel, false);

			if(i > ld.currentLevel && i != 0){
				newButton.GetComponent<Button>().interactable = false;
			}
			i++;
		}

		BackButton = Instantiate (BackButtonPf) as GameObject;
		BackButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickBack = () => {
			this.BackToMap();};
		BackButton.GetComponent<Button>().onClick.AddListener(ClickBack);
	}

	// when a map is clicked, load the levels
	void MapClick(LevelData ld){
		ClearList ();
		PopulateLevelButtons (ld);
	}

	// when a level is clicked, load the level
	void LevelClick(string name){
		ClearCanvas ();
		battle = true;
	}

	// move back to map option	
	void BackToMap(){
		ClearList ();
		PopulateMapButtons ();
	}

	// clear current list
	void ClearList(){
		var children = new List<GameObject> ();
		foreach (Transform child in contentPanel) {
			children.Add (child.gameObject);
		}
		children.ForEach (child => Destroy (child));
	}

	// Clear everything in Canvas
	void ClearCanvas(){
		var children = new List<GameObject> ();
		foreach (Transform child in canvas) {
			if(child.name != "EventSystem"){
				children.Add (child.gameObject);
			}
		}
		children.ForEach (child => Destroy (child));
	}

	// win the battle
	void BattleWin(){
		battle = false;
		if(!lastLevelData.complete){
			lastLevelData.currentLevel++;
		if (lastLevelData.currentLevel == lastLevelData.monsters.Count) {
				lastLevelData.complete = true;
			}
		}
		CreateListPanel ();
		PopulateLevelButtons (lastLevelData);
	}

	// lose the battle
	void BattleLose(){
		battle = false;
		CreateListPanel ();
		PopulateLevelButtons (lastLevelData);
	}

	#region Debug
	void OnGUI(){
		if (battle) {
			if (GUI.Button (new Rect (30, 10, 100, 100), "Win")){
				BattleWin();
			}

			if (GUI.Button (new Rect (30, 120, 100, 100), "Lose")){
				BattleLose();
			}
		}
	}
	#endregion
}
