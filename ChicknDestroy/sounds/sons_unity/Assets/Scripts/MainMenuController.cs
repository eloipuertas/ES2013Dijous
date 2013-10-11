using UnityEngine;
using System.Collections;


public class MainMenuController: MonoBehaviour{
	
	//Variables
	public bool isQuitButton = false;
	public bool entrar;
	public AudioSource audioOpcions;
	public AudioSource audioAcceptar;
	public AudioSource audioTirarEnrera;
	
	public GameObject tirarEnrera;

	
	void Start(){
		//Should the cursor be visible?
		Screen.showCursor = true;
		//The cursor will automatically be hidden, centered on view and made to never leave the view.
		Screen.lockCursor = false;	
	}
	
	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		renderer.material.color = Color.blue;
		audio.Play();
		entrar = true;
		
	}
	//This function is called when the mouse is not any longer over the GUIElement or Collider
	public void OnMouseExit(){
		renderer.material.color = Color.white;
		entrar = false;
	}
	
	
	
	//This function is called when the user has released the mouse button
	public void OnMouseUpAsButton(){
		if(isQuitButton)
			Application.Quit();
		else if(entrar)
			audio.Play();
	}
}