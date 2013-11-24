using UnityEngine;
using System.Collections;

public class PlayerController : Actor {
	
	
	//Atributos de control
	
	private float speed = 300;
	private float jumpHeight = 500;
	private float acceleration = 50;
	private float heightHero;
	
	//sonido
	private AudioSource sonidoSalto, sonidoDisparo, sonidoPowerUp;
	private GameObject bala, sortidaBalaDreta, sortidaBalaEsquerra;
	
	private float animTime;
	private float animDuration = 0.3f;

	private int movDer = 1;
	private int movIzq = 2;
	private int stopDer = 3;
	private int stopIzq = 4;
	private bool disparo= false;
	
	private int lastMovement;
	private int lastDirection;
	private int currentState;
	
	private const int STATE_ALIVE = 1;
	private const int STATE_DEAD = 2;
	
	private const int MOV_STOP = 1;
	private const int MOV_RUNNING = 2;
	private const int MOV_ATACKING = 3;
	
	private const int DIR_IZQUIERDA = 1;
	private const int DIR_DERECHA = 2;
	
	private Animation myAnim;
	
	private Rigidbody rigid;
	
	private GameObject gre, grk, grp;
	
	
	private bool esBajable;
	
	void Start () {
		
		rigid =	GetComponent<Rigidbody>();
		Debug.Log("NAME: "+this.gameObject.name);
		gre = GameObject.Find(gameObject.name+"/gre");
		grk = GameObject.Find(gameObject.name+"/grk");
		grp = GameObject.Find(gameObject.name+"/grp");
		
		sortidaBalaDreta =  GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		bala = GameObject.FindGameObjectWithTag("bala");
		
		sonidoSalto = GameObject.Find("Sounds/Jump").GetComponent<AudioSource>();
		sonidoDisparo = GameObject.Find("Sounds/Shot").GetComponent<AudioSource>();
		sonidoPowerUp = GameObject.Find("Sounds/Power_up").GetComponent<AudioSource>();
		
		
		sortidaBalaDreta.transform.position = new Vector3(55.5f,7f,22f);
		//sortidaBalaDreta.transform.rotation = Quaternion.identity;
		
		sortidaBalaEsquerra.transform.position = new Vector3(-47.5f,5.2f,22f);
		//sortidaBalaEsquerra.transform.rotation = Quaternion.identity;
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		
		gameManager.setTarget(this.transform);
		
		health = 100;
		currentState = STATE_ALIVE;
		
		heightHero = rigid.collider.bounds.extents.y;
		
		weapon = WEAPON_ESCOPETA;
		updateModelWeapon();
		lastDirection = stopDer;
		
		esBajable = false;
		disparo = false;
	}
	
