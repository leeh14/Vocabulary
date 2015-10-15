using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public class MapData{
	public List<LevelData> _Levels;
	public int currentMap;
}

[System.Serializable]
public class LevelData{

	// constructor
	public LevelData(string name, List<string> monsters){
		this.name = name;
		this.monsters = monsters;
		this.currentLevel = 0;
		this.complete = false;
	}

	public string name;
	public List<string> monsters;
	public int currentLevel;
	public bool complete;
}

public class MapLevels : MonoBehaviour {

	public static MapLevels _MapLevels;
	public static List<LevelData> _Levels;
	public static int currentMap;


	void Awake(){
		if (_MapLevels == null) {
			DontDestroyOnLoad(gameObject);
			_MapLevels = this;
		}else if(_MapLevels!= this){
			Destroy(gameObject);
		}

		_Levels = new List<LevelData> ();
		//Load ();
		UploadData ();
	}

	// Save Data
	public static void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/MapData.dat");
		MapData data = new MapData ();
		data._Levels = _Levels;
		data.currentMap = currentMap;
		
		bf.Serialize (file, data);
		file.Close ();
	}// end of Save
	
	// Load Data
	public static void Load(){
		if (File.Exists (Application.persistentDataPath + "/MapData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/MapData.dat", FileMode.Open);
			MapData data = (MapData)bf.Deserialize(file);
			file.Close ();
			
			_Levels = data._Levels;
			currentMap = data.currentMap;
		}
	}// end of Load

	public static List<string> GenerateEnemies()
	{
		//GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		List<string> Monsters = new List<string>();
		float amount = UnityEngine.Random.Range(1.0f,2.0f);
		for (int i = 0; i <= amount ; i++)
		{
			//randoomizze the different names of the enemies
			string mon = "Goblin";
			Monsters.Add(mon);
		}
//		foreach(string s in Monsters)
//		{
//			Debug.Log(s);
//		}
		return Monsters;
	}
	// Debug upload data
	public static void UploadData(){
		//Generate Enemies
		// Level 1: 
		List<string> monsters_1 ;
		monsters_1 = GenerateEnemies();

//		monsters_1.Add ("Monster_1_1");
//		monsters_1.Add ("Monster_1_2");
//		monsters_1.Add ("Monster_1_3");
//		monsters_1.Add ("Monster_1_4");
//		monsters_1.Add ("Monster_1_5");
		LevelData lv_1 = new LevelData ("Level_1", monsters_1);
		_Levels.Add (lv_1);

		// Level 2:
		List<string> monsters_2;
//		monsters_2.Add ("Monster_2_1");
//		monsters_2.Add ("Monster_2_2");
//		monsters_2.Add ("Monster_2_3");
//		monsters_2.Add ("Monster_2_4");
//		monsters_2.Add ("Monster_2_5");
		monsters_2 = GenerateEnemies();
		LevelData lv_2 = new LevelData ("Level_2", monsters_2);
		_Levels.Add (lv_2);

		// Level 2:
		List<string> monsters_3;
//		monsters_3.Add ("Monster_3_1");
//		monsters_3.Add ("Monster_3_2");
//		monsters_3.Add ("Monster_3_3");
//		monsters_3.Add ("Monster_3_4");
//		monsters_3.Add ("Monster_3_5");
		monsters_3 = GenerateEnemies();
		LevelData lv_3 = new LevelData ("Level_3", monsters_3);
		_Levels.Add (lv_3);


		Save ();
	}
}
