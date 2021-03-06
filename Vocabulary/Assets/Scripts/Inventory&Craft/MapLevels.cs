﻿using UnityEngine;
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
	public static bool final = false;

	void Awake(){
		if (_MapLevels == null) {
			DontDestroyOnLoad(gameObject);
			_MapLevels = this;
		}else if(_MapLevels!= this){
			Destroy(gameObject);
		}

		_Levels = new List<LevelData> ();
        if (!Load())
        {
            UploadData();
        }
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
	public static bool Load(){
		if (File.Exists (Application.persistentDataPath + "/MapData.dat")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/MapData.dat", FileMode.Open);
			MapData data = (MapData)bf.Deserialize(file);
			file.Close ();
			
			_Levels = data._Levels;
			currentMap = data.currentMap;
			return true;
		}
		return false;
	}// end of Load

	public static List<string> GenerateEnemies()
	{
		//GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		List<string> Monsters = new List<string>();
		float amount = UnityEngine.Random.Range(1.0f,2.0f);
		for (int i = 0; i <= amount ; i++)
		{
			float type = UnityEngine.Random.Range(0f,1f);
			//randoomizze the different names of the enemies
			string mon;
			if(type < .5f)
			{
                mon = "Goblin";
            }
			else
			{
				float color = UnityEngine.Random.Range(0f,1.0f);
				//different colors of the slime
				if(color < .25)
				{
                    mon = "Slime";
                }
				else if(color >= .25 && color < .5)
				{
                    mon = "SlimeB";
                }
				else if(color >= .5 && color < .85)
				{
                    mon = "SlimeR";
                }
				else
				{
                    mon = "SlimeP";
                }
			}
			Monsters.Add(mon);
		}
		return Monsters;
	}
	public static List<string> GenerateEnemiesLvl2()
	{
		//GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		List<string> Monsters = new List<string>();
		float amount = UnityEngine.Random.Range(1.0f,2.0f);

		for (int i = 0; i <= amount ; i++)
		{
			//randoomizze the different names of the enemies
			float type = UnityEngine.Random.Range(0f,1f);
			string mon;
			if(type > .5f)
			{
				mon = "Tome";
			}
			else
			{
				mon = "Mummy";
			}
			Monsters.Add(mon);
		}
		return Monsters;
	}
	public static List<string> GenerateEnemiesLvl3()
	{
		//GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		List<string> Monsters = new List<string>();
		float amount = UnityEngine.Random.Range(1.0f,2.0f);
		for (int i = 0; i <= amount ; i++)
		{
			//randoomizze the different names of the enemies
			float type = UnityEngine.Random.Range(0f,1f);
			string mon;
			if(type > .3f)
			{
				mon = "Shade";
			}
			else if(type >= .3f && type < 6f)
			{
				mon = "Ghost";
			}
			else
			{
				mon = "Sphinx";
			}
			Monsters.Add(mon);
		}
		return Monsters;
	}
	public static List<string> GenerateEnemiesLvl4()
	{
		//GameObject master = GameObject.FindGameObjectWithTag("GameMaster");
		List<string> Monsters = new List<string>();
		if(final == false)
		{
			float amount = UnityEngine.Random.Range(1.0f,2.0f);
			for (int i = 0; i <= amount ; i++)
			{

				string mon = "Lich";
				Monsters.Add(mon);
			}
			final = true;
		}
		else
		{
			Monsters.Add("Forgotten");
			final = false;
		}
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
		LevelData lv_1 = new LevelData ("Dungeons", monsters_1);
		_Levels.Add (lv_1);

		// Level 2:
		List<string> monsters_2;
//		monsters_2.Add ("Monster_2_1");
//		monsters_2.Add ("Monster_2_2");
//		monsters_2.Add ("Monster_2_3");
//		monsters_2.Add ("Monster_2_4");
//		monsters_2.Add ("Monster_2_5");
		monsters_2 = GenerateEnemiesLvl2();
		LevelData lv_2 = new LevelData ("Library", monsters_2);
		_Levels.Add (lv_2);

		// Level 2:
		List<string> monsters_3;
//		monsters_3.Add ("Monster_3_1");
//		monsters_3.Add ("Monster_3_2");
//		monsters_3.Add ("Monster_3_3");
//		monsters_3.Add ("Monster_3_4");
//		monsters_3.Add ("Monster_3_5");
		monsters_3 = GenerateEnemiesLvl3();
		LevelData lv_3 = new LevelData ("Tower", monsters_3);
		_Levels.Add (lv_3);

		// Level 4:
		List<string> monsters_4;
		//		monsters_3.Add ("Monster_3_1");
		//		monsters_3.Add ("Monster_3_2");
		//		monsters_3.Add ("Monster_3_3");
		//		monsters_3.Add ("Monster_3_4");
		//		monsters_3.Add ("Monster_3_5");
		monsters_4 = GenerateEnemiesLvl4();
		LevelData lv_4 = new LevelData ("Spire", monsters_3);
		_Levels.Add (lv_4);


		Save ();
	}
}
