using UnityEngine;
using System.Collections;

public class InitChooseTeam : MonoBehaviour {
	
	// Use this for initialization
	void Start(){
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;
		
		GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
		foreach(GameObject b in philoButtons){
			b.collider.enabled = false;
		}
		
		GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
		foreach(GameObject b in droidButtons){
			b.collider.enabled = false;
		}
		
		GameObject.Find ("StartButton").collider.enabled = false;
	}
	
	// Update is called once per frame
	
}
