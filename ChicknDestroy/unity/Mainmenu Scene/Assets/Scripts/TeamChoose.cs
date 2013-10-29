using UnityEngine;
using System.Collections;


public class TeamChoose : MonoBehaviour {
	
	public bool isPhilo = false, isDroid = false;//booleans referenced to team choice buttons
	public Material[] Materials;	
	
	public void OnMouseEnter(){//hover state		
		renderer.material = Materials[1];			
	}

	public void OnMouseExit(){//when mause stops hovering		
		if(!collider.enabled)//if button has been selected
			renderer.material = Materials[1];	
		else
			renderer.material = Materials[0];			
	}
	
	public void OnMouseUpAsButton(){//Selecting a button
		
		if(isPhilo){//when selected a button we disable the collider of the selected button and set the possible previous selected buttons enabling the colider and making it visible
			PlayerPrefs.SetInt("Team",1);//this will be the global var to know which team was selected
			GameObject.Find("DroidButton").renderer.material = Materials[2];
			GameObject.Find("DroidButton").collider.enabled = true;
			collider.enabled = false;
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");

			foreach(GameObject b in philoButtons){//enabling Philo chicken buttons
				b.collider.enabled = true;
				PhiloChickenChoose aux = (PhiloChickenChoose)b.GetComponent("PhiloChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[1];
			}
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){//Disabling robot chicken buttons
				b.collider.enabled = false;
				DroidChickenChoose aux = (DroidChickenChoose)b.GetComponent("DroidChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[0];
			}
			
		}else if(isDroid){
			PlayerPrefs.SetInt("Team",2);
			GameObject.Find("PhiloButton").renderer.material = Materials[2];
			GameObject.Find("PhiloButton").collider.enabled = true;
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){//enabling robot chicken buttons
				b.collider.enabled = true;
				DroidChickenChoose aux = (DroidChickenChoose)b.GetComponent("DroidChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[1];
			}
			
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
			foreach(GameObject b in philoButtons){//disabling philo chicken buttons
				b.collider.enabled = false;
				PhiloChickenChoose aux = (PhiloChickenChoose)b.GetComponent("PhiloChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[0];
			}
		}
		StartOrBack aux2 = (StartOrBack)GameObject.Find("StartButton").GetComponent("StartOrBack");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
		GameObject.Find("StartButton").collider.enabled = false;//just in case StarButton must be always disabled when we select a team
		GameObject.Find("StartButton").renderer.material = aux2.Materials[2];
	}
}
