using UnityEngine;
using System.Collections;

//MainMenuController.cs - This script will take control of the main menu options

public class MainMenuController: MonoBehaviour{
	
	//Variables
	public bool isQuit = false, isStart = false, isOptions = false, isLoad = false;			
	public Material[] Materials;
	public AudioSource audioOpcions;
	public AudioSource audioAcceptar;
	
	void Start(){
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;	
	}
	
	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		audioOpcions.Play();
		renderer.material = Materials[1];		
		
	}
	//This function is called when the mouse is not any longer over the GUIElement or Collider
	public void OnMouseExit(){
		renderer.material = Materials[0];
	}
	
	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){
		audioAcceptar.Play();
		if(isQuit)
			Application.Quit();//referenced to Quit Game button, quits program
		else if(isStart)
			Application.LoadLevel(1); //referenced to Start Game button, jump to start game scene
		else if(isLoad)
			Application.LoadLevel(2); //referenced to Load Game button, jump to Load game scene
		else if(isOptions)
			Application.LoadLevel(3); //referenced to Options button, jump to Options scene
	}
}