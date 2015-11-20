using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DebugScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
			//Inventory.AddItem (2, "Apple", 5);
			//Inventory.AddItem (2, "Orange", 10);
			Inventory.AddItem (3, "Health Potion", 20);
			Inventory.AddItem (2, "Broken Spearhead", 20);
			Inventory.AddItem (2, "Mummy Eye", 20);

			List<Item> ltmp4 = new List<Item> ();
			ltmp4.Add (new Item (2, "Broken Spearhead", 5));
			Item itmp4 = new Item (1, "Patchwork Scimitar", 1);
			Inventory.AddRecipe ("Patchwork Scimitar", ltmp4, itmp4);

			List<Item> ltmp3 = new List<Item> ();
			ltmp3.Add (new Item (2, "Mummy Eye", 7));
			Item itmp3 = new Item (1, "Staff of Visions", 1);
			Inventory.AddRecipe ("Staff of Visions", ltmp3, itmp3);
	}
}
