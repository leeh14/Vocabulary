using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	
	public static Inventory _Inventory;
	public static List<Item> _Items;
	public static List<Recipe> _Recipes;
	public static bool showItems = true;
	public static bool showRecipes = true;
	
	// Use this for initialization
	void Awake () {
		if (_Inventory == null) {
			DontDestroyOnLoad(gameObject);
			_Inventory = this;
		}else if(_Inventory != this){
			Destroy(gameObject);
		}

		_Items = new List<Item>();
		_Recipes = new List<Recipe>();
	}// end of Awake

	// Save Data
	public static void Save(){

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/InventoryData.dat");
		InventoryData data = new InventoryData ();
		data._Items = _Items;
		data._Recipes = _Recipes;

		bf.Serialize (file, data);
		file.Close ();
	}// end of Save

	// Load Data
	public static void Load(){

		if (File.Exists (Application.persistentDataPath + "/InventoryData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/InventoryData.dat", FileMode.Open);
			InventoryData data = (InventoryData)bf.Deserialize(file);
			file.Close ();

			_Items = data._Items;
			_Recipes = data._Recipes;
		}
	}// end of Load

	// Add / find a item and throw into items 
	public static void AddItem(int type, string name, int addAmount){

		// increase exisitng item amount
		bool found = false;
		foreach(Item it in _Items){
			if(it.name == name){
				it.AddMore(addAmount);
				found = true;
				break;
			}
		}

		// add a new item
		if (found == false) {
			_Items.Add(new Item(type, name, addAmount));
		}
	}// end of CreateItem

	// Remove certain amount of item
	public static void RemoveItem(string name, int removeAmount){
		// increase exisitng item amount
		foreach(Item it in _Items){
			if(it.name == name){
				it.RemoveMore(removeAmount);
				if(it.amount == 0){
					_Items.Remove(it);
				}
				break;
			}
		}
	} // end of RemoveItem

	// Show all the items player have
	public static void ShowItems(){
		Debug.Log("New Item List: ");
		foreach (Item it in _Items) {
			Debug.Log(it.name + ": " + it.amount);
		}
	} // end of showItems

	// Add a recipe
	public static void AddRecipe(string name, List<Item> material, Item product){

		// add a new recipe if don't exist
		if (GetRecipe(name) == null) {
			_Recipes.Add(new Recipe(name, material, product));
		}
	}// end of AddRecipe

	// Get a Recipe
	public static Recipe GetRecipe(string name){

		foreach(Recipe re in _Recipes){
			if(re.name == name){
				return re;
			}
		}
		return null;
	}// end of GetRecipe

	// Show Recipes
	public static void ShowRecipes(){
		Debug.Log("New Recipe List: ");
		foreach (Recipe re in _Recipes) {
			Debug.Log(re.name + " material needed:");
			foreach(Item it in re.materials){
				Debug.Log("_" + it.name + " " + it.amount);
			}
		}
	}// end of ShowRecipes

	public static void MakeProduct(string name){
		Recipe re = GetRecipe (name);
		if (re == null) {
			Debug.LogError("Method MakeRecipe: No Such Recipe.");
		} else {
			bool makeAble = true;
			foreach(Item ma in re.materials){
				bool found = false;
				foreach(Item it in _Items){
					if(ma.name == it.name){
						found = true;
						if(it.amount < ma.amount){
							makeAble = false;
						}
					}
				}
				if(!found ||!makeAble){
					Debug.Log ("Method MakeRecipe: Cannot make");
					break;
				}else{
					found = false;
				}
			}

			if(makeAble){
				foreach(Item ma in re.materials){
					foreach(Item it in _Items){
						if(ma.name == it.name){
							RemoveItem(ma.name, ma.amount);
						}
					}
				}
				_Items.Add(re.product);
			}
		}
	}// end of MakeRecipe
	
	public void OnGUI(){
		if (showItems) {
			string stmp = "The Items: \n";
			foreach (Item it in _Items) {
				stmp += it.name + ": " + it.amount + "\n";
			}

			GUI.Label(new Rect(300, 10, 100, 1000),stmp);
		}

		if (showRecipes) {
			string stmp = "The Recipes: \n";
			foreach (Recipe re in _Recipes) {
				stmp += re.name + " material needed: \n";
				foreach(Item it in re.materials){
					stmp += " _" + it.name + " " + it.amount + "\n";
				}
			}
			GUI.Label(new Rect(500, 10, 1000, 1000),stmp);
		}

	}// end of OnGUI
}// end of Inventory

[Serializable]
public class Recipe : IComparable<Recipe>
{
	public string name; 				// name of the recipe
	public List<Item> materials;		// material needed to make product
	public Item product;				// final product

	// constructor
	public Recipe(string n, List<Item> m, Item p){
		this.name = n;
		this.materials = m;
		this.product = p;
	}

	// compareTo method
	public int CompareTo(Recipe other){
		if (other == null) {
			return 1;
		} else if (name == other.name) {
			return 0;
		} else {
			return 1;
		}
	}
}// end of Recipe

[Serializable]
public class Item : IComparable<Item>
{
	public int type; 		// 0 - armor, 1 - weapons, 2 - material, 3 - potion
	public string name;		// name of the item
	public int amount = 0;		// amount the player owns

	// constructor
	public Item(int type, string name){
		this.type = type;
		this.name = name;
		this.amount = 0;
	}

	// constructor with given amount
	public Item(int type, string name, int amount){
		this.type = type;
		this.name = name;
		this.amount = amount;
	}

	// increaseAmount
	public void AddMore(int a){
		this.amount += a;
	}

	// decreaseAmount
	public void RemoveMore(int r){
		this.amount -= r;
	}

	// compareTo method
	public int CompareTo(Item other){
		if(other == null){
			return 1;
		}
		if (type == type && name == name) {
			return 0;
		} else {
			return 1;
		}
	}
}// end of Item

[Serializable]
class InventoryData
{
	public List<Item> _Items;
	public List<Recipe> _Recipes;
}// end of InventoryData
