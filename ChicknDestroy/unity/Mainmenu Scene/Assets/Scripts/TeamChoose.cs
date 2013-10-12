using UnityEngine;
using System.Collections;

public class TeamChoose : MonoBehaviour {
	
	public bool isPhilo = false, isDroid = false;
	
	

	
	public void OnMouseEnter(){
		if(isPhilo)
			renderer.material.color = Color.blue;
		else if(isDroid)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){
		if(isPhilo){
			if(!collider.enabled)
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
	
	public void OnMouseUpAsButton(){
		
		if(isPhilo){
			PlayerPrefs.SetInt("Team",1);
			GameObject.Find("DroidButton").renderer.material.color = Color.red;
			GameObject.Find("DroidButton").collider.enabled = true;
			collider.enabled = false;
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
			foreach(GameObject b in philoButtons){
				b.collider.enabled = true;
				b.renderer.material.color = Color.red;
			}
			
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				b.collider.enabled = false;
				b.renderer.material.color = Color.red;
			}
			
		}else if(isDroid){
			PlayerPrefs.SetInt("Team",2);
			GameObject.Find("PhiloButton").renderer.material.color = Color.red;
			GameObject.Find("PhiloButton").collider.enabled = true;
			collider.enabled = false;
			GameObject [] droidButtons = GameObject.FindGameObjectsWithTag("droidbuttons");
			foreach(GameObject b in droidButtons){
				b.collider.enabled = true;
			}
			
			GameObject [] philoButtons = GameObject.FindGameObjectsWithTag("philobuttons");
			foreach(GameObject b in philoButtons){
				b.collider.enabled = false;
				b.renderer.material.color = Color.red;
			}
		}
		GameObject.Find("StartButton").collider.enabled = false;
	}
}
