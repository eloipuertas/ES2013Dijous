using UnityEngine;
using System.Collections;
using System.IO;

/* This scrtipt control the save or back buttons in options scene
 * */

public class SaveOrBack : MonoBehaviour {

	public bool isBack = false, isSave = false;//referenced to the buttons
	
	
	public void OnMouseEnter(){//hovering
		if(isBack)
			renderer.material.color = Color.blue;
		else if(isSave)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){//leaving hover
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
		if(isSave){//if save we save the configuration of the options into a file
			string Path= Application.dataPath + "/Options.txt";
			StreamWriter sw = new StreamWriter(Path);//opening the file to write
			sw.WriteLine(PlayerPrefs.GetInt("Difficulty"));
			sw.Flush();
			sw.Close();
			Application.LoadLevel(0);
		}
	}
}
