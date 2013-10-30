﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour{
	private bool paused;
	
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
	
	// Definitive Objects
	private Lifebar life;
	private PauseMenu pause_menu;
	private Points game_points;

	void initComponents() {
		this.paused = false;
		this.rhealth = new Rect(10,10,100,10);
		this.rpweapon = new Rect(rhealth.xMax+10, rhealth.yMin, 100, 10);
		this.rsweapon = new Rect(rpweapon.xMax+10,rhealth.yMin, 100, 10);
		
		this.rpause = new Rect(700,rhealth.yMin,50,10);
		this.rpoints = new Rect(rpause.xMax/2,rhealth.yMin,100,10);
		
		
		this.life = new Lifebar(new Vector2(10,10),
			new Vector2(100,20),
			"LifeBar/life_bar_",
			new Vector2(0,0),
			21, // n-segments of LifeBar
			150, // Max life with the barrier
			0); // Current life
		
		this.pause_menu = new PauseMenu(new Vector2(100,40), // Origin
			new Vector2(500,500), // end
			"PauseMenu", // Menu layout
			"titulo", // Menu Title
			"button_"); // Button pattern.
		
		this.game_points = new Points(new Vector2(350,10),
			new Vector2(30,30),
			"Numbers/",
			new Vector2(15,0),
			10);
	}
	
	public void notifyHealth(int hp) {
		this.life.setLife(hp);
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Starting HUD debug message");
		this.skin = new GUISkin();
		
		initComponents();
		
		
		this.player = new PlayerInterface();
		this.game = new GameInterface();
		
	}
	
	void OnGUI() {
		GUI.skin = this.skin;
		
		// Player Information
		//GUI.Label(rhealth,player.getHealthPoints().ToString());
		GUI.Label(rpweapon,player.getPrimaryWeapon());
		GUI.Label(rsweapon,player.getSecondaryWeapon());
		
		this.life.render();
		this.game_points.render(this.player.getPlayerPoints());
		
		//GUI.Label(rpoints,game.getPoints().ToString());
		if(GUI.Button(rpause, "Pause")) {
			//this.paused = true;
			//this.pause_menu.setVisible(true);
			stub_pauseMenu();
		}
		if(this.paused) pauseMenu();
	}
	
	// Update is called once per frame
	void Update () {
		//GUI.Label(rhealth,player.getHealthPoints().ToString());
	}
	
	void pauseMenu() {
		this.paused = this.pause_menu.isVisible();
		this.pause_menu.render();
	}
	
	void stub_pauseMenu() {
		// show pause menu.
		this.player.setHealthPoints(this.player.getHealthPoints()+10);
		print ("Pause!!");
		print("Current Health: "+this.player.getHealthPoints().ToString());
		this.life.setLife(this.player.getHealthPoints());
		this.player.setPlayerPoints(this.player.getPlayerPoints()+10);
	}
	
	public bool isPaused() {
		return paused;
	}
	public void setPaused(bool paused) {
		this.paused = paused;
	}
}
