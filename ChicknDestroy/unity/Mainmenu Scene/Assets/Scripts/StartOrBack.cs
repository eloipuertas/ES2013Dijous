using UnityEngine;
using System.Collections;

public class StartOrBack : MonoBehaviour {
	
	public bool isBack = false, isStart = false;
	
	
	public void OnMouseEnter(){
		if(isBack)
			renderer.material.color = Color.blue;
		else if(isStart)
			renderer.material.color = Color.blue;
		
	}

	public void OnMouseExit(){
		if(isBack){
				renderer.material.color = Color.red;
		}
		else if(isStart){
				renderer.material.color = Color.red;
		}
	}
	
	public void OnMouseUpAsButton(){
		
		if(isBack)
			Application.LoadLevel(0);
		if(isStart){
			PlayerPrefs.SetFloat("PosX",0);
			PlayerPrefs.SetFloat("PosY",0);
			PlayerPrefs.SetFloat("PosZ",0);
			Application.LoadLevel(4);
		}
	}
	
}
