using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class Dictionary : MonoBehaviour {
	// load text

	public Dictionary<Word, bool> dictionary;
	public GameObject canvas;
	public GameObject canvasPf;
	public GameObject dictionaryPanel;
	public GameObject dictionaryPanelPf;

	void Start(){
		dictionary = new Dictionary<Word, bool> ();
		LoadDictionary ("Dictionary/dictionary");
	}

	public void CheckWord(){
		List<Question> list = this.GetComponent<QuestionBank> ().S_EasyQuestionBank;
		foreach (Question qs in list) {
			AddWord(qs.rAnswer);
			foreach(string s in qs.wAnswers){
				AddWord(s);
			}
		}
		
		list = this.GetComponent<QuestionBank> ().M_EasyQuestionBank;
		foreach (Question qs in list) {
			foreach(string rs in qs.rAnswers){
				AddWord(rs);
			}
			foreach(string s in qs.wAnswers){
				AddWord(s);
			}
		}
	}

	public void AddWord(string word){
		Word w = null;
		foreach (KeyValuePair<Word, bool> str in dictionary) {
			if(str.Key.wordBase == word || str.Key.wordVariant == word){
				w = str.Key;
				break;
			}
		}
		if (w != null) {
			dictionary [w] = true;
		} else {
			Debug.Log(word + " not found in dictionary");
		}
	}

	public void OpenDictionary(){
		canvas = Instantiate (canvasPf) as GameObject;
		dictionaryPanel = Instantiate (dictionaryPanelPf) as GameObject;
		dictionaryPanel.GetComponent<DictionaryPanelScript> ()._Dictionary = this;
		dictionaryPanel.GetComponent<DictionaryPanelScript> ().PopulateWords ();
		dictionaryPanel.transform.SetParent (canvas.transform, false);
	}
	
	public void CloseDictionary(){
		Destroy (canvas);
		transform.GetComponent<GameMaster_Control> ().LoadMenu ();
	}
	
	public void LoadDictionary(string fileName){
		TextAsset textAsset = (TextAsset)Resources.Load (fileName);
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (textAsset.text);

		XmlNodeList dictionarylist = xmlDoc.GetElementsByTagName ("dictionary");
		foreach (XmlNode word in dictionarylist) {
			XmlNodeList wordContent = word.ChildNodes;
			foreach(XmlNode wContent in wordContent){
				if (wContent.Name == "word"){
					XmlNodeList ws = wContent.ChildNodes;
					string wordBase = "";
					string pos = "";
					string definition = "";
					string wordVariant = "";
					string example = "";
					foreach (XmlNode w in ws) {
						switch(w.Name){
						case "word-base" :
							wordBase = w.InnerText;
							break;
						case "pos":
							pos = w.InnerText;
							break;
						case "definition":
							definition = w.InnerText;
							break;
						case "word-variant":
							wordVariant = w.InnerText;
							break;
						case "example":
							example = w.InnerText;
							break;
						}
					}
					dictionary.Add(new Word(wordBase, pos, definition, wordVariant, example), false);
				}
			}
		}
	}
}
