using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleInventoryPanelScript : MonoBehaviour {

	public InventoryScreen _IS;
	public GameObject contentPanel;
	public GameObject battleItemButtonPf;

	// populate the inventory buttons
	public void PopulateBattleInventoryButton(){
		foreach (Item it in Inventory._Items) {
			if(it.type == 3){
				GameObject newButton = Instantiate(battleItemButtonPf) as GameObject;
				BattleInventoryButtonScript bib = newButton.GetComponent<BattleInventoryButtonScript>();
				bib.nameLabel.text = it.name;
				bib.amountLabel.text = it.amount.ToString();
				bib.amount = it.amount;
				if(it.amount == 0){
					bib.button.interactable = false;
				}
				bib.icon.sprite = Resources.Load<Sprite> (it.name);
				newButton.transform.SetParent(contentPanel.transform, false);
			}
		}
	}

	// back button clicked
	public void BackClick(){
		
	}
}
