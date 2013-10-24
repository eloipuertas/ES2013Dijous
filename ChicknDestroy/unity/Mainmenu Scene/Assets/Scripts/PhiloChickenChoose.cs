using UnityEngine;
using System.Collections;

public class PhiloChickenChoose : MonoBehaviour {

	public bool isChicken1 = false, isChicken2 = false, isChicken3 = false, isChicken4 = false, isChicken5 = false;
	public Material[] Materials;
	
	public void OnMouseEnter(){		
		renderer.material = Materials[2];	
	}

	public void OnMouseExit(){		
		if(!collider.enabled)
			renderer.material = Materials[3];
		else
			renderer.material = Materials[1];
	}
	
	public void OnMouseUpAsButton(){
		collider.enabled = false;
		if(isChicken1)
			PlayerPrefs.SetInt("Chicken",1);
		else if(isChicken2)
			PlayerPrefs.SetInt("Chicken",2);
		else if(isChicken3)
			PlayerPrefs.SetInt("Chicken",3);
		else if(isChicken4)
			PlayerPrefs.SetInt("Chicken",4);
		else if(isChicken5)
			PlayerPrefs.SetInt("Chicken",5);	
					
		GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("philobuttons");
		foreach(GameObject b in droidButtons){
			if(!b.gameObject.Equals(this.gameObject)){
				PhiloChickenChoose aux = (PhiloChickenChoose)b.GetComponent("PhiloChickenChoose");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
				b.renderer.material = aux.Materials[1];
				b.collider.enabled = true;
				
			}			
		}
		
		StartOrBack aux2 = (StartOrBack)GameObject.Find("StartButton").GetComponent("StartOrBack");//GetComponent("componentname") gets the component with its name, at this line we are getting the Script component of the game object so we can cast it to the class inside this script and access at its global variables
		GameObject.Find("StartButton").collider.enabled = true;
		GameObject.Find("StartButton").renderer.material = aux2.Materials[0];
	}
	
}
