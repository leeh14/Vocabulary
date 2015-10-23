using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecipeContentPanelScript : MonoBehaviour {
	public SampleRecipeButton recipeButton;
	public GameObject materialContentPanel;
	public Image icon;
	public Button craftButton;

	public void CraftButtonClicked(){
		recipeButton.CraftButtonClicked ();
	}
}
