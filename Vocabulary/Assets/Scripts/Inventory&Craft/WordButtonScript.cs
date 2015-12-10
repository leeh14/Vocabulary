using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WordButtonScript : MonoBehaviour {
	public Text WordLabel;
	public Word word;
	public GameObject wordDetailPanel;
	public GameObject wordDetailPanelPf;

	public void wordButtonClick(){
		if (wordDetailPanel == null) {
			OpenDetailPanel ();
		} else {
			CloseDetailPanel ();
		}
	}

	public void OpenDetailPanel(){
		wordDetailPanel = Instantiate (wordDetailPanelPf) as GameObject;
		WordDetailPanelScript wdps = wordDetailPanel.GetComponent<WordDetailPanelScript> ();
		wdps.PosLabel.text = word.pos;
		wdps.DefinitionLabel.text = word.definition;
		wdps.ExampleLabel.text = "\"" + word.example + "\"";
		wordDetailPanel.transform.SetParent (this.transform.parent.transform, false);
		wordDetailPanel.transform.SetSiblingIndex (this.transform.GetSiblingIndex()+1);
	}

	public void CloseDetailPanel(){
		if (wordDetailPanel != null) {
			Destroy(wordDetailPanel);
		}
	}
}
