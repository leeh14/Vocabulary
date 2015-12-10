using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class Question : IComparable<Question> 
{
	public Dictionary _dictionary;  // the dictionary reference
	public string id;				// the id of the question
	public int type;				// 0 - standard, 1 - magic
	public int difficulty;			// 0 - easy, 1 - normal, 2 - difficult
	public string question;			// the actual question
	public string rAnswer;			// the right answer
	public List<string> rAnswers;	// the right answers
	public List<string> wAnswers;	// the answers

	// constructor for standard question
	public Question(int difficulty, string question, string rAnswer, List<string> wAnswers){
		this.type = 0;
		this.difficulty = difficulty;
		this.question = question;
		this.rAnswer = rAnswer;
		this.rAnswers = null;
		this.wAnswers = wAnswers;
	}

	// constructor for magic question
	public Question(int difficulty, string question, List<string> rAnswers, List<string> wAnswers){
		this.type = 1;
		this.difficulty = difficulty;
		this.question = question;
		this.rAnswer = null;
		this.rAnswers = rAnswers;
		this.wAnswers = wAnswers;
	}

	// mix the questions
	public List<string> GetOptions(){
		List<string> options; 

		if (type == 0) {
			options = wAnswers;
			int ri = UnityEngine.Random.Range(0, options.Count);
			options.Insert(ri, rAnswer);
		} else {
			options = wAnswers;
			options.Insert(0, rAnswers[0]);
			options.Insert(1, rAnswers[1]);
		}

		for (int i = 0; i < options.Count; i++) {
			string tmp = options [i];
			int randomIndex = UnityEngine.Random.Range (i, options.Count);
			options [i] = options [randomIndex];
			options [randomIndex] = tmp;
		}
		return options;
	}

	// check if get the correct answer
	public bool CheckAnswer(string option){
		if (this.type == 0) {
			if (option == rAnswer) {
				return true;
			}
		} else {
			foreach(string ans in rAnswers){
				if(option == ans){
					return true;
				}
			}
		}
		_dictionary.AddWord (option);
		return false;
	}

	// compareTo method
	public int CompareTo(Question other){
		if(other == null){
			return 1;
		}
		if (type == type && id == id) {
			return 0;
		} else {
			return 1;
		}
	}
}
