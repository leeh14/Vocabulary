using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

	#region Data
	public static Inventory _Inventory;				// the Inventory reference
	public static List<GenericWeapon> _Weapons;		// the weapon list
	public static List<GenericArmor> _Armors;		// the armor list
	public static List<Item> _Items;				// the list of currently own items
	public static List<Recipe> _Recipes;			// the list of currently own recipes
	public static List<int> _RemoveIndex;			// the removing item index

	public static int currentWeapon;				// current player weapon
	public static int currentArmor;					// current player armor
	#endregion

	// Use this for initialization
	void Start () {
		if (_Inventory == null) {
			DontDestroyOnLoad(gameObject);
			_Inventory = this;
		}else if(_Inventory != this){
			Destroy(gameObject);
		}

		if (_Items == null) {
			_Items = new List<Item> ();
		}
		if (_Weapons == null) {
			_Weapons = new List<GenericWeapon> ();
		}
		if (_Armors == null) {
			_Armors = new List<GenericArmor> ();
		}
		if (_Recipes == null) {
			_Recipes = new List<Recipe> ();
		}
		_RemoveIndex = new List<int>();	
	}

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

	// check how much of this item the player have
	public static int CheckItem(string name){
	
		// find the item
		foreach(Item it in _Items){
			if(it.name == name){
				return it.amount;
			}
		}
		
		return 0;
	}

	// use item
	public static void UseItem(string name){
		switch(name){
		case "Health Potion":
			Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			player.Health += 2;
			break;

		default:
			break;
		}
		RemoveItem (name, 1);
	}

	#endregion

	#region Armor and Weapon

	// change weapon
	public static void ChangeWeapon(string name){
		int index = 0;
		foreach(GenericWeapon weapon in _Weapons){
			if(weapon.name == name){
				currentWeapon = index;
				Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
				player.SetWeapon(_Weapons[index]);
				break;
			}
			index++;
		}
	}

	// add a weapon to weapon list
	public static void AddWeapon(string name){
		switch (name) {
		case "Basic Sword":
			_Weapons.Add(new BasicSword());
			break;
		case "Patchwork Scimitar":
			_Weapons.Add(new PatchworkScimitar());
			break;
		case "Staff of Visions":
			_Weapons.Add (new StaffofVisions());
			break;
		case "Quoth Rapier":
			_Weapons.Add (new QuothRapier());
			break;
		case "Dictionator":
			_Weapons.Add (new Dictionator());
			break;
		default:
			Debug.Log ("No such Weapon");
			break;
		}
	}

	// change armor
	public static void ChangeArmor(string name){
		int index = 0;
		foreach (GenericArmor armor in _Armors) {
			if(armor.name == name){
				currentArmor = index;
				break;
			}
			index++;
		}
	}

	// add an armor to armor list
	public static void AddArmor(string name){
		switch (name) {
		case "Basic Armor":
			_Armors.Add (new BasicArmor());
			break;
		case "Glowing Scales":
			_Armors.Add(new GlowingScales());
			break;
		default:
			Debug.Log ("No such Armor");
			break;
		}
	}
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

	// check if enough of this material
	public static bool CheckEnough(string name, int amount){
		bool enough = false;
		foreach(Item it in _Items){
			if(it.name == name){
				if(it.amount >= amount){
					enough = true;
				}
				break;
			}
		}
		return enough;
	}

	// check if have armor
	public static bool canMakeArmor(string name){
		foreach (GenericArmor ar in _Armors) {
			if(ar.name == name){
				return false;
			}
		}
		return true;
	}

	// check if have weapon
	public static bool canMakeWeapon(string name){
		foreach (GenericWeapon wp in _Weapons) {
			if(wp.name == name){
				return false;
			}
		}
		return true;
	}

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
				foreach(Item it in Inventory._Items){
					if(ma.name == it.name){
						found = true;
						if(ma.amount > it.amount){
							makeAble = false;
						}
					}
				}
				if(!makeAble || !found){
					makeAble = false;
					break;
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

			if(re.product.type != 0 && re.product.type != 1){
				AddItem(re.product.type, re.product.name, re.product.amount);
			}else if (re.product.type == 0){
				AddArmor(re.name);
			}else if (re.product.type == 1){
				AddWeapon(re.name);
			}
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
		if (this.type == other.type && this.name == other.name) {
			return 0;
		} else {
			return 1;
		}
	}
}

[Serializable]
public class InventoryData
{
	public List<Item> _Items;
	public List<Recipe> _Recipes;
	public List<GenericArmor> _Armors;
	public List<GenericWeapon> _Weapons;
}
