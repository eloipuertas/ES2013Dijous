using UnityEngine;
using System.Collections;
using System.IO;

public class SaveOrBack : MonoBehaviour {

	public bool isBack = false, isSave = false;
	
	
	public void OnMouseEnter(){
		if(isBack)
			renderer.material.color = Color.blue;
		else if(isSave)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){
		if(isBack){
				renderer.material.color = Color.red;
		}
		else if(isSave){
				renderer.material.color = Color.red;
		}
	}
	
	public void OnMouseUpAsButton(){
		
		if(isBack)
			Application.LoadLevel(0);
		if(isSave){
			string Path= Application.dataPath + "/Options.txt";
			StreamWriter sw = new StreamWriter(Path);
			sw.WriteLine(PlayerPrefs.GetInt("Difficulty"));
			sw.Flush();
			sw.Close();
			Application.LoadLevel(0);
		}
	}
}
