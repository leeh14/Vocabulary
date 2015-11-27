using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Background : MonoBehaviour {
	private Sprite  temp;
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	void Start()
	{
	}
	public void LoadCombatBG()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_dungeon1") ;
		gameObject.GetComponent<Image>().sprite = temp;

	}
	public void LoadQuestionbg1()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_QuestionScreen_Background1");
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadCrafting()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_CraftingScreen_background") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadEquipment()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_EquipmentScreen_Background") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadStart()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_StartScreen_Background") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadLibrary()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_Library") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
}
