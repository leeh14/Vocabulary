using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapPanelScript : MonoBehaviour {	
	public MapScreen _MS;
	public GameObject contentPanel;
	public int current;
	public Button backbutton;

	public void BackClick(){
		if (current == 0) {
			_MS.BackToMain();
		}else if (current == 1) {
			_MS.BackToMap();
		}
	}

	public void TurnOnButton(){
		backbutton.enabled = false;
		backbutton.enabled = true;
	}
}
