using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleInventoryButtonScript : MonoBehaviour {
	public BattleInventoryPanelScript _BPS;
	public GameMaster_Control _GameMaster;
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
		if(amount == 0 || checkUseCondition(nameLabel.text)){
			button.interactable = false;
		}
	}

	public bool checkUseCondition(string name){
		Player player = _GameMaster.player.GetComponent<Player>();
		switch(name){
		case "Health Potion":
			return player.Health >= player.MaxHealth;
			//break;
		case "Attack Boost Potion":
			return player.attkboost;
			//break;
		case "Magic Boost Potion":
			return player.mgattkboost;
			//break;
		}
		return true;
	}
}
