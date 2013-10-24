using UnityEngine;
using System.Collections;

public class PhiloChickenChoose : MonoBehaviour {

	public bool isChicken1 = false, isChicken2 = false, isChicken3 = false, isChicken4 = false, isChicken5 = false;
	public Material[] Materials;
	
	public void OnMouseEnter(){		
		renderer.material = Materials[1];	
	}

	public void OnMouseExit(){		
		if(!collider.enabled)
			renderer.material = Materials[2];
		else
			renderer.material = Materials[0];
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
		int i = 3;
		foreach(GameObject b in droidButtons){
			if(!b.gameObject.Equals(this.gameObject)){
				b.renderer.material = Materials[i];
				b.collider.enabled = true;
				i++;				
			}			
		}				
		GameObject.Find("StartButton").collider.enabled = true;
		GameObject.Find("StartButton").renderer.material = Materials[7];
	}
}
