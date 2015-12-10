using UnityEngine;
using System;
using System.Collections;


public class Word : IComparable<Word>{
	public string wordBase;
	public string pos;
	public string definition;
	public string wordVariant;
	public string example;

	public Word (string wordBase, string pos, string definition, string wordVariant, string example){
		this.wordBase = wordBase;
		this.pos = pos;
		this.definition = definition;
		this.wordVariant = wordVariant;
		this.example = example;
	}

	public int CompareTo(Word other){
		if (wordBase == other.wordBase && wordVariant == other.wordVariant) {
			return 0;
		} else {
			return 1;
		}
	}
}
