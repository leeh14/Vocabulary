using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button StartMenu= gameObject.GetComponentInChildren<Button>();
		GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		StartMenu.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().LoadMenu());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
