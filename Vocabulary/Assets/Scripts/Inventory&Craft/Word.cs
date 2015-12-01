using UnityEngine;
using System.Collections;

public class Word : MonoBehaviour {
	public string wordBase;
	public string pos;
	public string definition;
	public string wordVariant;

	public Word (string wordBase, string pos, string definition, string wordVariant){
		this.wordBase = wordBase;
		this.pos = pos;
		this.definition = definition;
		this.wordVariant = wordVariant;
	}
}
