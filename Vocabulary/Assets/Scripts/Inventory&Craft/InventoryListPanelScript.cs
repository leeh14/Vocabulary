using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryListPanelScript : MonoBehaviour {
	public InventoryScreen _IS;
	public GameObject contentPanel;
	public Button BackButton;
	public Image background;
	public Button ItemButton;
	public Button EquipButton;
	public Button MaterialButton;

	public void ChangeBackground(int type){
		switch (type) {
		case 1:
			background.sprite = Resources.Load<Sprite> ("Backgrounds/UI_Inventory_Equipment");
			break;
		case 2:
			background.sprite = Resources.Load<Sprite> ("Backgrounds/UI_Inventory_Materials");
			break;
		case 3:
			background.sprite = Resources.Load<Sprite> ("Backgrounds/UI_Inventory_Usable");
			break;
		}
	}

	public void BackClick(){
		_IS.BackButtonClick ();
	}

	public void ItemClick(){
		_IS.PopulateItemList (3);
		ChangeBackground (3);
	}

	public void EquipClick(){
		_IS.PopulateItemList (1);
		ChangeBackground (1);
	}

	public void MaterialClick(){
		_IS.PopulateItemList (2);
		ChangeBackground (2);
	}
}
