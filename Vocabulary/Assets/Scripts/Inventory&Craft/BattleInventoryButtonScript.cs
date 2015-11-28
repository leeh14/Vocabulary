using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleInventoryButtonScript : MonoBehaviour {
	public BattleInventoryPanelScript _BPS;
	public Button button;
	public Text nameLabel;
	public Text amountLabel;
	public Image icon;
	public int amount;
	public int index;

	public void ItemClicked(){
		_BPS.OpenConfirmPanel (index);
	}

	public void VisualUpdate(){
		amount--;
		amountLabel.text = amount.ToString ();
		if(amount == 0){
			button.interactable = false;
		}
	}
}
