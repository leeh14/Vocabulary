using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InventoryScreen : MonoBehaviour {

	#region Data
	// Panel and canvas
	private GameObject Canvas;
	private GameObject MainPanel;
	private GameObject InventoryPanel;
	private GameObject EquipPanel;
	private GameObject RecipePanel;
	private GameObject ItemListPanel;
	private GameObject BattleInventoryPanel;

	// list objects
	public static GameObject contentPanel;
	public static GameObject player;
	
	// Prefab
	public GameObject CanvasPf;
	public GameObject MainPanelPf;
	public GameObject InventoryPanelPf;
	public GameObject EquipPanelPf;
	public GameObject RecipePanelPf;
	public GameObject ItemListPanelPf;
	public GameObject BattleInventoryPanelPf;

	public GameObject ItemListButtonPf;
	public GameObject RecipeListButtonPf;
	public GameObject ArmorListButtonPf;
	public GameObject WeaponListButtonPf;
	#endregion

	// Use this for initialization
	public void StartInventory() {
		Canvas = Instantiate (CanvasPf) as GameObject;
		player = GameObject.FindGameObjectWithTag("Player");
		CreateMainScreen ();
		JustDebug ();
	}

	#region Main Screen
	// Main screen with Inventory, Recipe, Armor, and Weapon Button
	void CreateMainScreen(){
		MainPanel = Instantiate (MainPanelPf) as GameObject;
		MainPanel.GetComponent<InventoryMainPanelScript> ()._IS = this;
		MainPanel.transform.SetParent (Canvas.transform, false);
	}

	// onclick for goto Inventory
	public void InventoryButtonClick(){
		ClearCanvas ();
		InventoryPanel = Instantiate (InventoryPanelPf) as GameObject;
		InventoryPanel.GetComponent<InventoryListPanelScript> ()._IS = this;
		InventoryPanel.GetComponent<InventoryListPanelScript> ().ChangeBackground (3);
		contentPanel = InventoryPanel.GetComponent<InventoryListPanelScript> ().contentPanel;
		InventoryPanel.transform.SetParent (Canvas.transform, false);
		PopulateItemList (3);
	}
	
	// onclick for goto Equip
	public void EquipButtonClick(){
		ClearCanvas ();
		EquipPanel = Instantiate (EquipPanelPf) as GameObject;
		EquipPanel.GetComponent<EquipPanelScript> ()._IS = this;
		EquipPanel.GetComponent<EquipPanelScript> ().LoadSprites ();
		EquipPanel.transform.SetParent (Canvas.transform, false);
	}
	
	// onclick for goto Recipe
	public void RecipeButtonClick(){
		ClearCanvas ();
		RecipePanel = Instantiate (RecipePanelPf) as GameObject;
		RecipePanel.GetComponent<RecipePanelScript> ()._IS = this;
		contentPanel = RecipePanel.GetComponent<RecipePanelScript> ().contentPanel;
		RecipePanel.transform.SetParent (Canvas.transform, false);
		PopulateRecipeList ();
	}

	// onclick for back to main
	public void BackButtonClick(){
		ClearCanvas ();
		CreateMainScreen ();
	}
	#endregion

	#region Inventory Screen
	// Populate the item list
	public void PopulateItemList(int type){
		ClearContentPanel ();

		if (type != 1) {
			foreach (Item ib in Inventory._Items) {
				if (ib.type == type) {
					GameObject newButton = Instantiate (ItemListButtonPf) as GameObject;
					SampleItemButton sib = newButton.GetComponent<SampleItemButton> ();
					sib.nameLable.text = ib.name;
					switch (ib.type) {
					case 0:
						sib.typeLabel.text = "Armor";
						break;
					case 1: 
						sib.typeLabel.text = "Weapon";
						break;
					case 2:
						sib.typeLabel.text = "Material";
						break;
					case 3:
						sib.typeLabel.text = "Potion";
						break;
					}
					sib.amountLabel.text = ib.amount.ToString ();
					sib.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + ib.name);
					newButton.transform.SetParent (contentPanel.transform, false);
				}
			}
		}else if (type == 1){
			foreach (GenericArmor ga in Inventory._Armors) {
				GameObject newButton = Instantiate (ItemListButtonPf) as GameObject;
				SampleItemButton sib = newButton.GetComponent<SampleItemButton> ();
				sib.nameLable.text = ga.name;
				sib.typeLabel.text = "Armor";
				sib.amountLabel.text = "1";
				sib.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + ga.name);
				newButton.transform.SetParent (contentPanel.transform, false);
			}
			foreach (GenericWeapon gw in Inventory._Weapons) {
				GameObject newButton = Instantiate (ItemListButtonPf) as GameObject;
				SampleItemButton sib = newButton.GetComponent<SampleItemButton> ();
				sib.nameLable.text = gw.name;
				sib.typeLabel.text = "Weapon";
				sib.amountLabel.text = "1";
				sib.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + gw.name);
				newButton.transform.SetParent (contentPanel.transform, false);
			}
		}
	}

	// clear the content panel
	void ClearContentPanel(){
		var children = new List<GameObject> ();
		foreach (Transform child in contentPanel.transform) {
			if(child.name != "EventSystem"){
				children.Add (child.gameObject);
			}
		}
		children.ForEach (child => Destroy (child));
	}
	#endregion

	#region Recipe
	// Populate the recipe list
	void PopulateRecipeList(){
		int i = 0;
		foreach (Recipe re in Inventory._Recipes) {
			GameObject newButton = Instantiate(RecipeListButtonPf) as GameObject;
			SampleRecipeButton sib = newButton.GetComponent<SampleRecipeButton>();
			sib.nameLabel.text = re.name;
			sib.index = i;
			sib.canMake = Inventory.CanMake(re.name);

			newButton.transform.SetParent(contentPanel.transform, false);
			i++;
		}
	}

	// onclick for make in recipe menu
	public static void MakeClick(string name){
		Inventory.MakeProduct (name);
		UpdateRecipeList(name);
	}

	// refresh the recipe list
	public static void UpdateRecipeList(string name){
		if (contentPanel != null) {
			foreach(Transform child in contentPanel.transform){
				Button bt = child.GetComponent<Button>();
				if(bt){
					if(!Inventory.CanMake(name)){
						bt.interactable = false;
					}
				}
			}
		}
	}
	#endregion
	
	#region Equip
	// create the list panel for armor
	public void CreateListPanel(){
		ItemListPanel = Instantiate (ItemListPanelPf) as GameObject;
		ItemListPanel.GetComponent<ItemListPanelScript> ()._IS = this;
		contentPanel = ItemListPanel.GetComponent<ItemListPanelScript> ().contentPanel;
		ItemListPanel.transform.SetParent (Canvas.transform, false);
	}

	// onclick for goto armor menu 
	public void ArmorButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		Debug.Log ("Armors: " + Inventory._Armors.Count);
		PopulateArmorList ();
	}
	
	// Populate the armor list
	void PopulateArmorList(){
		foreach (GenericArmor ga in Inventory._Armors) {
			GameObject newButton = Instantiate (ArmorListButtonPf) as GameObject;
			SampleArmorButton sab = newButton.GetComponent<SampleArmorButton> ();
			sab.nameLable.text = ga.name;
			sab.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + ga.name);
			sab.equipped = false;
			if(Inventory.currentArmor != null){
				if(ga.name == Inventory._Armors[Inventory.currentArmor].name){
					sab.button.interactable = false;
					sab.equipped = true;
				}
			}
			newButton.transform.SetParent (contentPanel.transform, false);
		}
	}

	// onclick for change armor in armor
	public static void ArmorChange(string name){
		Inventory.ChangeArmor (name);
		Debug.Log ("change armor to: " + name);
		UpdateArmorList (name);
	}
	
	// refresh the armor list
	public static void UpdateArmorList(string name){
		if (contentPanel != null) {
			foreach(Transform child in contentPanel.transform){
				SampleArmorButton sab = child.GetComponent<SampleArmorButton>();
				if(sab){
					if(sab.nameLable.text != name){
						sab.button.interactable = true;
						sab.equipped = false;
					}else{
						sab.button.interactable = false;
						sab.equipped = true;
					}
				}
			}
		}
	}

	// onclick for goto weapon menu 
	public void WeaponButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateWeaponList ();
	}
	
	// Populate the weapon list
	void PopulateWeaponList(){
		foreach (GenericWeapon gw in Inventory._Weapons) {
			GameObject newButton = Instantiate (WeaponListButtonPf) as GameObject;
			SampleWeaponButton swb = newButton.GetComponent<SampleWeaponButton> ();
			swb.nameLable.text = gw.name;
			swb.icon.sprite = Resources.Load<Sprite> ("Item Sprites/" + gw.name);
			swb.equipped = false;
			if(Inventory.currentWeapon != null){
				if(gw.name == Inventory._Weapons[Inventory.currentWeapon].name){
					swb.button.interactable = false;
					swb.equipped = true;
				}
			}
			newButton.transform.SetParent (contentPanel.transform, false);
		}
	}
	
	// onclick for change weapon in weapon
	public static void WeaponChange(string name){
		Inventory.ChangeWeapon (name);
		Debug.Log ("change weapon to: " + name);
		UpdateWeaponList (name);
	}

	// refresh the weapon list
	public static void UpdateWeaponList(string name){
		if (contentPanel != null) {
			foreach(Transform child in contentPanel.transform){
				SampleWeaponButton swb = child.GetComponent<SampleWeaponButton>();
				if(swb){
					if(swb.nameLable.text != name){
						swb.button.interactable = true;
						swb.equipped = false;
					}else{
						swb.button.interactable = false;
						swb.equipped = true;
					}
				}
			}
		}
	}
	#endregion

	#region debug
	// just for debug purpose
	void JustDebug(){
		//Inventory.AddItem (2, "Apple", 5);
		//Inventory.AddItem (2, "Orange", 10);
		//Inventory.AddItem (3, "Health Potion", 20);
		//Inventory.AddItem (2, "Broken Spearhead", 20);
		//Inventory.AddItem (2, "Mummy Eye", 20);

		List<Item> ltmp = new List<Item>();
		ltmp.Add(new Item(2, "Apple", 2));
		ltmp.Add(new Item(2, "Orange", 3));
		Item itmp = new Item(3, "Banana", 1);
		Inventory.AddRecipe("Banana", ltmp, itmp); 

		/*
		List<Item> ltmp2 = new List<Item>();
		ltmp2.Add(new Item(2, "Apple", 2));
		ltmp2.Add(new Item(2, "Banana", 9));
		Item itmp2 = new Item(3, "Watermelon", 1);
		Inventory.AddRecipe("Watermelon", ltmp2, itmp2); 
		*/

		List<Item> ltmp3 = new List<Item> ();
		ltmp3.Add (new Item(2, "Mummy Eye", 7));
		Item itmp3 = new Item(1, "Staff of Visions", 1);
		Inventory.AddRecipe ("Staff of Visions", ltmp3, itmp3);

		List<Item> ltmp4 = new List<Item> ();
		ltmp4.Add(new Item(2, "Broken Spearhead", 5));
		Item itmp4 = new Item(1, "Patchwork Scimitar", 1);
		Inventory.AddRecipe ("Patchwork Scimitar", ltmp4, itmp4);

		Inventory.AddArmor ("Wooden");
		Inventory.AddArmor ("Crystal Armor");

		Inventory.AddWeapon ("Patchwork Scimitar");
	}
	#endregion

	#region Utilities
	// onclick for go back to main screen
	public void BackMainClick(){
		ClearCanvas ();
		Destroy (Canvas);
		transform.GetComponent<GameMaster_Control> ().LoadMenu ();
	}

	// Clear everything in Canvas
	void ClearCanvas(){
		var children = new List<GameObject> ();
		foreach (Transform child in Canvas.transform) {
			if(child.name != "EventSystem"){
				children.Add (child.gameObject);
			}
		}
		children.ForEach (child => Destroy (child));
		contentPanel = null;
	}
	#endregion

	#region BattleInventory
	// Open the inventory in Battle
	public void OpenBattleInventory(){
		if(Canvas == null)
		{
			Canvas = Instantiate (CanvasPf) as GameObject;
			BattleInventoryPanel = Instantiate (BattleInventoryPanelPf) as GameObject;
			BattleInventoryPanel.GetComponent<BattleInventoryPanelScript> ()._IS = this;
			contentPanel = BattleInventoryPanel.GetComponent<BattleInventoryPanelScript> ().contentPanel;
			BattleInventoryPanel.transform.SetParent (Canvas.transform, false);
			BattleInventoryPanel.GetComponent<BattleInventoryPanelScript> ().PopulateBattleInventoryButton ();
		}
	}

	public void CloseBattleInventory(){
		Destroy (Canvas);
		GetComponent<GameMaster_Control> ().Show ();
	}
	#endregion
}
