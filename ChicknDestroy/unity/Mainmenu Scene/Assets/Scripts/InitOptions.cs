using UnityEngine;
using System.Collections;
using System;

using System.IO;

public class InitOptions : MonoBehaviour {
	
	int Difficulty = 0;
	
	// Use this for initialization
	void Start () {
		
		string Path = Application.dataPath + "/Options.txt";
		print(Path);
		if(File.Exists(Path)){
			StreamReader sr = new StreamReader(Path);
			Difficulty = int.Parse(sr.ReadLine());
			switch(Difficulty){
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
