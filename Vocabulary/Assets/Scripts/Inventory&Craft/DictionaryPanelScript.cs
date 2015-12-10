using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DictionaryPanelScript : MonoBehaviour {
	public Dictionary _Dictionary;
	public GameObject contentPanel;
	public GameObject wordButtonPf;

	public void PopulateWords(){
		foreach (KeyValuePair<Word, bool> str in _Dictionary.dictionary) {
			if(str.Value == true){
				GameObject newButton = Instantiate(wordButtonPf) as GameObject;
				WordButtonScript wbs = newButton.GetComponent<WordButtonScript>();
				wbs.WordLabel.text = str.Key.wordBase;
				wbs.word = str.Key;
				newButton.transform.SetParent(contentPanel.transform, false);
			}
		}
	}

	public void BackButtonClick(){
		_Dictionary.CloseDictionary ();
	}
}
