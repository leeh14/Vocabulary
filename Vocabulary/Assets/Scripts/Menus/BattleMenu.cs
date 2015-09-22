using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleMenu : MonoBehaviour {
    private GameObject master;
    private Button[] options;
    // Use this for initialization
    void Start() {
        master = GameObject.FindGameObjectWithTag("GameMaster");
        options = master.GetComponentsInChildren<Button>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void CreateQuestions(QuestionData data)
    {
        //change the text of all the buttons
    }
}
