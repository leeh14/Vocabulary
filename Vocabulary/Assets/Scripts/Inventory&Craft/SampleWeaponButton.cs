using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleWeaponButton : MonoBehaviour {
	public Button button;
	public Text nameLable;
	public Image icon;
	public bool equipped;

	public void OnClick(){
		InventoryScreen.WeaponChange (nameLable.text);
	}
}
