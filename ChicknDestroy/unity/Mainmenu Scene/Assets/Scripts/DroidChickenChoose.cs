using UnityEngine;
using System.Collections;

/*
 * This script controls the robotChicken choice buttons
 * */

public class DroidChickenChoose : MonoBehaviour {

	public bool isChicken1 = false, isChicken2 = false, isChicken3 = false, isChicken4 = false, isChicken5 = false;//booleans referenced to robot chicken choice buttons	
	public Material[] Materials;
	
	public void OnMouseEnter(){//hover state		
		renderer.material = Materials[2];		
	}

	public void OnMouseExit(){//when mause stops hovering		
		if(!collider.enabled)//if button has been selected
			renderer.material = Materials[3];
		else
			renderer.material = Materials[1];
	}
	
	public void OnMouseUpAsButton(){//Selecting a button
		collider.enabled = false;
		
		if(isChicken1)//when selected a button we disable the collider of the selected button and set the possible previous selected buttons enabling the colider and making it visible
			PlayerPrefs.SetInt("Chicken",1);//this will be the global var to know which chicken was selected
		else if(isChicken2)
			PlayerPrefs.SetInt("Chicken",2);			
		else if(isChicken3)
			PlayerPrefs.SetInt("Chicken",3);			
		else if(isChicken4)
			PlayerPrefs.SetInt("Chicken",4);		
		else if(isChicken5)
			PlayerPrefs.SetInt("Chicken",5);	
				

		GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
		foreach(GameObject b in droidButtons){//setting the other button to unselected state
			if(!b.gameObject.Equals(this.gameObject)){
				DroidChickenChoose aux = (DroidChickenChoose)b.GetComponent("DroidChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[1];
				b.collider.enabled = true;

			}
		}		
		StartOrBack aux2 = (StartOrBack)GameObject.Find("StartButton").GetComponent("StartOrBack");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
		GameObject.Find("StartButton").collider.enabled = true;
		GameObject.Find("StartButton").renderer.material = aux2.Materials[0];
	}
}
