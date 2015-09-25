using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugScript : MonoBehaviour {

	void OnGUI(){

		if(GUI.Button(new Rect(10, 100, 100, 30), "Add Apple")){
			Inventory.AddItem(3, "Apple", 1);
		}
		if(GUI.Button(new Rect(10, 140, 100, 30), "Show Item")){
			Inventory.ShowItems();
		}
		if(GUI.Button(new Rect(10, 180, 100, 30), "Save")){
			Inventory.Save();
		}
		if(GUI.Button(new Rect(10, 220, 100, 30), "Load")){
			Inventory.Load();
		}
		if(GUI.Button(new Rect(10, 260, 100, 30), "Remove Apple")){
			Inventory.RemoveItem("Apple", 1);
		}
		if (GUI.Button (new Rect (150, 100, 100, 30), "Add Orange")) {
			Inventory.AddItem(3,"Orange",1);
		}
		if (GUI.Button (new Rect (150, 140, 100, 30), "Add B Recipe")) {
			List<Item> ltmp = new List<Item>();
			ltmp.Add(new Item(3, "Apple", 2));
			ltmp.Add(new Item(3, "Orange", 3));
			Item itmp = new Item(3, "Banana", 1);
			Inventory.AddRecipe("Banana", ltmp, itmp); 
		}
		if (GUI.Button (new Rect (150, 180, 100, 30), "Show Recipe")) {
			Inventory.ShowRecipes();
		}
		if (GUI.Button (new Rect (150, 220, 100, 30), "Make Recipe")) {
			Inventory.MakeProduct("Banana");
		}
	}
}
