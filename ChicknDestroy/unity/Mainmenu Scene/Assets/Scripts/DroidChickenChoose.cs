using UnityEngine;
using System.Collections;

/*
 * This script controls the robotChicken choice buttons
 * */

public class DroidChickenChoose : MonoBehaviour {

	public bool isChicken1 = false, isChicken2 = false, isChicken3 = false, isChicken4 = false, isChicken5 = false;//booleans referenced to robot chicken choice buttons
	
	
	
	public void OnMouseEnter(){//hover state
		if(isChicken1)
			renderer.material.color = Color.blue;
		else if(isChicken2)
			renderer.material.color = Color.blue;
		else if(isChicken3)
			renderer.material.color = Color.blue;
		else if(isChicken4)
			renderer.material.color = Color.blue;
		else if(isChicken5)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){//when mause stops hovering
		if(isChicken1){
			if(!collider.enabled)//if button has been selected
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
		else if(isChicken2){
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
		else if(isChicken3){
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
		else if(isChicken4){
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
		else if(isChicken5){
			if(!collider.enabled)
				renderer.material.color = Color.cyan;
			else
				renderer.material.color = Color.red;
		}
	}
	
	public void OnMouseUpAsButton(){//Selecting a button
		
		if(isChicken1){//when selected a button we disable the collider of the selected button and set the possible previous selected buttons enabling the colider and making it visible
			PlayerPrefs.SetInt("Chicken",1);//this will be the global var to know which chicken was selected
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){//setting the other button to unselected state
				if(!b.gameObject.Equals(this.gameObject)){
					b.renderer.material.color = Color.red;
					b.collider.enabled = true;
				}
			}
			
		}else if(isChicken2){
			PlayerPrefs.SetInt("Chicken",2);
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				if(!b.gameObject.Equals(this.gameObject)){
					b.renderer.material.color = Color.red;
					b.collider.enabled = true;
				}
			}
		}else if(isChicken3){
			PlayerPrefs.SetInt("Chicken",3);
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				if(!b.gameObject.Equals(this.gameObject)){
					b.renderer.material.color = Color.red;
					b.collider.enabled = true;
				}
			}
		}else if(isChicken4){
			PlayerPrefs.SetInt("Chicken",4);
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				if(!b.gameObject.Equals(this.gameObject)){
					b.renderer.material.color = Color.red;
					b.collider.enabled = true;
				}
			}
		}else if(isChicken5){
			PlayerPrefs.SetInt("Chicken",5);
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				if(!b.gameObject.Equals(this.gameObject)){
					b.renderer.material.color = Color.red;
					b.collider.enabled = true;
				}
			}
		}
		GameObject.Find("StartButton").collider.enabled = true;
	}
}
