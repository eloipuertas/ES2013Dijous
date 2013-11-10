using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	//Atributos de personaje
	private int health;
	private GameObject primaryWeapon;
	private GameObject secondaryWeapon;
	
	//Atributos de control
	public float gravity = 40;
	public float speed = 30;
	public float acceleration = 30;
	public float jumpHeight = 60;
	
	//sonido
	public AudioSource sonidoSalto;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	
	
	private int movDer = 1;
	private int movIzq = 2;
	private int stopDer = 3;
	private int stopIzq = 4;

	private int lastDirection;
	
	private PlayerPhysics playerPhysics;
	
	private HUD hud;
	private GameManager gameManager;
	
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		try{
			animation.Play("paradaDerecha");
		}catch{
			//print ("Exeption Parada Derecha: Null pointer animation");
		}
		lastDirection = stopDer;
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		health = 100;
		fireHealthNotification();
	}
	
	void Update () {
		float raw = Input.GetAxisRaw("Horizontal");
		targetSpeed = raw * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
		
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) {
				sonidoSalto.Play();
				amountToMove.y = jumpHeight;	
			}
		}
		
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);
		
		//Debug.Log("x "+raw);
		
		if(amountToMove.y > 1) {
		//Si estamos en el aire de subida
			if(lastDirection == movDer || lastDirection == stopDer){
				try{
					animation.Play("saltoVerticalDer");
				}catch{
					//print ("Exeption Salto vertical Izq: Null Pointer animation");
				}
			}
			else {
				try{
					animation.Play("saltoVerticalIz");
				}catch{
					//print ("Exeption Salto vertical Izq: Null Pointer animation");
				}
			}
		}else if(amountToMove.y < -1){
		//Si estamos en el aire de bajada
			if(lastDirection == movDer || lastDirection == stopDer){
				try{
						animation.Play("caidaDerecha");
				}catch{
						//print ("Exeption caidaDerecha Izq: Null Pointer animation");
				}
			}
			else{
				try{
						animation.Play("caidaIzquierda");
				}catch{
						//print ("Exeption caidaIzquierda Izq: Null Pointer animation");
				}
			}
		}else {
		//Si estamos en el suelo
			if (lastDirection == movDer ) {
				if (amountToMove.x > 1) {
					try{
						animation.Play("correrDerecha");
					}catch{
						//print ("Exeption correrDerecha Izq: Null Pointer animation");
					}
					lastDirection = movDer;
				}
				else if(raw < 0) {
					try{
						animation.Play("giroDerechaIzq", PlayMode.StopAll);
					}catch{
						//print ("Exeption giroDerechaIzq Izq: Null Pointer animation");
					}
					lastDirection = movIzq;
				}
				else {
					try{
						animation.Play("paradaDerecha");
					}catch{
						//print ("Exeption paradaDerecha Izq: Null Pointer animation");
					}
					lastDirection = stopDer;
				}
			}else if (lastDirection == movIzq) {
				if (amountToMove.x < -1) {
					try{
						animation.Play("correrIzquierda");
					}catch{
						//print ("Exeption correrIzquierda Izq: Null Pointer animation");
					}
					
					lastDirection = movIzq;
				}
				else if(raw > 0) {
					try{
						animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
					}catch{
						//print ("Exeption giroIzquierdaDerecha Izq: Null Pointer animation");
					}
					lastDirection = movDer;
				}
				else {
					try{
						animation.Play("paradaIzquierda");
					}catch{
						//print ("Exeption giroIzquierdaDerecha Izq: Null Pointer animation");
					}
					lastDirection = stopIzq;
				}
			}else {
				
				if (lastDirection == stopDer) {
					if (amountToMove.x > 1) {
						try{
							animation.Play("correrDerecha");
						}catch{
							//print ("Exeption correrDerecha Izq: Null Pointer animation");
						}
						lastDirection = movDer;
					}else if(raw < 0) {
						try{
							animation.Play("giroDerechaIzq", PlayMode.StopAll);
						}catch{
							//print ("Exeption giroDerechaIzq Izq: Null Pointer animation");
						}
						lastDirection = movIzq;
					}
					else {
						try{
							animation.Play("paradaDerecha");
						}catch{
							//print ("Exeption paradaDerecha Izq: Null Pointer animation");
						}

						lastDirection = stopDer;
					}
				}
				else {
					if(amountToMove.x < -1) {
						try{
							animation.Play("correrIzquierda");
						}catch{
							//print ("Exeption correrIzquierda Izq: Null Pointer animation");
						}	
						lastDirection = movIzq;
					}else if(raw > 0) {
						try{
							animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
						}catch{
							//print ("Exeption giroIzquierdaDerecha Izq: Null Pointer animation");
						}						
						lastDirection = movDer;
					}else {
						try{
							animation.Play("paradaIzquierda");
						}catch{
							//print ("Exeption paradaIzquierda Izq: Null Pointer animation");
						}
						lastDirection = stopIzq;
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
}
