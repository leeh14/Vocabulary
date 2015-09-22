using UnityEngine;
using System.Collections;

public class QuestionData : MonoBehaviour {
    public string Question;
    public string Answer;
    private string[] options;
    public QuestionData()
    {
    }
    public QuestionData(string[] data)
    {
        //determine questoin and answer string
        Question = data[0];
        Answer = data[1];

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
