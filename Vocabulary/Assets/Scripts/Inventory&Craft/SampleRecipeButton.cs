using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SampleRecipeButton : MonoBehaviour {
	public Button button;
	public Text nameLabel;
	public int index;
	public GameObject detailPanel;
	public bool canMake = false;
	public bool panelOpen = false;
	public GameObject RecipeContentPanelPf;
	public GameObject SampleMaterialButtonPf;

	// open the panel
	public void RecipeButtonClicked(){
		if (!panelOpen) {
			CreatePanel ();
			panelOpen = true;
		} else {
			Destroy(detailPanel);
			panelOpen = false;
		}
	}

	// craft button clicked
	public void CraftButtonClicked(){
		if(Inventory.CanMake(nameLabel.text)){
			Inventory.MakeProduct(nameLabel.text);
		}
		UpdateDisplay ();
	}

	// update the info after craft
	public void UpdateDisplay(){
		Destroy(detailPanel);
		CreatePanel();
	}

	// create the panel
	public void CreatePanel(){
		detailPanel = Instantiate (RecipeContentPanelPf) as GameObject;
		RecipeContentPanelScript rs = detailPanel.GetComponent<RecipeContentPanelScript> ();
		rs.icon.sprite = Resources.Load<Sprite> (nameLabel.text);
		rs.recipeButton = this;
		PopulateMaterialList (rs.materialContentPanel);
		rs.craftButton.interactable = Inventory.CanMake (nameLabel.text);
		detailPanel.transform.SetParent (this.transform.parent.transform, false);
		detailPanel.transform.SetSiblingIndex (this.transform.GetSiblingIndex()+1);
	}

	public void PopulateMaterialList(GameObject contentPanel){
		Recipe recipe = Inventory._Recipes [index];
		foreach (Item it in recipe.materials) {
			GameObject newMaterial = Instantiate (SampleMaterialButtonPf) as GameObject;
			SampleMaterialButton smb = newMaterial.GetComponent<SampleMaterialButton> ();
			smb.nameLabel.text = it.name;
			smb.quantityLabel.text = "x" + it.amount.ToString();
			smb.ownLabel.text = Inventory.CheckItem(it.name).ToString();
			if(Inventory.CheckEnough(it.name, it.amount)){
				smb.checkIcon.color = Color.green;
			}else{
				smb.checkIcon.color = Color.red;
			}
			newMaterial.transform.SetParent(contentPanel.transform, false);
		}
	}
}
