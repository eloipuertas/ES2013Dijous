﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour{
	private static int MESSAGE_POOL = 10;
	
	private PlayerInterface player;
	private GameInterface game;
	
	private GUISkin skin;
	
	// Game HUD
	private Vector2 points;
	private Vector2 pause;
	
	// Definitive Objects
	private static double millis = 2000;
	
	private Lifebar life;
	private Sprite bg_life;
	
	private PauseMenu pause_menu;
	private Points game_points;
	
	private SpriteWeaponControl weapons;
	
	private SpriteButton pause_button;
	
	private MessagePool mes;
	
	// Flag Control
	private bool robotic;
	private bool philo;

	void initComponents() {
		this.robotic = false;
		this.philo = false;
		
		int isegment = Screen.width/10;
		
		this.weapons = new SpriteWeaponControl(new Rect(10,10,100,40));
		
		this.life = new Lifebar(new Vector2((Screen.width/2)-100,20),
			new Vector2(200,60),
			"LifeBar/life_bar_",
			new Vector2(0,0),
			21, // n-segments of LifeBar
			150, // Max life with the barrier
			0); // Current life
		this.bg_life = new Sprite(new Rect(this.life.getXY().x-10,this.life.getXY().y-13,
			this.life.getSize().x+20,this.life.getSize().y+10),"LifeBar/hudVida");
		
		/*this.pause_menu = new PauseMenu(this,
			new Vector2(100,40), // Origin
			new Vector2(500,500), // end
			new Vector2(0,20),	// Deviation
			"PauseMenu", // Menu layout
			"titulo", // Menu Title
			"button_"); // Button pattern.*/
		
		this.game_points = new Points(new Vector2(this.life.getXY().x+this.life.getSize().x+isegment,20),
			new Vector2(20,20),
			"Numbers/",
			new Vector2(20,0),
			10);
		
		this.pause_button = new SpriteButton(new Rect((Screen.width)-100,20,100,20),
			"pausa/pause",
			"pausa/pause1",
			new PauseAction(),
			KeyCode.P);
		
		this.mes = new MessagePool(MESSAGE_POOL);
		
		this.player = new PlayerInterface();
		this.game = new GameInterface();
	}
	
	
	// Use this for initialization
	void Start () {
		Debug.Log ("Starting HUD debug message");
		this.skin = new GUISkin();
		
		initComponents();
		
		notifyFlag (false,false);
		
	}
	
	void OnGUI() {
		GUI.skin = this.skin;
		this.bg_life.render();
		
		// Player Information
		this.weapons.render ();
		
		this.life.render();
		this.game_points.render(this.player.getPlayerPoints());
		
		this.pause_button.render();
		
		this.mes.render();
		
		//if(isPaused())this.pause_menu.render();
		if(isPaused())stub_pauseMenu();
	}
	
	// Update is called once per frame
	void Update () {
		//GUI.Label(rhealth,player.getHealthPoints().ToString());
	}
	
	void stub_pauseMenu() {
		// show pause menu.
		this.player.setHealthPoints(this.player.getHealthPoints()+10);
		print ("Pause!!");
		print("Current Health: "+this.player.getHealthPoints().ToString());
		this.life.setLife(this.player.getHealthPoints());
		this.player.setPlayerPoints(this.player.getPlayerPoints()+10);
		this.pause_button.setToggled (false);
	}
	
	public bool isPaused() {
		return this.pause_button.isToggled();
	}
	
	public void resume() {
		this.pause_button.setToggled(false);
	}
	
	// Notify section.
	/**
	 * Notify the new HP and update the lifebar (with the current value of HP).
	 */
	public void notifyHealthChange(int hp) {
		this.life.setLife(hp);
	}
	
	public void notifyPrimaryWeapon(string weapon) {
		this.weapons.notifyPrimaryWeapon(weapon);
	}
	
	public void notifySecondaryWeapon(string weapon) {
		this.weapons.notifySecondaryWeapon(weapon);
	}
	public void notifyMessage(Vector2 position, string message) {
		this.mes.start(position,message,millis);
	}
	public void notifyFlag(bool flag, bool robotic) {
		if (robotic)this.robotic = flag;
		else this.philo = flag;
	}
}
