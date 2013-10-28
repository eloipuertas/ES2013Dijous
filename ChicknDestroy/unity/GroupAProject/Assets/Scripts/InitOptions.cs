using UnityEngine;
using System.Collections;
using System;

using System.IO;

/*
 * This script executes with the init of the options scene
 **/

public class InitOptions : MonoBehaviour {
	
	int Difficulty = 0;
	
	// Use this for initialization
	void Start () {
		
		string Path = Application.dataPath + "/Options.txt";
		print(Path);
		if(File.Exists(Path)){//If options has already a configuration Sets it's configuration
			StreamReader sr = new StreamReader(Path);//Opening a file to read
			Difficulty = int.Parse(sr.ReadLine());//reading configuration of the file
			switch(Difficulty){//setting  the gameObjects properly
				case 1:
					GameObject.Find("Easy").collider.enabled = false;
					GameObject.Find("Easy").renderer.material.color = Color.cyan;
				break;
				
				case 2:
					GameObject.Find("Normal").collider.enabled = false;
					GameObject.Find("Normal").renderer.material.color = Color.cyan;
				break;
				
				case 3:
					GameObject.Find("Difficult").collider.enabled = false;
					GameObject.Find("Difficult").renderer.material.color = Color.cyan;

				break;
			}
			sr.Close();
		}
		GameObject.Find("Save").collider.enabled = false;
		
	}

}
