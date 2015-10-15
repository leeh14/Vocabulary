using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleRecipeButton : MonoBehaviour {
	public Button button;
	public Text nameLable;
	public Image icon;
	public bool canMake = false;

	public void OnClick(){
		InventoryScreen.MakeClick (nameLable.text);
	}
}
