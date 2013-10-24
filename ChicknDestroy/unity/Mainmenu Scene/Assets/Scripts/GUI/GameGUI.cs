using UnityEngine;
using System.Collections;

public class GameGUI : MonoBehaviour{
	private HUD hud;
	private GUI gui;
	public GameGUI (){
		this.gui = new GUI();
		
		this.hud = new HUD();
	}
}

