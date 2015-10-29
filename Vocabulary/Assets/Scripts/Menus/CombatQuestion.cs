using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatQuestion : MonoBehaviour {
	public bool ShowAll = false;
	private GameObject master;
	private Button[] options;
	private int ButtonCounter;
	// Use this for initialization
	void Start() {
		master = GameObject.FindGameObjectWithTag("GameMaster");

		ButtonCounter = 0;
	}
	public void RemoveAnswer()
	{
		ShowAll = true;
	}
	public void CreateQuestions(Question data)
	{
		int correct = (int)Random.Range(0f,3f);
		options = gameObject.GetComponentsInChildren<Button>();
		Text question = gameObject.GetComponentInChildren<Text>();
		question.text = data.question;
		question.verticalOverflow = VerticalWrapMode.Overflow;
		//change the text of all the buttons
		for(int i = 0 ; i < options.Length ; i++){
//			if(options[i].name == "Question")
//			{
//				options[i].GetComponentInChildren<Text>().text = data.Question;
//				options[i].GetComponentInChildren<Text>().verticalOverflow = VerticalWrapMode.Overflow;
//
//			}
//			else
//			{

				//do this portion for the incorrect answer for attacks
			if(i == correct)
			{
				options[i].GetComponentInChildren<Text>().text = data.rAnswer;
				options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  data.rAnswer));
				continue;
			}
			if(options[i].name == "Answer4" && ShowAll == true)
			{
				options[i].gameObject.SetActive(false);
				ShowAll = false;
				continue;
			}
			//place wrong answers
			options[i].GetComponentInChildren<Text>().text = data.wAnswers[ButtonCounter];
			//Debug.Log(b.name + data.options[ButtonCounter] + data.options.Length + "counter" +ButtonCounter);
			//string answer =  data.options[ButtonCounter];
			//Debug.Log(answer);
			//add the button corr
			options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  data.wAnswers[ButtonCounter]));

			ButtonCounter++;
			//}
		}
	}
}
