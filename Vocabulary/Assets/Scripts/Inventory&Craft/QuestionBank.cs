using UnityEngine;
using System;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class QuestionBank : MonoBehaviour {
	public List<Question> S_EasyQuestionBank;
	public List<Question> S_NormalQuestionBank;
	public List<Question> S_HardQuestionBank;

	public List<Question> M_EasyQuestionBank;
	public List<Question> M_NormalQuestionBank;
	public List<Question> M_HardQuestionBank;

	// for debug purpose only
	void Start(){
		S_EasyQuestionBank = new List<Question> ();
		S_NormalQuestionBank = new List<Question> ();
		S_HardQuestionBank = new List<Question> ();

		M_EasyQuestionBank = new List<Question> ();
		M_NormalQuestionBank = new List<Question> ();
		M_HardQuestionBank = new List<Question> ();
		
		List<string> q1w = new List<string> ();
		q1w.Add ("insane");
		q1w.Add ("trivial");
		q1w.Add ("traumatic");
		q1w.Add ("ineffable");
		Question q1 = new Question (0, "With his sub-four minute mile Bannister broke a psychological barrier, and inspired thousands of others to attempt to overcome seemingly ____ hurdles.",
		                         	"insurmountable", q1w);
		AddQuestion (q1);

		List<string> q2w = new List<string> ();
		q2w.Add ("amuse");
		q2w.Add ("avert");
		q2w.Add ("juxtapose");
		q2w.Add ("solve");
		Question q2 = new Question (0, "All good comic writers use humor to ____, not to side-step the problems of human behavior.",
		                            "confront", q2w);
		AddQuestion (q2);

		List<string> q3w = new List<string> ();
		q3w.Add ("disappointing");
		q3w.Add ("conceived");
		q3w.Add ("promising");
		q3w.Add ("required");
		List<string> q3r = new List<string> ();
		q3r.Add ("touted");
		q3r.Add ("heralded");
		Question q3 = new Question (0, "The prize competition was ____ as a showcase for new technology, but instead the competition was marred by disqualifications and disputes.",
		                            q3r, q3w);
		AddQuestion (q3);

		List<string> q4w = new List<string> ();
		q4w.Add ("emerging");
		q4w.Add ("booming");
		q4w.Add ("domestic");
		q4w.Add ("bankrupt");
		List<string> q4r = new List<string> ();
		q4r.Add ("languishing");
		q4r.Add ("flagging");
		Question q4 = new Question (0, "The new institute provides intensive postgraduate teaching to a wide range of students, in the hope that these students will use their knowledge to boost the country's ____ economy.",
		                            q4r, q4w);
		AddQuestion (q4);
	}

	// add a question
	public void AddQuestion(Question q){
		switch(q.type){
		case 0:
			switch(q.difficulty){
			case 0:
				S_EasyQuestionBank.Add(q);
				break;
			case 1:
				S_NormalQuestionBank.Add(q);
				break;
			case 2:
				S_HardQuestionBank.Add(q);
				break;
			}
			break;
		case 1:
			switch(q.difficulty){
			case 0:
				M_EasyQuestionBank.Add(q);
				break;
			case 1:
				M_NormalQuestionBank.Add(q);
				break;
			case 2:
				M_HardQuestionBank.Add(q);
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
		if (qBank.Count > 0) {
			int index = UnityEngine.Random.Range (0, qBank.Count);
			return qBank [index];
		}
		return null;
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
