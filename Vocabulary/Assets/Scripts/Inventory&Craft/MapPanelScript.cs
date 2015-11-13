using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapPanelScript : MonoBehaviour {	
	public MapScreen _MS;
	public GameObject contentPanel;
	public int current;
	public GameObject header;
	public Button backbutton;

	public void BackClick(){
		if (current == 0) {
			_MS.BackToMain();
		}else if (current == 1) {
			_MS.BackToMap();
		}
	}

	public void TurnOffHeader(){
		header.SetActive (false);
	}

	public void TurnOnHeader(){
		header.SetActive (true);
		backbutton.enabled = false;
		backbutton.enabled = true;
	}
}
