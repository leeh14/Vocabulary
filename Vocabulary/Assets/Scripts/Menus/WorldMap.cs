using UnityEngine;
using System.Collections;

public class WorldMap : MonoBehaviour {
	public BoxCollider2D lowerlevel;
	public BoxCollider2D MiddleLevel;
	public BoxCollider2D Upperlevel;
	public Camera theCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(0))
		{
			RaycastHit hit;
			Debug.Log("herer");
			Ray r = theCamera.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(r, out hit))
			{
				Debug.Log(hit);
				//a collider was hit, if the name of that collider is "blah", the "blah" button was pressed!
				if(hit.collider == lowerlevel)
				{
					Debug.Log("Lower Level");
				}
			}
		}
	}
	void OnTriggerEnter(Collider coll)
	{
		if(coll == lowerlevel)
		{
			Debug.Log("Lower Level");
		}
		else if(coll == MiddleLevel)
		{
			Debug.Log("middle level");
		}
		else if(coll == Upperlevel)
		{
			Debug.Log("upper levl");
		}
	}
}