	void FixedUpdate(){
		
		updateModelWeapon();
		
		float raw = Input.GetAxisRaw("Horizontal");
		float rawVertical = Input.GetAxisRaw("Vertical");
		
		if (!disparo && Input.GetButtonDown("Fire1")) {
			disparo = true;		
		}
			
		if (!disparo && Input.GetButtonDown("Fire2")) {
			disparo = true;
		}
		
		
		if (isGround()) {
			if (rawVertical > 0) {
				sonidoSalto.Play();
				rigid.velocity += Vector3.up * jumpHeight;
			}
			else if(rawVertical < 0 && esBajable) {
				transform.position = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
			}
		}
		
		//Actualiza la posicion del personaje
		rigid.velocity = new Vector3((raw * speed * acceleration)*Time.deltaTime, rigid.velocity.y, 0);
		//rigid.velocity += (Vector3.up * -gravity * Time.deltaTime);
	
		
		if(rigid.velocity.y > 10) {
		//Si estamos en el aire de subida
			if(lastDirection == movDer || lastDirection == stopDer) myAnim.Play("saltoVerticalDer");
			else myAnim.Play("saltoVerticalIzq");
		}else if(rigid.velocity.y < -65){
		//Si estamos en el aire de bajada
			if(lastDirection == movDer || lastDirection == stopDer) myAnim.Play("caidaDerecha");
			else myAnim.Play("caidaIzquierda");
		}else {

			float currentTime = Time.time - animTime;
			
			if (lastDirection == movDer) {
				if (rigid.velocity.x > 0) {
					if(currentTime > animDuration){
						if(disparo) {
							myAnim["atacarDerCorriendo"].speed = 3;
							myAnim.Play("atacarDerCorriendo",PlayMode.StopAll);
							realizarTiro(DIR_DERECHA);
						}
						else myAnim.Play("correrDerecha");
						animTime = Time.time;
					}
					lastDirection = movDer;
					//sonidoWalking.Play(1000);
				}
				else if(raw < 0) {
					myAnim["giroDerIzq"].speed = 2;
					myAnim.Play("giroDerIzq", PlayMode.StopAll);
					animTime = Time.time;
					lastDirection = movIzq;
					//if(currentTime > animDuration)sonidoWalking.Play();
				}
				else {
					lastDirection = stopDer;
				}
				
			}else if (lastDirection == movIzq) {
				if (rigid.velocity.x < 0) {
					if(currentTime > animDuration){
						if(disparo) {
							myAnim["atacarIzqCorriendo"].speed = 3;
							myAnim.Play("atacarIzqCorriendo",PlayMode.StopAll);
							realizarTiro(DIR_IZQUIERDA);
						}
						else myAnim.Play("correrIzquierda");
						animTime = Time.time;
					}
					lastDirection = movIzq;
					//if(currentTime > animDuration)sonidoWalking.Play();
				}
				else if(raw > 0) {
					myAnim["giroIzqDer"].speed = 2;
					myAnim.Play("giroIzqDer", PlayMode.StopAll);
					animTime = Time.time;
					lastDirection = movDer;
					//if(currentTime > animDuration)sonidoWalking.Play();
				}
				else {
					lastDirection = stopIzq;
				}
			}else {
				if (lastDirection == stopDer) {
					if (disparo && currentTime > animDuration) {
						myAnim["atacarDer"].speed = 3;
						myAnim.Play("atacarDer",PlayMode.StopAll);
						realizarTiro(DIR_DERECHA);
						animTime = Time.time;
					}
					else if (rigid.velocity.x > 0) {
						myAnim.Play("correrDerecha");
						lastDirection = movDer;
					}
					else if(raw < 0) {
						myAnim["giroDerIzq"].speed = 2;
						myAnim.Play("giroDerIzq", PlayMode.StopAll);
						animTime = Time.time;
						lastDirection = movIzq;
					}
					else {
						if(currentTime > animDuration){
							myAnim.Play("paradaDerecha");
							animTime = Time.time;
						}
						lastDirection = stopDer;
					}
				}
				else {
					if(currentTime > animDuration && disparo) {
						myAnim["atacarIzq"].speed = 3;
						myAnim.Play("atacarIzq",PlayMode.StopAll);
						realizarTiro(DIR_IZQUIERDA);
						animTime = Time.time;
					}
					else if(rigid.velocity.x < 0) {
						myAnim.Play("correrIzquierda");
						lastDirection = movIzq;
					}else if(raw > 0) {
						myAnim["giroIzqDer"].speed = 2;
						myAnim.Play("giroIzqDer", PlayMode.StopAll);
						animTime = Time.time;
						lastDirection = movDer;
					}else {
						if (currentTime > animDuration) {	
							myAnim.Play("paradaIzquierda");
							animTime = Time.time;
						}
						lastDirection = stopIzq;
					}
				}
				
				
			}
		}
	}
	
	void Update () {
		// Va muy rapido, nada aqui :D
	}
	
	/********* CODIGO AUXILIAR **************/
	
	private bool isGround() {
		bool ret = false;
		for (int i = -2; i < 2 && !ret; ++i) {
			ret = ret || Physics.Raycast((transform.position + new Vector3(i,0,0)), Vector3.down, team==ROBOT_TEAM? heightHero+ 0.1f:3f);
		}
		return ret;
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "upVida"){ 
				sonidoPowerUp.Play(); //el motivo por el cual suena en el script del player el sonido del power up es porque al autodestruirse rapidamente el power up no se oye en su script
 				heal(50);
		}
		
		if(collision.gameObject.tag == "escopeta_off") {
				sonidoPowerUp.Play();
		}
		if (collision.gameObject.layer == 8) {
			esBajable = true;
		}else {
			esBajable = false;
		}
	}
	
	void updateModelWeapon() {
		
		switch(weapon){
			case WEAPON_KATANA:
				
				myAnim = grk.animation;
				grk.SetActive(true);
				gre.SetActive(false);
				grp.SetActive(false);
				
			
				break;
			case WEAPON_ESCOPETA:
				myAnim = gre.animation;
				gre.SetActive(true);
				grk.SetActive(false);
				grp.SetActive(false);
				break;
			default:
				break;
		}
		
	}
	
	void realizarTiro(int direccion) {
		GameObject nouTir = null;
		
		switch(direccion){
			case DIR_IZQUIERDA:
				nouTir = (GameObject) Instantiate (bala, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
				nouTir.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
				break;
			case DIR_DERECHA:
				nouTir = (GameObject) Instantiate (bala, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
				nouTir.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
				break;
			default:
				break;
		}
		
		nouTir.AddComponent("DestruirBala");
		sonidoDisparo.Play();
		disparo = false;
	}
	

	
}
