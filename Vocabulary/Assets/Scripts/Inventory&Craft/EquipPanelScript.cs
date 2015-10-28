using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanelScript : MonoBehaviour {
	public InventoryScreen _IS;
	public Button ArmorButton;
	public Button WeaponButton;
	public Button BackButton;

	public void ArmorClick(){
		_IS.ArmorButtonClick ();
	}

	public void WeaponClick(){
		_IS.WeaponButtonClick ();
	}

	public void BackClick(){
		_IS.BackButtonClick ();
	}
}
