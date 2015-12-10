using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class QuestionBank : MonoBehaviour {
	public List<Question> S_EasyQuestionBank;
	public List<Question> S_NormalQuestionBank;
	public List<Question> S_HardQuestionBank;

	public List<Question> M_EasyQuestionBank;
	public List<Question> M_NormalQuestionBank;
	public List<Question> M_HardQuestionBank;

	public string lastId = "";

	// for debug purpose only
	void Start(){
		S_EasyQuestionBank = new List<Question> ();
		S_NormalQuestionBank = new List<Question> ();
		S_HardQuestionBank = new List<Question> ();

		M_EasyQuestionBank = new List<Question> ();
		M_NormalQuestionBank = new List<Question> ();
		M_HardQuestionBank = new List<Question> ();
	
		LoadQuestions ("Questions/standard_questions");
		LoadQuestions ("QUestions/magic_questions");

		lastId = "";
	}

	// add a question
	public void AddQuestion(Question q){
		switch(q.type){
		case 0:
			switch(q.difficulty){
			case 0:
				S_EasyQuestionBank.Add(q);
				q.id = "SE" + (S_EasyQuestionBank.Count - 1).ToString();
				break;
			case 1:
				S_NormalQuestionBank.Add(q);
				q.id = "SN" + (S_NormalQuestionBank.Count - 1).ToString();
				break;
			case 2:
				S_HardQuestionBank.Add(q);
				q.id = "SH" + (S_HardQuestionBank.Count - 1).ToString();
				break;
			}
			break;
		case 1:
			switch(q.difficulty){
			case 0:
				M_EasyQuestionBank.Add(q);
				q.id = "ME" + (M_EasyQuestionBank.Count - 1).ToString();
				break;
			case 1:
				M_NormalQuestionBank.Add(q);
				q.id = "MN" + (M_NormalQuestionBank.Count - 1).ToString();
				break;
			case 2:
				M_HardQuestionBank.Add(q);
				q.id = "MH" + (M_HardQuestionBank.Count - 1).ToString();
				break;
			}
			break;
		}
	}

	// get a question
	public Question GetQuestion(int type, int difficulty){
		switch(type){
		case 0:
			switch(difficulty){
			case 0:
				return GetRandomQuestion(S_EasyQuestionBank);
			case 1:
				return GetRandomQuestion(S_NormalQuestionBank);
			case 2:
				return GetRandomQuestion(S_HardQuestionBank);
			default:
				return null;
			}
		case 1:
			switch(difficulty){
			case 0:
				return GetRandomQuestion(M_EasyQuestionBank);
			case 1:
				return GetRandomQuestion(M_NormalQuestionBank);
			case 2:
				return GetRandomQuestion(M_HardQuestionBank);
			default:
				return null;
			}
		default:
			return null;
		}
	}

	// get a random question
	public Question GetRandomQuestion(List<Question> qBank){
		int index = 0;
		if (qBank.Count > 0) {
			while (true) {
				index = UnityEngine.Random.Range (0, qBank.Count);
				if (qBank [index].id != lastId) {
					lastId = qBank [index].id;
					break;
				}
			}
			Question q = qBank[index];
			q._dictionary = this.GetComponent<Dictionary>();
			return q;
		} 
		return null;
	}

	// load the questions from xml
	public void LoadQuestions(string fileName){
		// load text
		TextAsset textAsset = (TextAsset)Resources.Load (fileName);
		XmlDocument xmlDoc = new XmlDocument ();
		xmlDoc.LoadXml (textAsset.text);

		// parse
		int type = 0;

		XmlNodeList qlist = xmlDoc.GetElementsByTagName ("qlist");
		foreach (XmlNode qlistChild in qlist) {
			XmlNodeList qlistContent = qlistChild.ChildNodes;
			foreach(XmlNode qContent in qlistChild){
				if (qContent.Name == "question"){
					int difficulty = 0;
					string question = "";
					string rAnswer = "";
					List<string> rAnswers = new List<string>();
					List<string> wAnswers = new List<string>();
					XmlNodeList qs = qContent.ChildNodes;
					foreach (XmlNode q in qs) {
						switch(q.Name){
						case "qstring" :
							question = q.InnerText;
							break;
						case "correct":
							rAnswer = q.InnerText;
							rAnswers.Add(q.InnerText);
							break;
						case "wrong":
							wAnswers.Add(q.InnerText);
							break;
						}
					}

					if(type == 0){
						AddQuestion(new Question(difficulty, question, rAnswer, wAnswers));
					}else if(type == 1){
						AddQuestion(new Question(difficulty, question, rAnswers, wAnswers));
					}
				}else if (qContent.Name == "qtype") {
					switch (qContent.InnerText) {
					case "standard":
						type = 0;
						break;
					case "magic":
						type = 1;
						break;
					}
				}
			}
		}
	}
}

[Serializable]
public class QuestionBankData : IComparable<QuestionBankData> {
	public string name;
	public List<Question> qBank;

	// compareTo method
	public int CompareTo(QuestionBankData other){
		if(other == null){
			return 1;
		}
		if (name == name) {
			return 0;
		} else {
			return 1;
		}
	}
}
