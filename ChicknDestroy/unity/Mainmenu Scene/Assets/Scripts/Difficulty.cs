using UnityEngine;
using System.Collections;

/*
 *This Script controls de difficulty buttons in the options scene 
 * 
 */

public class Difficulty : MonoBehaviour {
	
	public bool isEasy = false, isNormal = false, isDifficult = false;//booleans referenced to difficulty buttons

	public void OnMouseEnter(){//hover state
		if(isEasy)
			renderer.material.color = Color.blue;
		else if(isNormal)
			renderer.material.color = Color.blue;
		else if(isDifficult)
			renderer.material.color = Color.blue;
	}

	public void OnMouseExit(){//when mause stops hovering
		if(isEasy)
			if(!collider.enabled)//if button has been selected
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		else if(isNormal)
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		else if(isDifficult)
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
	}
	
	public void OnMouseUpAsButton(){//Selecting a button
		
		if(isEasy){//when selected a button we disable the collider of the selected button and set the possible previous selected buttons enabling the colider and making it visible
			renderer.material.color = Color.cyan;
			collider.enabled = false;
			PlayerPrefs.SetInt("Difficulty",1);//global var
			GameObject.Find("Normal").collider.enabled = true;
			GameObject.Find("Normal").renderer.material.color = Color.red;
			GameObject.Find("Difficult").collider.enabled = true;
			GameObject.Find("Difficult").renderer.material.color = Color.red;
		}
		else if(isNormal){
			renderer.material.color = Color.cyan;
			collider.enabled = false;
			PlayerPrefs.SetInt("Difficulty",2);
			GameObject.Find("Easy").collider.enabled = true;
			GameObject.Find("Easy").renderer.material.color = Color.red;
			GameObject.Find("Difficult").collider.enabled = true;
			GameObject.Find("Difficult").renderer.material.color = Color.red;
		}
		else if(isDifficult){
			renderer.material.color = Color.cyan;
			collider.enabled = false;
			PlayerPrefs.SetInt("Difficulty",3);
			GameObject.Find("Easy").collider.enabled = true;
			GameObject.Find("Easy").renderer.material.color = Color.red;
			GameObject.Find("Normal").collider.enabled = true;
			GameObject.Find("Normal").renderer.material.color = Color.red;
		}
		GameObject.Find("Save").collider.enabled = true;
	}
}
