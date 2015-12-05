using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Background : MonoBehaviour {
	private Sprite  temp;
	public GameObject overlay;
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}
	void Start()
	{
	}
	public void LoadCombatBG()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_Dungeon") ;
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
		overlay.SetActive(false);
	}
	public void LoadLibrary()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_Library") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadSpire()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_Spire") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
	public void LoadTower()
	{
		temp = Resources.Load<Sprite>("Backgrounds/UI_BattleScreen_Tower") ;
		gameObject.GetComponent<Image>().sprite = temp;
	}
}
