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

		List<Item> ltmp10 = new List<Item> ();
		ltmp10.Add (new Item (2, "Shattered Headdress", 5));
		ltmp10.Add (new Item (2, "Deep Cloak", 5));
		Item itmp10 = new Item (0, "Shadowy Sneak", 1);
		Inventory.AddRecipe ("Shadowy Sneak", ltmp10, itmp10);

		List<Item> ltmp6 = new List<Item> ();
		ltmp6.Add (new Item (2, "Shorn Pages", 4));
		ltmp6.Add (new Item (2, "Dark Wisps", 4));
		Item itmp6 = new Item (1, "Quoth Rapier", 1);
		Inventory.AddRecipe ("Quoth Rapier", ltmp6, itmp6);

		List<Item> ltmp7 = new List<Item> ();
		ltmp7.Add (new Item (2, "Resonant Ooze", 3));
		Item itmp7 = new Item (3, "Health Potion", 1);
		Inventory.AddRecipe ("Health Potion", ltmp7, itmp7);

		List<Item> ltmp8 = new List<Item> ();
		ltmp8.Add (new Item (2, "Tightened Wrap", 3));
		Item itmp8 = new Item (3, "Attack Boost Potion", 1);
		Inventory.AddRecipe ("Attack Boost Potion", ltmp8, itmp8);

		List<Item> ltmp9 = new List<Item> ();
		ltmp9.Add (new Item (2, "Utter Ink", 3));
		Item itmp9 = new Item (3, "Magic Boost Potion", 1);
		Inventory.AddRecipe ("Magic Boost Potion", ltmp9, itmp9);



		/*
		List<Item> ltmp7 = new List<Item> ();
		ltmp7.Add (new Item (2, "Sphinx Drop", 3));
		ltmp7.Add (new Item (2, "Forgotten Drop", 1));
		Item itmp7 = new Item (1, "Dictionator", 1);
		Inventory.AddRecipe ("Dictionator", ltmp7, itmp7);
		*/
	}

	public void Reset(){
		Inventory._Items.Clear ();
		Inventory._Armors.Clear ();
		Inventory._Weapons.Clear ();

		Inventory.AddItem ("Health Potion", 5);
		Inventory.AddItem ("Broken Spearhead", 10);
		Inventory.AddItem ("Mummy Eye", 10);
		Inventory.AddItem ("Congealed Plasma", 10);
		Inventory.AddItem ("Weathered Hide", 10);
		Inventory.AddItem ("Resonant Ooze", 20);
		Inventory.AddArmor ("Basic Armor");
		Inventory.AddWeapon ("Basic Sword");
		Inventory.currentArmor = 0;
		Inventory.currentWeapon = 0;
	}
}
