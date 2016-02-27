using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class CombatText : MonoBehaviour {
    public List<string> combattxt = new List<string>();
    public GameObject master;
    public Text txt;
    private int counter = 0;
	// Use this for initialization
	void Start () {
        txt =GetComponentInChildren<Text>();
        txt.text = combattxt[counter];
        master = GameObject.FindGameObjectWithTag("GameMaster");
	}
	
	// Update is called once per frame
	void Update () {
        //on player click iterate through options
        //if (Input.GetButton("Fire1"))
        //{
        //    counter++;
        //    if(counter > combattxt.Count)
        //    {
        //        Destroy(gameObject);
        //        //load the master
        //    }
        //    else
        //    {
        //        txt.text = combattxt[counter];
        //    }
        //}
    }
    public void OnClick()
    {
        counter++;
        if (counter > combattxt.Count - 1)
        {
            Destroy(gameObject);
            //load the master
            master.GetComponent<GameMaster_Control>().CombatTextOver();
        }
        else
        {
            txt.text = combattxt[counter];
        }
    }
}
