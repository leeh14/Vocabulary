using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class Dictionary : MonoBehaviour {
	// load text

	public List<Word> dictionary;
	public GameObject canvas;
	public GameObject canvasPf;
	public GameObject dictionaryPanel;
	public GameObject dictionaryPanelPf;

	void Start(){
		dictionary = new List<Word> ();
		LoadDictionary ("Dictionary/dictionary");
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
						}
					}
					dictionary.Add(new Word(wordBase, pos, definition,wordVariant));
				}
			}
		}
	}
}
