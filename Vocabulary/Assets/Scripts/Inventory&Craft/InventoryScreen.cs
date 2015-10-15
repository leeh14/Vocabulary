using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InventoryScreen : MonoBehaviour {

	#region Data
	// Panel and canvas
	public static Transform contentPanel;
	private GameObject ItemListPanel;
	private GameObject contentPanelObj;
	private GameObject Canvas;
	private Transform canvas;

	// Main Screen Buttons
	private GameObject InventoryButton;
	private GameObject RecipeButton;
	private GameObject BackButton;
	private GameObject SaveButton;
	private GameObject LoadButton;
	private GameObject ArmorButton;
	private GameObject WeaponButton;

	// List item button
	private GameObject ItemListButton;
	private GameObject RecipeListButton;
	private GameObject ArmorListButton;
	private GameObject WeaponListButton;

	// Reference
	public static GameObject player;

	// Prefab
	public GameObject InventoryButtonPf;
	public GameObject RecipeButtonPf;
	public GameObject BackButtonPf;
	public GameObject SaveButtonPf;
	public GameObject LoadButtonPf;
	public GameObject ArmorButtonPf;
	public GameObject WeaponButtonPf;

	public GameObject ItemListPanelPf;
	public GameObject CanvasPf;

	public GameObject ItemListButtonPf;
	public GameObject RecipeListButtonPf;
	public GameObject ArmorListButtonPf;
	public GameObject WeaponListButtonPf;

	// for debug purpose
	public static string currentArmor;
	public static string currentWeapon;

	#endregion

	// Use this for initialization
	public void StartInventory() {
		Canvas = Instantiate (CanvasPf) as GameObject;
		canvas = Canvas.transform;
		player = GameObject.FindGameObjectWithTag("Player");
		CreateMainScreen ();
		JustDebug ();
	}

	#region functions
	// Main screen with Inventory, Recipe, Armor, and Weapon Button
	void CreateMainScreen(){
		// Inventory button
		InventoryButton = Instantiate (InventoryButtonPf) as GameObject;
		InventoryButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickInventory = () => {
			this.InventoryButtonClick();};
		InventoryButton.GetComponent<Button>().onClick.AddListener(ClickInventory);

		// Recipe button
		RecipeButton = Instantiate (RecipeButtonPf) as GameObject;
		RecipeButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickRecipe = () => {
			this.RecipeButtonClick();};
		RecipeButton.GetComponent<Button>().onClick.AddListener(ClickRecipe);

		// Armor button
		ArmorButton = Instantiate (ArmorButtonPf) as GameObject;
		ArmorButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickArmor = () => {
			this.ArmorButtonClick();};
		ArmorButton.GetComponent<Button> ().onClick.AddListener (ClickArmor);

		// Weapon button
		WeaponButton = Instantiate (WeaponButtonPf) as GameObject;
		WeaponButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickWeapon = () => {
			this.WeaponButtonClick();};
		WeaponButton.GetComponent<Button> ().onClick.AddListener (ClickWeapon);

		// Save Button
		SaveButton = Instantiate (SaveButtonPf) as GameObject;
		SaveButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickSave = () => {
			this.SaveButtonClick();};
		SaveButton.GetComponent<Button>().onClick.AddListener(ClickSave);

		// Load Button
		LoadButton = Instantiate (LoadButtonPf) as GameObject;
		LoadButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickLoad = () => {
			this.LoadButtonClick();};
		LoadButton.GetComponent<Button>().onClick.AddListener(ClickLoad);
	}

	// Create Item List Panel
	void CreateListPanel(){
		ItemListPanel = Instantiate (ItemListPanelPf) as GameObject;
		ItemListPanel.transform.SetParent (canvas, false);
		contentPanelObj = GameObject.Find ("ContentPanel");
		contentPanel = contentPanelObj.transform;

		BackButton = Instantiate (BackButtonPf) as GameObject;
		BackButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickBackButton = () => {
			this.BackButtonClick();};
		BackButton.GetComponent<Button>().onClick.AddListener(ClickBackButton);
	}

	// Populate the item list
	void PopulateItemList(){
		foreach (Item ib in Inventory._Items) {
			GameObject newButton = Instantiate(ItemListButtonPf) as GameObject;
			SampleItemButton sib = newButton.GetComponent<SampleItemButton>();
			sib.nameLable.text = ib.name;
			switch(ib.type){
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
			sib.amountLabel.text = ib.amount.ToString();
			sib.icon.sprite = Resources.Load<Sprite>(ib.name);
			newButton.transform.SetParent(contentPanel, false);
		}
	}

	// Populate the recipe list
	void PopulateRecipeList(){
		foreach (Recipe re in Inventory._Recipes) {
			GameObject newButton = Instantiate(RecipeListButtonPf) as GameObject;
			SampleRecipeButton sib = newButton.GetComponent<SampleRecipeButton>();
			sib.nameLable.text = re.name;
			sib.icon.sprite = Resources.Load<Sprite>(re.name);
			sib.canMake = Inventory.CanMake(re.name);
			sib.button.interactable = sib.canMake;
			newButton.transform.SetParent(contentPanel, false);
		}
	}

	// Populate the armor list
	void PopulateArmorList(){
		foreach (Item ib in Inventory._Items) {
			if (ib.type == 0) {
				GameObject newButton = Instantiate (ArmorListButtonPf) as GameObject;
				SampleArmorButton sab = newButton.GetComponent<SampleArmorButton> ();
				sab.nameLable.text = ib.name;
				sab.icon.sprite = Resources.Load<Sprite> (ib.name);
				sab.equipped = false;
				if(ib.name == currentArmor){
					sab.button.interactable = false;
					sab.equipped = true;
				}
				newButton.transform.SetParent (contentPanel, false);
			}
		}
	}

	// Populate the weapon list
	void PopulateWeaponList(){
		foreach (Item ib in Inventory._Items) {
			if (ib.type == 1) {
				GameObject newButton = Instantiate (WeaponListButtonPf) as GameObject;
				SampleWeaponButton swb = newButton.GetComponent<SampleWeaponButton> ();
				swb.nameLable.text = ib.name;
				swb.icon.sprite = Resources.Load<Sprite> (ib.name);
				swb.equipped = false;
				if(ib.name == currentWeapon){
					swb.button.interactable = false;
					swb.equipped = true;
				}
				newButton.transform.SetParent (contentPanel, false);
			}
		}
	}

	#endregion

	#region button clicked
	// onclick for goto Recipe
	public void RecipeButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateRecipeList ();
	}

	// onclick for goto Inventory
	public void InventoryButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateItemList ();
	}

	// onclick for back to main
	public void BackButtonClick(){
		ClearCanvas ();
		CreateMainScreen ();
	}

	// onclick for goto armor menu 
	public void ArmorButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateArmorList ();
	}

	// onclick for goto weapon menu 
	public void WeaponButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateWeaponList ();
	}

	// onclick for save
	public void SaveButtonClick(){
		Inventory.Save ();
	}
	
	// onclick for load
	public void LoadButtonClick(){
		Inventory.Load ();
	}

	
	// onclick for make in recipe menu
	public static void MakeClick(string name){
		Inventory.MakeProduct (name);
		UpdateRecipeList(name);
	}
	
	// onclick for change armor in armor
	public static void ArmorChange(string name){
		currentArmor = name;
		Debug.Log ("change armor to: " + name);
		UpdateArmorList ();
	}

	// onclick for change weapon in weapon
	public static void WeaponChange(string name){
		currentWeapon = name;
		Debug.Log ("change weapon to: " + name);
		UpdateWeaponList ();
	}

	#endregion

	#region debug
	// just for debug purpose
	void JustDebug(){
		Inventory.AddItem (2, "Apple", 5);
		Inventory.AddItem (2, "Orange", 10);
		Inventory.AddItem (1, "Axe", 1);
		Inventory.AddItem (1, "Spear", 1);
		Inventory.AddItem (0, "Wood Armor", 1);
		Inventory.AddItem (0, "Crystal Armor", 1);

		List<Item> ltmp = new List<Item>();
		ltmp.Add(new Item(2, "Apple", 2));
		ltmp.Add(new Item(2, "Orange", 3));
		Item itmp = new Item(3, "Banana", 1);
		Inventory.AddRecipe("Banana", ltmp, itmp); 

		currentArmor = "Wood Armor";
		currentWeapon = "Axe";
	}
	#endregion

	#region Utilities
	// Clear everything in Canvas
	void ClearCanvas(){
		var children = new List<GameObject> ();
		foreach (Transform child in canvas) {
			if(child.name != "EventSystem"){
				children.Add (child.gameObject);
			}
		}
		children.ForEach (child => Destroy (child));
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

	// refresh the armor list
	public static void UpdateArmorList(){
		if (contentPanel != null) {
			foreach(Transform child in contentPanel.transform){
				SampleArmorButton sab = child.GetComponent<SampleArmorButton>();
				if(sab){
					if(sab.nameLable.text != currentArmor){
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

	// refresh the weapon list
	public static void UpdateWeaponList(){
		if (contentPanel != null) {
			foreach(Transform child in contentPanel.transform){
				SampleWeaponButton swb = child.GetComponent<SampleWeaponButton>();
				if(swb){
					if(swb.nameLable.text != currentWeapon){
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
}
