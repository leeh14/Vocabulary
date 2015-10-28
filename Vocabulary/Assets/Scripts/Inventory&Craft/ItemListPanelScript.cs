using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemListPanelScript : MonoBehaviour {
	public InventoryScreen _IS;
	public GameObject contentPanel;
	public Button BackButton;


	public void BackClick(){
		_IS.EquipButtonClick ();
	}
}
