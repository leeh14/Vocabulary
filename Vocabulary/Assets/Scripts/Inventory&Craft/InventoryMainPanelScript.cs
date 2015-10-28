using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryMainPanelScript : MonoBehaviour {
	public InventoryScreen _IS;
	public Button ToInventoryButton;
	public Button ToEquipButton;
	public Button ToRecipeButton;
	public Button BackButton;

	public void InventoryClick(){
		_IS.InventoryButtonClick ();
	}

	public void EquipClick(){
		_IS.EquipButtonClick ();
	}

	public void RecipeClick(){
		_IS.RecipeButtonClick ();
	}

	public void BackClick(){
		_IS.BackMainClick ();
	}
}
