using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class BackMenu : MonoBehaviour {
	public Animator BackMovement;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void GoBack()
	{
		string parent = gameObject.name;
		parent = parent.Replace ("(Clone)", "");
		Debug.Log ("here" + parent);
		BackMovement.SetBool ("Move", true);
		if (parent == "AttackMenu") 
		{
			var temp = GameObject.Instantiate(Resources.Load("Prefabs/MenuUI"));
		}

		StartCoroutine (Menu());
	}
	public void Disable()
	{
		foreach(Transform child in transform)
		{
			Button b = child.gameObject.GetComponent<Button>();
			if(b != null)
			{
				b.interactable = false;
			}

			//Button temp = (Button)child;
			//if(temp != null)
			//{
			//	temp.enabled = false;
			//}
		}
		StartCoroutine (ReEnable ());
	}
	IEnumerator ReEnable()
	{
		yield return new WaitForSeconds (2);
		foreach (Transform child in transform) {
			Button b = child.gameObject.GetComponent<Button> ();
			if (b != null) {
				b.interactable = true;
			}
		}
	}
	IEnumerator Menu()
	{

		yield return new WaitForSeconds (5); 
		Destroy (gameObject);	
	}

}
