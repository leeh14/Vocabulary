using UnityEngine;
using System.Collections;

public class MapPanelScript : MonoBehaviour {	
	public MapScreen _MS;
	public GameObject contentPanel;
	public int current;

	public void BackClick(){
		if (current == 0) {
			_MS.BackToMain();
		}else if (current == 1) {
			_MS.BackToMap();
		}
	}
}
