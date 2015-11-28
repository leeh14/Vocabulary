using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SampleItemButton : MonoBehaviour {

	public Button button;
	public Text nameLable;
	public Image icon;
	public Text typeLabel;
	public Text amountLabel;
	public int index;
	public GameObject detailPanel;
	public GameObject detailPanelPf;

	public void ItemClicked(){
		if (detailPanel == null) {
			OpenPanel ();
		} else {
			ClosePanel();
		}
	}

	public void OpenPanel(){
		detailPanel = Instantiate (detailPanelPf) as GameObject;
		detailPanel.GetComponent<ItemDetailPanelScript> ().description.text 
			= Inventory.getDescription (index, typeLabel.text);
		detailPanel.transform.SetParent (this.transform.parent.transform, false);
		detailPanel.transform.SetSiblingIndex (this.transform.GetSiblingIndex()+1);
	}

	public void ClosePanel(){
		Destroy (detailPanel);
	}
}
