using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class DebugScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 0 - armor, 1 - weapons, 2 - material, 3 - potion
		/*
		Inventory.AddItem (3, "Health Potion", 20);
		Inventory.AddItem (2, "Broken Spearhead", 20);
		Inventory.AddItem (2, "Mummy Eye", 20);
		Inventory.AddArmor ("Basic Armor");
		Inventory.AddWeapon ("Basic Sword");
		*/

		List<Item> ltmp4 = new List<Item> ();
		ltmp4.Add (new Item (2, "Broken Spearhead", 5));
		Item itmp4 = new Item (1, "Patchwork Scimitar", 1);
		Inventory.AddRecipe ("Patchwork Scimitar", ltmp4, itmp4);

		List<Item> ltmp3 = new List<Item> ();
		ltmp3.Add (new Item (2, "Mummy Eye", 7));
		Item itmp3 = new Item (1, "Staff of Visions", 1);
		Inventory.AddRecipe ("Staff of Visions", ltmp3, itmp3);

		List<Item> ltmp5 = new List<Item>();
		ltmp5.Add(new Item(2, "Congealed Plasma", 4));
		ltmp5.Add(new Item(2, "Weathered Hide", 4));
		Item itmp5 = new Item (0, "Glowing Scales", 1);
		Inventory.AddRecipe ("Glowing Scales", ltmp5, itmp5);

		List<Item> ltmp6 = new List<Item> ();
		ltmp6.Add (new Item (2, "Shorn Pages", 4));
		ltmp6.Add (new Item (2, "Dark Wisps", 4));
		Item itmp6 = new Item (1, "Quoth Rapier", 1);
		Inventory.AddRecipe ("Quoth Rapier", ltmp6, itmp6);

		List<Item> ltmp7 = new List<Item> ();
		ltmp7.Add (new Item (2, "Sphinx Drop", 3));
		ltmp7.Add (new Item (2, "Forgotten Drop", 1));
		Item itmp7 = new Item (1, "Dictionator", 1);
		Inventory.AddRecipe ("Dictionator", ltmp7, itmp7);

		Reset ();
	}

	public void Reset(){
		Inventory._Items.Clear ();
		Inventory._Armors.Clear ();
		Inventory._Weapons.Clear ();

		Inventory.AddItem (3, "Health Potion", 20);
		Inventory.AddItem (2, "Broken Spearhead", 20);
		Inventory.AddItem (2, "Mummy Eye", 20);
		Inventory.AddArmor ("Basic Armor");
		Inventory.AddWeapon ("Basic Sword");
	}
}
