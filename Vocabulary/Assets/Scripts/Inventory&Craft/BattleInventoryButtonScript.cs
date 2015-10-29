using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleInventoryButtonScript : MonoBehaviour {
	public Button button;
	public Text nameLabel;
	public Text amountLabel;
	public Image icon;
	public int amount;

	public void UseItem(){
		if (amount > 0) {
			Inventory.UseItem (nameLabel.text);
			amount--;
			amountLabel.text = amount.ToString ();
			if(amount == 0){
				button.interactable = false;
			}
		}
	}
}
