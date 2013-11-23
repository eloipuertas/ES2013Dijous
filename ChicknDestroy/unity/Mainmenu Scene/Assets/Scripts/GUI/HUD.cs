﻿using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour{
	private static int MESSAGE_POOL = 10;
	
	private bool current_player_robotic;
	private string team;
	
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
	private Lifebar shield;
	private Sprite bg_shield;
	
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
		
		this.current_player_robotic = true;
		
		if (this.current_player_robotic == true) this.team = "robot";
		else this.team = "philo";
		
		int isegment = Screen.width/10;
		
		this.weapons = new SpriteWeaponControl(new Rect(10,10,100,40));
		
		this.life = new Lifebar(new Vector2((Screen.width/2)-180,20),
			new Vector2(200,60),
			"LifeBar/life_bar_",
			new Vector2(0,0),
			21, // n-segments of LifeBar
			100, // Max life
			100); // Current life
		this.bg_life = new Sprite(new Rect(this.life.getXY().x-10,this.life.getXY().y-13,
			this.life.getSize().x+20,this.life.getSize().y+10),"LifeBar/hudVida");
		
		this.shield = new Lifebar(new Vector2(this.life.getXY().x+(this.life.getSize().x+20),this.life.getXY ().y-7),
			new Vector2(100,100),
			"escudo/escudo",
			new Vector2(0,0),
			10, // n-Images of the Shield
			100, // Barrier max points
			0); // barrier current points.
		this.bg_shield = new Sprite(new Rect(this.shield.getXY ().x-5,this.shield.getXY ().y-5,
			this.shield.getSize ().x+10,this.shield.getSize ().y+10),"escudo/hudEscudo");
		
		this.pause_menu = new PauseMenu(this,
			new Vector2((Screen.width/2)-isegment+10,Screen.height/5), // Origin
			new Vector2((Screen.width/2)+2*isegment,(int)(Screen.height*3.5)/5), // end
			new Vector2(0,50),	// Deviation
			"pausa/pauseMenu"+this.team, // Menu layout
			this.team); // Team
		
		this.game_points = new Points(new Vector2(this.bg_shield.getXY().x+this.bg_shield.getSize ().x+20,20),
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
		this.skin = new GUISkin();
		
		initComponents();
		
		notifyFlag (false,false);
		
	}
	
	void OnGUI() {
		GUI.skin = this.skin;
		this.bg_life.render();
		this.bg_shield.render ();
		
		// Player Information
		this.weapons.render ();
		
		this.life.render();
		this.shield.render ();
		this.game_points.render(this.player.getPlayerPoints());
		
		this.pause_button.render();
		
		this.mes.render();
		
		//if(isPaused())this.pause_menu.render();
		if(isPaused())pauseMenu();
	}
	
	// Update is called once per frame
	void Update () {
		//GUI.Label(rhealth,player.getHealthPoints().ToString());
	}
	private void pauseMenu() {
		this.pause_menu.setVisible(true);
		this.pause_menu.render();
	}
	
	public void resume() {
		this.pause_menu.setVisible (false);
		this.pause_button.setToggled (false);
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
	
	// Notify section.
	/**
	 * Notify the new HP and update the lifebar (with the current value of HP).
	 */
	public void notifyHealthChange(int hp) {
		this.life.setLife(hp);
	}
	
	public void notifyPrimaryWeapon(int weapon) {
		switch(weapon) {
			case 1:
				this.weapons.notifyPrimaryWeapon("katana");
				break;
			case 2:
				this.weapons.notifyPrimaryWeapon("escopeta");
				break;
			case 3:
				this.weapons.notifyPrimaryWeapon("revolver");
				break;
			case 4:
				this.weapons.notifyPrimaryWeapon("metralleta");
				break;
			case 5:
				this.weapons.notifyPrimaryWeapon("carpeta");
				break;
			case 6:
				this.weapons.notifyPrimaryWeapon("rifle");
				break;
		}
		//this.weapons.notifyPrimaryWeapon(weapon);
	}
	
	public void notifySecondaryWeapon(int weapon) {
		switch(weapon) {
			case 1:
			this.weapons.notifySecondaryWeapon("granada");
				break;
			case 2:
				this.weapons.notifySecondaryWeapon("mina");
				break;
		}
		//this.weapons.notifySecondaryWeapon(weapon);
	}
	public void notifyMessage(Vector2 position, string message) {
		this.mes.start(position,message,millis);
	}
	public void notifyFlag(bool flag, bool robotic) {
		if (robotic)this.robotic = flag;
		else this.philo = flag;
	}
}
