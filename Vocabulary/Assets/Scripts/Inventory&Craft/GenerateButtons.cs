using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GenerateButtons : MonoBehaviour {
	public GameObject menu2;
	public GameObject MenuCanvas;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
	public void Generate()
	{

		GameObject canvas = GameObject.FindGameObjectWithTag ("MenuButton");
		GameObject b = GameObject.Instantiate (menu2);
		b.transform.SetParent (canvas.transform);
		GameObject temp = GameObject.Instantiate (MenuCanvas);
		Destroy (canvas);
	}
}
