using UnityEngine;
using System.Collections;

/*
 * This script Controls the start and back button in TeamCHoose scene
 * */

public class StartOrBack : MonoBehaviour {
	
	public bool isBack = false, isStart = false;//referenced to the buttons
	public Material[] Materials;
	private int entrar = 0;
	public AudioSource audioEnrera;
	public AudioSource audioOpcions;
	public AudioSource audioAcceptar;
	
	public void OnMouseEnter(){//hovering
		renderer.material = Materials[1];
		audioOpcions.Play();
	}

	public void OnMouseExit(){//leaving hover
		renderer.material = Materials[0];
	}
	
	public void OnMouseUpAsButton(){//selecting
		
		if(isBack) {
			audioEnrera.Play();
			Application.LoadLevel(0);
		}
		if(isStart){//if start we set the variables we want to use in the game
			audioAcceptar.Play();
			PlayerPrefs.SetFloat("PosX",0);
			PlayerPrefs.SetFloat("PosY",0);
			PlayerPrefs.SetFloat("PosZ",0);
			Application.LoadLevel(2);
		}
	}
	
}
