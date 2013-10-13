using UnityEngine;
using System.Collections;

public class Difficulty : MonoBehaviour {
	
	public bool isEasy = false, isNormal = false, isDifficult = false;

	public void OnMouseEnter(){
		if(isEasy)
			renderer.material.color = Color.blue;
		else if(isNormal)
			renderer.material.color = Color.blue;
		else if(isDifficult)
			renderer.material.color = Color.blue;
	}

	public void OnMouseExit(){
		if(isEasy)
			if(!collider.enabled)
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
	
	public void OnMouseUpAsButton(){
		
		if(isEasy){
			renderer.material.color = Color.cyan;
			collider.enabled = false;
			PlayerPrefs.SetInt("Difficulty",1);
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
