using UnityEngine;
using System.Collections;

public class QuestionData {
    public string Question;
    public string Answer;
    public string [] options;
	public string name;
	public string definition;

    public QuestionData(string[] data)
    {
		options = new string[5];
		name = "question";
        //determine questoin and answer string
        Question = data[0];
        Answer = data[1];
		//could set 3rd data point as defintin
		int counter = 0;
		for (int i = 1; i < data.Length  ; i ++) {
			options[counter]  = data[i];
			counter++;
		}

    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    

    public bool CheckAnswer(string testAnswer)
    {
        if (testAnswer == Answer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
