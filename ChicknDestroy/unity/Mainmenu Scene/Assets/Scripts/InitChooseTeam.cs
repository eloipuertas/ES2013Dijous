using UnityEngine;
using System.Collections;

/*
 * This script executes with the TeamChoose Scene's init
 * */

public class InitChooseTeam : MonoBehaviour {
	
	// Use this for initialization
	void Start(){
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;
		
		GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
		foreach(GameObject b in philoButtons){//Disable chicken choice buttons
			b.collider.enabled = false;
			b.renderer.material.color = Color.gray;
		}
		
		GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
		foreach(GameObject b in droidButtons){
			b.collider.enabled = false;
			b.renderer.material.color = Color.gray;
		}
		
		GameObject.Find ("StartButton").collider.enabled = false;//disabling the stargame button
	}
	
	// Update is called once per frame
	
}
