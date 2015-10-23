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
}
