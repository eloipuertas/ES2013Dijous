using UnityEngine;
using System.Collections;

public class HUDs : MonoBehaviour {
	private PlayerInterface player;
	private GameInterface game;
	
	private GUISkin skin;
	
	// Game HUD
	private Vector2 points;
	private Vector2 pause;
	
	Rect rhealth;
	Rect rpweapon;
	Rect rsweapon;
	
	Rect rpause;
	Rect rpoints;
	void initComponents() {
		this.rhealth = new Rect(10,10,100,10);
		this.rpweapon = new Rect(rhealth.xMax+10, rhealth.yMin, 100, 10);
		this.rsweapon = new Rect(rpweapon.xMax+10,rhealth.yMin, 100, 10);
		
		this.rpause = new Rect(700,rhealth.yMin,50,10);
		this.rpoints = new Rect(rpause.xMax/2,rhealth.yMin,100,10);
	}
	
	// Use this for initialization
	void Start () {
		this.skin = new GUISkin();
		
		initComponents();
		
		
		this.player = new PlayerInterface();
		this.game = new GameInterface();
	}
	
	void OnGUI() {
		GUI.skin = this.skin;
		// Player Information
		GUI.Label(rhealth,player.getHealthPoints().ToString());
		GUI.Label(rpweapon,player.getPrimaryWeapon());
		GUI.Label(rsweapon,player.getSecondaryWeapon());
		
		GUI.Label(rpoints,game.getPoints().ToString());
		if(GUI.Button(rpause, "Pause")) {
			pauseMenu();
		}
	}
	
	// Update is called once per frame
	void Update () {
		//GUI.Label(rhealth,player.getHealthPoints().ToString());
	}
	
	void pauseMenu() {
		// show pause menu.
		this.player.setHealthPoints(this.player.getHealthPoints()+10);
		print ("Pause!!");
	}
}
