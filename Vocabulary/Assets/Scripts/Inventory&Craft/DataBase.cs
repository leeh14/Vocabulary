using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class DataBase : MonoBehaviour {

	// Save Data
	public void Save(){
		SaveInventory ();
	}
	
	// Load Data
	public void Load(){
		if (!LoadInventory ()) {
			CreateInventory ();
			this.GetComponent<DebugScript>().Reset();
		}
	}

	#region Inventory

	public void CreateInventory(){
		
		if (Inventory._Items == null) {
			Inventory._Items = new List<Item> ();
		}
		if (Inventory._Weapons == null) {
			Inventory._Weapons = new List<GenericWeapon> ();
		}
		if (Inventory._Armors == null) {
			Inventory._Armors = new List<GenericArmor> ();
		}
		if (Inventory._Recipes == null) {
			Inventory._Recipes = new List<Recipe> ();
		}
	}

	public void SaveInventory(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/InventoryData.dat");
		InventoryData data = new InventoryData ();
		data._Items = Inventory._Items;
		data._Recipes = Inventory._Recipes;
		data._Armors = Inventory._Armors;
		data._Weapons = Inventory._Weapons;
		
		bf.Serialize (file, data);
		file.Close ();
	}

	public bool LoadInventory(){
		if (File.Exists (Application.persistentDataPath + "/InventoryData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/InventoryData.dat", FileMode.Open);
			InventoryData data = (InventoryData)bf.Deserialize(file);
			file.Close ();
			
			Inventory._Items = data._Items;
			Inventory._Recipes = data._Recipes;
			Inventory._Armors = data._Armors;
			Inventory._Weapons = data._Weapons;
			return true;
		}
		return false;
	}
	#endregion

	#region QuestionBank
	public void SaveQuestionBank(){
		QuestionBank qb = GetComponent<QuestionBank> ();
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		QuestionBankData data;

		// S_Easy
		file = File.Create(Application.persistentDataPath + "/S_EasyQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.S_EasyQuestionBank;
		bf.Serialize (file, data);
		file.Close ();

		// S_Normal
		file = File.Create(Application.persistentDataPath + "/S_NormalQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.S_NormalQuestionBank;
		bf.Serialize (file, data);
		file.Close ();

		// S_Hard
		file = File.Create(Application.persistentDataPath + "/S_HardQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.S_HardQuestionBank;
		bf.Serialize (file, data);
		file.Close ();

		// M_Easy
		file = File.Create(Application.persistentDataPath + "/M_EasyQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.M_EasyQuestionBank;
		bf.Serialize (file, data);
		file.Close ();

		// M_Normal
		file = File.Create(Application.persistentDataPath + "/M_NormalQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.M_NormalQuestionBank;
		bf.Serialize (file, data);
		file.Close ();

		// M_Hard
		file = File.Create(Application.persistentDataPath + "/M_HardQuestionBank.dat");
		data = new QuestionBankData ();
		data.qBank = qb.M_HardQuestionBank;
		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadQuestionBank(){
		
	}
	#endregion
}
