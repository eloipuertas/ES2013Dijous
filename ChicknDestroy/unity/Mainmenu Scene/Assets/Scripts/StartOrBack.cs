using UnityEngine;
using System.Collections;

/*
 * This script Controls the start and back button in TeamCHoose scene
 * */

public class StartOrBack : MonoBehaviour {
	
	public bool isBack = false, isStart = false;//referenced to the buttons
	
	
	public void OnMouseEnter(){//hovering
		if(isBack)
			renderer.material.color = Color.blue;
		else if(isStart)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){//leaving hover
		if(isBack){
				renderer.material.color = Color.red;
		}
		else if(isStart){
				renderer.material.color = Color.red;
		}
	}
	
	public void OnMouseUpAsButton(){//selecting
		
		if(isBack)
			Application.LoadLevel(0);
		if(isStart){//if start we set the variables we want to use in the game
			PlayerPrefs.SetFloat("PosX",0);
			PlayerPrefs.SetFloat("PosY",0);
			PlayerPrefs.SetFloat("PosZ",0);
			Application.LoadLevel(4);
		}
	}
	
}
