using UnityEngine;
using System.Collections;

/*
 * This script controls the team choice buttons
 * */

public class TeamChoose : MonoBehaviour {
	
	public bool isPhilo = false, isDroid = false;//booleans referenced to team choice buttons
	
	

	
	public void OnMouseEnter(){//hover state
		if(isPhilo)
			renderer.material.color = Color.blue;
		else if(isDroid)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){//when mause stops hovering
		if(isPhilo){
			if(!collider.enabled)//if button has been selected
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
		else if(isDroid){
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
	}
	
	public void OnMouseUpAsButton(){//Selecting a button
		
		if(isPhilo){//when selected a button we disable the collider of the selected button and set the possible previous selected buttons enabling the colider and making it visible
			PlayerPrefs.SetInt("Team",1);//this will be the global var to know which team was selected
			GameObject.Find("DroidButton").renderer.material.color = Color.red;
			GameObject.Find("DroidButton").collider.enabled = true;
			collider.enabled = false;
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
			foreach(GameObject b in philoButtons){//enabling Philo chicken buttons
				b.collider.enabled = true;
				b.renderer.material.color = Color.red;
			}
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){//Disabling robot chicken buttons
				b.collider.enabled = false;
				b.renderer.material.color = Color.gray;
			}
			
		}else if(isDroid){
			PlayerPrefs.SetInt("Team",2);
			GameObject.Find("PhiloButton").renderer.material.color = Color.red;
			GameObject.Find("PhiloButton").collider.enabled = true;
			collider.enabled = false;
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){//enabling robot chicken buttons
				b.collider.enabled = true;
				b.renderer.material.color = Color.red;
			}
			
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
			foreach(GameObject b in philoButtons){//disabling philo chicken buttons
				b.collider.enabled = false;
				b.renderer.material.color = Color.gray;
			}
		}
		GameObject.Find("StartButton").collider.enabled = false;//just in case StarButton must be always disabled when we select a team
	}
}
