using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SampleMapButton : MonoBehaviour {

	public Button button;
	public Text nameLable;
	public Image icon;
	public Text CurrentLv;
	public LevelData levelData;
	public List<Button> mapList;

	void Start(){
		mapList = new List<Button> ();
	}
}
