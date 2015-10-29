using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EquipPanelScript : MonoBehaviour {
	public InventoryScreen _IS;
	public Button ArmorButton;
	public Button WeaponButton;
	public Button BackButton;

	public void LoadSprites(){
		ArmorButton.GetComponent<Image>().sprite = 
			Resources.Load<Sprite> ("Item Sprites/" + Inventory._Armors[Inventory.currentArmor].name);
		WeaponButton.GetComponent<Image>().sprite =
			Resources.Load<Sprite> ("Item Sprites/" + Inventory._Weapons[Inventory.currentWeapon].name);
	}

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
