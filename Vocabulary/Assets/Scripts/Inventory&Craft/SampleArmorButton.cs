using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleArmorButton : MonoBehaviour {
	public Button button;
	public Text nameLable;
	public Image icon;
	public bool equipped;

	public void OnClick(){
		InventoryScreen.ArmorChange (nameLable.text);
	}
}
