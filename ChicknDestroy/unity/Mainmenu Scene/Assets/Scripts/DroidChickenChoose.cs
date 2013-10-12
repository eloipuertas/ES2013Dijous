using UnityEngine;
using System.Collections;

public class DroidChickenChoose : MonoBehaviour {

	public bool isChicken1 = false, isChicken2 = false, isChicken3 = false, isChicken4 = false, isChicken5 = false;
	
	

	
	public void OnMouseEnter(){
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

	public void OnMouseExit(){
		if(isChicken1){
			if(!collider.enabled)
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
	
	public void OnMouseUpAsButton(){
		
		if(isChicken1){
			PlayerPrefs.SetInt("Chicken",1);
			collider.enabled = false;
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
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
