using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BattleInventoryPanelScript : MonoBehaviour {

	public InventoryScreen _IS;
	public GameMaster_Control _GameMaster;
	public List<GameObject> itemList;
	public GameObject contentPanel;
	public GameObject confirmPanel;
	public GameObject ScrollView;
	public GameObject battleItemButtonPf;
	public int currentItem = 0;

	// populate the inventory buttons
	public void PopulateBattleInventoryButton(){
		itemList = new List<GameObject> ();
		int index = 0;
		foreach (Item it in Inventory._Items) {
			if(it.type == 3){
				GameObject newButton = Instantiate(battleItemButtonPf) as GameObject;
				BattleInventoryButtonScript bib = newButton.GetComponent<BattleInventoryButtonScript>();
				bib.nameLabel.text = it.name;
				bib.amountLabel.text = it.amount.ToString();
				bib.amount = it.amount;
				bib.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + it.name);
				bib._BPS = this;
				bib.index = index;
				bib._GameMaster = this._GameMaster;
				bib.VisualUpdate();
				newButton.transform.SetParent(contentPanel.transform, false);
				itemList.Add(newButton);
				index++;
			}
		}
	}

	// open confirm panel
	public void OpenConfirmPanel(int index){
		currentItem = index;
		confirmPanel.SetActive (true);
		ScrollView.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	// close confirm panel
	public void CloseConfirmPanel(){
		confirmPanel.SetActive (false);
		ScrollView.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}

	// use the item
	public void UseItem(){
		BattleInventoryButtonScript bib = itemList [currentItem].GetComponent<BattleInventoryButtonScript>();
		Inventory.UseItem (bib.nameLabel.text);
		bib.VisualUpdate ();
		CloseConfirmPanel ();
	}

	// back button clicked
	public void BackClick(){
		_IS.CloseBattleInventory ();
	}
}
