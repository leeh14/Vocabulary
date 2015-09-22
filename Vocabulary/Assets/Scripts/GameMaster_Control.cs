using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameMaster_Control : MonoBehaviour {
    GameObject CurrentMenu;
    // Use this for initialization
    void Start() {
        //debugging purposes in scene just auto generate the choice menu
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/ChoiceMenu"));
        
    }

    // Update is called once per frame
    void Update() {

    }
    //keep the data here
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Save()
    {

    }
    void Load()
    {
    }
    public void BeginBattle()
    {
        ClearMenu();
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/BattleMenu"));
    }
    //the multiple choices for combat
    public void BeginCombat(string[] data)
    {
        ClearMenu();
        CurrentMenu = (GameObject)GameObject.Instantiate(Resources.Load("Prefabs/CombatQuestion"));
        //add the questions to the buttons
        QuestionData question = new QuestionData(data);
        CurrentMenu.GetComponent<BattleMenu>().CreateQuestions(question);
    }
    //remove the current menu on the screen
    void ClearMenu()
    {
        Destroy(CurrentMenu);
    }
}
