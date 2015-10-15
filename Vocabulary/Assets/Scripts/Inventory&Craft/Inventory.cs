using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	#region Data
	public static Inventory _Inventory;				// the Inventory reference
	public static List<Item> _Items;				// the list of currently own items
	public static List<Recipe> _Recipes;			// the list of currently own recipes
	public static List<int> _RemoveIndex;			// the removing item index
	#endregion

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
		_RemoveIndex = new List<int>();
	}

	#region Save & Load
	// Save Data
	public static void Save(){

		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/InventoryData.dat");
		InventoryData data = new InventoryData ();
		data._Items = _Items;
		data._Recipes = _Recipes;

		bf.Serialize (file, data);
		file.Close ();
	}

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
	}
	#endregion

	#region Item
	// type: 0 - armor, 1 - weapons, 2 - material, 3 - potion
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
	}

	// Remove certain amount of item
	public static void RemoveItem(string name, int removeAmount){

		int currentIndex = 0;

		// increase exisitng item amount
		foreach(Item it in _Items){
			if(it.name == name){
				it.RemoveMore(removeAmount);
				if(it.amount == 0){
					_RemoveIndex.Add(currentIndex);
				}
				break;
			}
			currentIndex++;
		}
	} 

	// Remove stuff schedule to remove
	public static void RemoveUpdate(){
		if (_RemoveIndex.Count != 0) {
			int j = 0;
			for(int i = 0; i < _RemoveIndex.Count; i++){
				_Items.RemoveAt(_RemoveIndex[i]-j);
				j++;
			}
			_RemoveIndex.Clear();
		}
	}// end of RemoveUpdate

	#endregion

	#region Recipe
	// Add a recipe
	public static void AddRecipe(string name, List<Item> material, Item product){

		// add a new recipe if don't exist
		if (GetRecipe(name) == null) {
			_Recipes.Add(new Recipe(name, material, product));
		}
	}

	// Get a Recipe
	public static Recipe GetRecipe(string name){

		foreach(Recipe re in _Recipes){
			if(re.name == name){
				return re;
			}
		}
		return null;
	}
	#endregion

	#region Utilities

	// check if a product can be make
	public static bool CanMake(string name){
		Recipe re = GetRecipe (name);
		if (re == null) {
			Debug.LogError ("Method MakeRecipe: " + name + " No Such Recipe.");
			return false;
		} else {
			bool makeAble = true;
			foreach (Item ma in re.materials) {
				bool found = false;
				foreach (Item it in _Items) {
					if (ma.name == it.name) {
						found = true;
						if (it.amount < ma.amount) {
							makeAble = false;
						}
					}
				}
				if (!found || !makeAble) {
					break;
				} else {
					found = false;
				}
			}
			return makeAble;
		}
		return false;
	}

	// make the product
	public static void MakeProduct(string name){
		Recipe re = GetRecipe (name);
			if(CanMake(name)){
				foreach(Item ma in re.materials){
					foreach(Item it in _Items){
						if(ma.name == it.name){
							RemoveItem(ma.name, ma.amount);
						}
					}
				}
				AddItem(re.product.type, re.product.name, re.product.amount);
				Debug.Log ("Crafted: " + re.product.name);
			}
	}
	#endregion

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
}

[Serializable]
public class Item : IComparable<Item>
{
	public int type; 			// 0 - armor, 1 - weapons, 2 - material, 3 - potion
	public string name;			// name of the item
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
}

[Serializable]
class InventoryData
{
	public List<Item> _Items;
	public List<Recipe> _Recipes;
}
