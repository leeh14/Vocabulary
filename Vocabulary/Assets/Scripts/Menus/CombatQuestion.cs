using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatQuestion : MonoBehaviour {
	private GameObject master;
	private Button[] options;
	private int ButtonCounter;
	// Use this for initialization
	void Start() {
		master = GameObject.FindGameObjectWithTag("GameMaster");

		ButtonCounter = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void CreateQuestions(QuestionData data)
	{
		options = gameObject.GetComponentsInChildren<Button>();
		//change the text of all the buttons
		foreach (Button b in options) {
			if(b.name == "Question")
			{
				b.GetComponentInChildren<Text>().text = data.Question;
				b.GetComponentInChildren<Text>().verticalOverflow = VerticalWrapMode.Overflow;

			}
			else
			{

				b.GetComponentInChildren<Text>().text = data.options[ButtonCounter];
				//Debug.Log(b.name + data.options[ButtonCounter] + data.options.Length + "counter" +ButtonCounter);
				string answer =  data.options[ButtonCounter];
				//Debug.Log(answer);
				//add the button corr
				b.onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  answer));

				ButtonCounter++;
			}
		}
	}
}
