using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Winning : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button WinMenu= gameObject.GetComponentInChildren<Button>();
		GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		WinMenu.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().LoadMenu());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
