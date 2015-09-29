using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ItemButton {
	public string name;
	public Sprite icon;
	public string type;
	public string amount;
}

public class InventoryScreen : MonoBehaviour {

	public GameObject ItemListPanel;
	public GameObject sampleButton;
	public GameObject sampleButton2;
	public GameObject InventoryButton;
	public GameObject RecipeButton;
	public GameObject contentPanelObj;	
	public GameObject BackButton;
	public GameObject SaveButton;
	public GameObject LoadButton;
	
	public Transform canvas;
	public Transform contentPanel;
	public List<ItemButton> ItemButtonList;

	// Prefab
	public GameObject ItemListPanelPf;
	public GameObject sampleButtonPf;
	public GameObject sampleButton2Pf;
	public GameObject InventoryButtonPf;
	public GameObject RecipeButtonPf;
	public GameObject BackButtonPf;
	public GameObject SaveButtonPf;
	public GameObject LoadButtonPf;

	// Use this for initialization
	void Start () {
		CreateMainScreen ();
		JustDebug ();
	}

	// Main screen with Inventory and Recipe Button
	void CreateMainScreen(){
		InventoryButton = Instantiate (InventoryButtonPf) as GameObject;
		InventoryButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickInventory = () => {
			this.InventoryButtonClick();};
		InventoryButton.GetComponent<Button>().onClick.AddListener(ClickInventory);

		RecipeButton = Instantiate (RecipeButtonPf) as GameObject;
		RecipeButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickRecipe = () => {
			this.RecipeButtonClick();};
		RecipeButton.GetComponent<Button>().onClick.AddListener(ClickRecipe);
	
		SaveButton = Instantiate (SaveButtonPf) as GameObject;
		SaveButton.transform.SetParent (canvas, false);
		UnityEngine.Events.UnityAction ClickSave = () => {
			this.SaveButtonClick();};
		SaveButton.GetComponent<Button>().onClick.AddListener(ClickSave);

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
			GameObject newButton = Instantiate(sampleButtonPf) as GameObject;
			SampleItemButton sib = newButton.GetComponent<SampleItemButton>();
			sib.nameLable.text = ib.name;
			sib.typeLabel.text = ib.type.ToString();
			sib.amountLabel.text = ib.amount.ToString();
			sib.icon = Resources.Load(ib.name+".png") as Image;
			newButton.transform.SetParent(contentPanel, false);
		}
	}

	// Populate the recipe list
	void PopulateRecipeList(){
		foreach (Recipe re in Inventory._Recipes) {
			GameObject newButton = Instantiate(sampleButton2Pf) as GameObject;
			SampleRecipeButton sib = newButton.GetComponent<SampleRecipeButton>();
			sib.nameLable.text = re.name;
			sib.icon = Resources.Load(re.name+".png") as Image;
			sib.canMake = Inventory.CanMake(re.name);
			sib.button.interactable = sib.canMake;
			UnityEngine.Events.UnityAction ClickMake = () => {
				this.MakeClick(re.name);};
			newButton.GetComponent<Button>().onClick.AddListener(ClickMake);
			newButton.transform.SetParent(contentPanel, false);
		}
	}

	// Buttons

	public void RecipeButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateRecipeList ();
	}

	// onclick for InventoryButton
	public void InventoryButtonClick(){
		ClearCanvas ();
		CreateListPanel ();
		PopulateItemList ();
	}

	// onclick for backbutton
	public void BackButtonClick(){
		ClearCanvas ();
		CreateMainScreen ();
	}

	public void MakeClick(string name){
		Inventory.MakeProduct (name);
	}

	public void SaveButtonClick(){
		Inventory.Save ();
	}

	public void LoadButtonClick(){
		Inventory.Load ();
	}

	// just for debug purpose
	void JustDebug(){
		Inventory.AddItem (2, "Apple", 5);
		Inventory.AddItem (2, "Orange", 10);

		List<Item> ltmp = new List<Item>();
		ltmp.Add(new Item(3, "Apple", 2));
		ltmp.Add(new Item(3, "Orange", 3));
		Item itmp = new Item(3, "Banana", 1);
		Inventory.AddRecipe("Banana", ltmp, itmp); 
	}

	// utilities

	// Clear everything in Canvas
	void ClearCanvas(){
		var children = new List<GameObject> ();
		foreach (Transform child in canvas)
			children.Add (child.gameObject);
		children.ForEach (child => Destroy (child));
	}
}
