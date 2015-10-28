using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class DataBase : MonoBehaviour {

	// Save Data
	public void Save(){
		SaveInventory ();
	}
	
	// Load Data
	public void Load(){
		LoadInventory ();
	}

	#region Inventory
	public void SaveInventory(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/InventoryData.dat");
		InventoryData data = new InventoryData ();
		data._Items = Inventory._Items;
		data._Recipes = Inventory._Recipes;
		
		bf.Serialize (file, data);
		file.Close ();
	}

	public void LoadInventory(){
		if (File.Exists (Application.persistentDataPath + "/InventoryData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/InventoryData.dat", FileMode.Open);
			InventoryData data = (InventoryData)bf.Deserialize(file);
			file.Close ();
			
			Inventory._Items = data._Items;
			Inventory._Recipes = data._Recipes;
		}
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
