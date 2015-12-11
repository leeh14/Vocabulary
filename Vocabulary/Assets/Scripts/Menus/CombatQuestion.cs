using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatQuestion : MonoBehaviour {
	public bool ShowAll = false;
	private GameObject master;
	private Button[] options;
	private int ButtonCounter;
	public Text Question;
	// Use this for initialization
	void Start() {
		master = GameObject.FindGameObjectWithTag("GameMaster");
	}
	public void RemoveAnswer()
	{
		ShowAll = true;
	}
	public void Disable(Button but)
	{
		but.interactable = false;
	}

	public void CreateQuestions(Question data, int type)
	{
		int correct = (int)Random.Range(0f,3f);
		int correct2 = correct;
		//magic type 
		if(type == 1)
		{
			while(correct2 == correct)
			{
				correct2 =(int)Random.Range(0f,3f); 
			}
		}
		options = gameObject.GetComponentsInChildren<Button>();
		//Text question = gameObject.GetComponentInChildren<Text>();
		Question.text = data.question;
		Question.verticalOverflow = VerticalWrapMode.Overflow;
		ButtonCounter = 0;
		//135 pass the boundary
		//change the text of all the buttons
		for(int i = 0 ; i < options.Length ; i++){

			if(ButtonCounter >= data.wAnswers.Count)
			{
				//Debug.Log("sadlkfj");
				Destroy(options[i].gameObject);
				break;
			}
			//addin gthe disable option to tell user already clicked on it
			//options[i].onClick.AddListener(()=> Disable(options[i]));
			//do this portion for the incorrect answer for attacks
			if(i == correct)
			{
				if(type == 1)
				{
					options[i].GetComponentInChildren<Text>().text = data.rAnswers[0];
					options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  data.rAnswers[0], type));
				}
				else
				{
					options[i].GetComponentInChildren<Text>().text = data.rAnswer;
					options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  data.rAnswer, type));
				}
				continue;
			}
			if((correct2 != correct) && correct2 == i )
			{
				options[i].GetComponentInChildren<Text>().text = data.rAnswers[1];
				options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data,  data.rAnswers[1],type));
				continue;
			}

			//place wrong answers
			string wronganswer = data.wAnswers[ButtonCounter];
			options[i].GetComponentInChildren<Text>().text = wronganswer;

			//Debug.Log(b.name + data.options[ButtonCounter] + data.options.Length + "counter" +ButtonCounter);
			//string answer =  data.options[ButtonCounter];
			//Debug.Log(answer);
			//add the button corr
			options[i].onClick.AddListener(() => master.GetComponent<GameMaster_Control>().ChangeTurn(data, wronganswer, type));
			//if hide answer hide a wrong answer
			if(ShowAll == true)
			{
				options[i].gameObject.SetActive(false);
				ShowAll = false;
			}
			ButtonCounter++;
		}
	}
}
