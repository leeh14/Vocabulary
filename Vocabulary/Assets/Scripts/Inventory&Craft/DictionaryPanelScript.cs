using UnityEngine;
using System.Collections;

public class DictionaryPanelScript : MonoBehaviour {
	public Dictionary _Dictionary;
	public GameObject contentPanel;
	public GameObject wordButtonPf;

	public void PopulateWords(){
		foreach (Word w in _Dictionary.dictionary) {
			GameObject newButton = Instantiate(wordButtonPf) as GameObject;
			WordButtonScript wbs = newButton.GetComponent<WordButtonScript>();
			wbs.WordLabel.text = w.wordBase;
			wbs.word = w;
			newButton.transform.SetParent(contentPanel.transform, false);
		}
	}

	public void BackButtonClick(){
		_Dictionary.CloseDictionary ();
	}
}
