﻿using UnityEngine;
using System.Collections;

public class PlayerControllerKatana : MonoBehaviour {

	//Atributos de personaje
	private int health;
	private GameObject primaryWeapon;
	private GameObject secondaryWeapon;
	
	//Atributos de control
	private float gravity = 800;
	private float speed = 250;
	private float acceleration = 2000;
	private float jumpHeight = 500;
	
	//sonido
	public AudioSource sonidoSalto;
	public AudioSource sonidoDisparo;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	
	
	private int movDer = 1;
	private int movIzq = 2;
	private int stopDer = 3;
	private int stopIzq = 4;
	private bool disparo= false;

	private int lastDirection;
	
	//private PlayerPhysics playerPhysics;
	
	private HUD hud;
	private GameManager gameManager;
	
	void Start () {
		//playerPhysics = GetComponent<PlayerPhysics>();
		animation.Play("paradaDerecha");
		lastDirection = stopDer;
		
		
		gravity = 800;
		speed = 250;
		acceleration = 1000;
		jumpHeight = 500;
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		health = 100;
		fireHealthNotification();
	}
	
	void Update () {
		
		float raw = Input.GetAxisRaw("Horizontal");
		targetSpeed = raw * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
		/*
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) {
				sonidoSalto.Play();
				amountToMove.y = jumpHeight;	
			}
			
			if (Input.GetButtonDown("Fire1")) {
				
				disparo = true;
				sonidoDisparo.Play();
			}
		}
		*/
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= (gravity * Time.deltaTime);
		//playerPhysics.Move(amountToMove * Time.deltaTime);
		
		//Debug.Log("x: "+amountToMove.x+" y: "+amountToMove.y);
		
		if(amountToMove.y > 50) {
		//Si estamos en el aire de subida
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("saltoVerticalDer");
			else animation.Play("saltoVerticalIz");
		}else if(amountToMove.y < -50){
		//Si estamos en el aire de bajada
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("caidaDerecha");
			else animation.Play("caidaIzquierda");
		}else {
		//Si estamos en el suelo
			if (disparo == true && lastDirection == movDer){
						
						
						animation.Play("golpearkatanaDer");
						
						disparo = false;
						
				
			}
			else if (disparo == true && lastDirection == movIzq){
						
						animation.Play("golpearkatanaIzq");
								
						disparo = false;
						
				
			}
			if (lastDirection == movDer ) {
				if (amountToMove.x > 1) {
					animation.Play("correrDerecha");
					lastDirection = movDer;
					
				}
				else if(raw < 0) {
					animation.Play("giroDerechaIzq", PlayMode.StopAll);
					lastDirection = movIzq;
				}
				
				else {
					animation.Play("paradaDerecha");
					lastDirection = stopDer;
				}
				
			}else if (lastDirection == movIzq) {
				if (amountToMove.x < -1) {
					animation.Play("correrIzquierda");
					lastDirection = movIzq;
				}
				else if(raw > 0) {
					animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
					lastDirection = movDer;
				}
				else {
					
					animation.Play("paradaIzquierda");
					lastDirection = stopIzq;
				}
			}else {
				
				if (lastDirection == stopDer) {
					
					if(disparo == true){
						animation["golpearkatanaDer"].speed = (float) 2;
					 	animation.Play("golpearkatanaDer");
						disparo = false;
					}
					else if (amountToMove.x > 1) {
						animation.Play("correrDerecha");
						lastDirection = movDer;
					}
					else if(raw < 0) {
						animation.Play("giroDerechaIzq", PlayMode.StopAll);
						lastDirection = movIzq;
					}
					else {
						    
							//animation.Play("paradaDerecha");
							//lastDirection = stopDer;
								
						
					}
				}
				else {
					if(disparo == true){
						Debug.Log("ENTRO AQUIIIII");
						animation["golpearkatanaIzq"].speed = (float) 2;
					 	animation.Play("golpearkatanaIzq");
						disparo = false;
					}
					else if(amountToMove.x < -1) {
						animation.Play("correrIzquierda");
						lastDirection = movIzq;
					}else if(raw > 0) {
						animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
						lastDirection = movDer;
					}else {
							
							//animation.Play("paradaIzquierda");
							
							
						  //lastDirection = stopIzq;
					}
				}
				
				
			}
		}
		
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
	
	public int getHealthPoints() {
		return health;
	}
	
	public void setHealthPoints(int n) {
		health = n;
		fireHealthNotification();
		if (health <= 0) {
			fireDeathNotification();
		}
	}
	
	
	public string getPrimaryWeapon() {
		return primaryWeapon.ToString();
	}
	
	public string getSecondaryWeapon() {
		return secondaryWeapon.ToString();
	}
	
	public Vector3 getCoordinates() {
		return transform.position;
	}
	
	public void setDamage(int damage) {
		if ((health - damage) > 0) 
			setHealthPoints(health - damage);
		else 
			setHealthPoints(0);
	}
	private void fireHealthNotification() {
		this.hud.notifyHealthChange(this.health);
	}
	private void fireDeathNotification() {
		this.gameManager.notifyPlayerDeath();
		
	}
	private IEnumerator wait(){
		yield return new WaitForSeconds(-10);
	}
}

