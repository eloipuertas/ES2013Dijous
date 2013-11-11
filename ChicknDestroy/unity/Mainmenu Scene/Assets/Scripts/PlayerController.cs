using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	//Atributos de personaje
	private int health;
	private GameObject primaryWeapon;
	private GameObject secondaryWeapon;
	
	//Atributos de control
	private float gravity = 800;
	private float speed = 300;
	private float jumpHeight = 500;
	private float acceleration = 50;
	
	//sonido
	public AudioSource sonidoSalto;
	public AudioSource sonidoDisparo;
	
	
	private float animTime;
	private float animDuration = 0.3f;
	
	private float currentSpeed;
	private float targetSpeed;
	
	
	private int movDer = 1;
	private int movIzq = 2;
	private int stopDer = 3;
	private int stopIzq = 4;
	private bool disparo= false;

	private int lastDirection;
	private float heightHero;
	
	private HUD hud;
	private GameManager gameManager;
	private Rigidbody rigid;
	
	
	
	void Start () {
		
		rigid =	GetComponent<Rigidbody>();
		
		animation.Play("paradaDerecha");
		lastDirection = stopDer;
		
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		health = 100;
		//fireHealthNotification();
		
		disparo = true;
		
		heightHero = rigid.collider.bounds.extents.y;
	}
	
	private bool isGround() {
		return Physics.Raycast(transform.position, -Vector3.up, heightHero + 0.1f);
	}
	
	void OnCollisionStay(Collision collision) {
		//TRATAMIENTO DE COLISIONES CON OBJETOS
	}
	
	void Update () {
		
		float raw = Input.GetAxisRaw("Horizontal");
		
		
		if (isGround()) {
			if (Input.GetButtonDown("Jump")) {
				sonidoSalto.Play();
				rigid.velocity += Vector3.up * jumpHeight;
			}
			
			if (!disparo && Input.GetButtonDown("Fire1")) {
				
				sonidoDisparo.Play();
				disparo = true;
				
			}
		}
		
		//Actualiza la posicion del personaje
		rigid.velocity = new Vector3((raw * speed * acceleration)*Time.deltaTime, rigid.velocity.y, 0);
		rigid.velocity += (Vector3.up * -gravity * Time.deltaTime);
	
		
		if(rigid.velocity.y > 10) {
		//Si estamos en el aire de subida
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("saltoVerticalDer");
			else animation.Play("saltoVerticalIz");
		}else if(rigid.velocity.y < -65){
		//Si estamos en el aire de bajada
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("caidaDerecha");
			else animation.Play("caidaIzquierda");
		}else {

			float currentTime = Time.time - animTime;
			
			if (lastDirection == movDer) {
				if (rigid.velocity.x > 0) {
					if(currentTime > animDuration){
						if(disparo) {
							animation["disparaEscopetaDerCorriendo"].speed = 3;
							animation.Play("disparaEscopetaDerCorriendo",PlayMode.StopAll);
							disparo = false;
						}
						else animation.Play("correrDerecha");
						animTime = Time.time;
					}
					lastDirection = movDer;
					
				}
				else if(raw < 0) {
					animation["giroDerechaIzq"].speed = 2;
					animation.Play("giroDerechaIzq", PlayMode.StopAll);
					animTime = Time.time;
					lastDirection = movIzq;
				}
				else {
					lastDirection = stopDer;
				}
				
			}else if (lastDirection == movIzq) {
				if (rigid.velocity.x < 0) {
					if(currentTime > animDuration){
						if(disparo) {
							animation["disparaEscopetaIzqCorriendo"].speed = 3;
							animation.Play("disparaEscopetaIzqCorriendo",PlayMode.StopAll);
							disparo = false;
						}
						else animation.Play("correrIzquierda");
						animTime = Time.time;
					}
					lastDirection = movIzq;
				}
				else if(raw > 0) {
					animation["giroIzquierdaDerecha"].speed = 2;
					animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
					animTime = Time.time;
					lastDirection = movDer;
				}
				else {
					lastDirection = stopIzq;
				}
			}else {
				if (lastDirection == stopDer) {
					if (disparo && currentTime > animDuration) {
						animation["disparaEscopetaDer"].speed = 3;
						animation.Play("disparaEscopetaDer",PlayMode.StopAll);
						disparo = false;
						animTime = Time.time;
					}
					else if (rigid.velocity.x > 0) {
						animation.Play("correrDerecha");
						lastDirection = movDer;
					}
					else if(raw < 0) {
						animation["giroDerechaIzq"].speed = 2;
						animation.Play("giroDerechaIzq", PlayMode.StopAll);
						animTime = Time.time;
						lastDirection = movIzq;
					}
					else {
						if(currentTime > animDuration){
							animation.Play("paradaDerecha");
							animTime = Time.time;
						}
						lastDirection = stopDer;
					}
				}
				else {
					if(currentTime > animDuration && disparo) {
						animation["disparaEscopetaIzq"].speed = 3;
						animation.Play("disparaEscopetaIzq",PlayMode.StopAll);
						disparo = false;
						animTime = Time.time;
					}
					else if(rigid.velocity.x < 0) {
						animation.Play("correrIzquierda");
						lastDirection = movIzq;
					}else if(raw > 0) {
						animation["giroIzquierdaDerecha"].speed = 2;
						animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
						animTime = Time.time;
						lastDirection = movDer;
					}else {
						if (currentTime > animDuration) {	
							animation.Play("paradaIzquierda");
							animTime = Time.time;
						}
						lastDirection = stopIzq;
					}
				}
				
				
			}
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
		//this.hud.notifyHealthChange(this.health);
	}
	private void fireDeathNotification() {
		//this.gameManager.notifyPlayerDeath();
		
	}
}
