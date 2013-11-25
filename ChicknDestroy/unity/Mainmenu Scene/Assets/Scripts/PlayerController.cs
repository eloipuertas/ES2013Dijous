using UnityEngine;
using System.Collections;

public class PlayerController : Actor {
	
<<<<<<< HEAD
	//Atributos de personaje
	private int health;
	private GameObject primaryWeapon;
	private GameObject secondaryWeapon;
	
	//Atributos de control
	private float gravity = 200;
=======
	private int[] animSpeed = {4,4,4,4,2,2,3,3,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2};
	private string[] animNames = {
		"atacarIzq",
		"atacarDer",
		"atacarIzqCorriendo",
		"atacarDerCorriendo",
		"caidaIzquierda",
		"caidaDerecha",
		"correrIzquierda",
		"correrDerecha",
		"giroDerIzq",
		"giroIzqDer",
		"paradaIzquierda",
		"paradaDerecha",
		"recogerItemIzq",
		"recogerItemDer",
		"saltoVerticalIzq",
		"saltoVerticalDer",
		"saltarDerecha",
		"saltarIzquierda",
		"atacarSecIzq",
		"atacarSecDer",
		"atacarSecIzqCorriendo",
		"atacarSecDerCorriendo",
		"muerteDerecha",
		"muerteIzquierda"
	};	//24
	

	
	//Atributos de control
	
>>>>>>> origin/devel-A
	private float speed = 300;
	private float jumpHeight = 500;
	private float acceleration = 50;
	
	//sonido
<<<<<<< HEAD
	public AudioSource sonidoSalto;
	public AudioSource sonidoDisparoEscopeta;
	public AudioSource sonidoDisparoPistola;
	public AudioSource sonidoPowerUp;
	public AudioSource sonidoEscudo; 
	
	public GameObject balaEscopeta;
	public GameObject balaPistola;
	public GameObject granada;
	public GameObject sortidaBalaDreta;
	public GameObject sortidaBalaEsquerra;
	public GameObject sortidaBalaDretaCorrent;
	public GameObject sortidaBalaEsquerraCorrent;
=======
	private AudioSource sonidoSalto, sonidoDisparo, sonidoPowerUp;
	private GameObject bala, sortidaBalaDreta, sortidaBalaEsquerra, detected;
>>>>>>> origin/devel-A
	
	//indica el tiempo transcurrido de animacion
	private float animTime;
<<<<<<< HEAD
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
=======
	
	//indica el tiempo de duracion de animacion
	private float animDuration;
	
	
	private int currentDirection;
	private int currentState;
	
	private const int STATE_STOP = 1;
	private const int STATE_RUNNING = 2;
	private const int STATE_DEAD = 3;
	
	private const int DIR_IZQUIERDA = 1;
	private const int DIR_DERECHA = 2;
>>>>>>> origin/devel-A
	
	private HUD hud;
	private GameManager gameManager;
	private Animation myAnim;
	
	private Rigidbody rigid;
	
<<<<<<< HEAD
	private GameObject gre;
	private GameObject grk;
	
	public bool isKatana;
=======
	private GameObject gre, grk, grp;
	private bool esBajable, disparoActivo, ataque, dead;
>>>>>>> origin/devel-A
	
	void Start () {
		
		rigid =	GetComponent<Rigidbody>();
<<<<<<< HEAD
=======
		
		gre = GameObject.Find(gameObject.name+"/gre");
		grk = GameObject.Find(gameObject.name+"/grk");
		grp = GameObject.Find(gameObject.name+"/grp");
		
		sortidaBalaDreta =  GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		bala = GameObject.FindGameObjectWithTag("bala");
>>>>>>> origin/devel-A
		
		gre = GameObject.Find("gre");
		grk = GameObject.Find("grk");
		if (isKatana) {
			myAnim = gre.animation;
			grk.SetActive(false);
			gre.SetActive(true);
		}
		else {
			myAnim = grk.animation;
			gre.SetActive(false);
			grk.SetActive(true);
		}
	
		lastDirection = stopDer;
		
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
<<<<<<< HEAD
		health = 100;
		
		disparo = true;
=======
		
		gameManager.setTarget(this.transform);
		
		
		initAnimations();
		animDuration = 0.3f;
		
		health = 100;
		currentState = STATE_STOP;
>>>>>>> origin/devel-A
		
		heightHero = rigid.collider.bounds.extents.y;
	}
	
	private bool isGround() {
		return Physics.Raycast(transform.position, -Vector3.up, heightHero + 0.5f);
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "upVida"){ 
				sonidoPowerUp.Play(); //el motivo por el cual suena en el script del player el sonido del power up es porque al autodestruirse rapidamente el power up no se oye en su script
 				heal(50);
		}
		
<<<<<<< HEAD
		if(collision.gameObject.tag == "escopeta_off" || collision.gameObject.tag == "katana" || collision.gameObject.tag == "granada") {
				sonidoPowerUp.Play();
		}
		
		if(collision.gameObject.tag == "escut") {
				sonidoEscudo.Play();
		}
		
	}
	
	/* Para que se mueva conjuntamente con las plataformas horizontales */
	void OnCollisionStay (Collision hit) { 
		
	    if (hit.gameObject.tag == "plataforma_moviment")
	        transform.parent = hit.transform ; 
	    else
	        transform.parent = null;
		
	}
	
	void Update () {
		
		if (isKatana) {
			myAnim = gre.animation;
			grk.SetActive(false);
			gre.SetActive(true);
		}
		else {
			myAnim = grk.animation;
			gre.SetActive(false);
			grk.SetActive(true);
		}
		
		
		float raw = Input.GetAxisRaw("Horizontal");
=======
		weapon = WEAPON_KATANA;
		updateModelWeapon();
		
		currentDirection = DIR_DERECHA;
		
		esBajable = false;
		ataque = false;
		dead = false;
	}
	
	
	
	void FixedUpdate(){
		
		if(health <= 0) currentState = STATE_DEAD;
>>>>>>> origin/devel-A
		
		if(currentState != STATE_DEAD){	
			
<<<<<<< HEAD
		if (!disparo && Input.GetButtonDown("Fire2")) {
			GameObject novaGranada = (GameObject) Instantiate (granada, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
			GestioTir b = novaGranada.GetComponent("GestioTir") as GestioTir;
			b.setEquip(1);
			novaGranada.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
			//disparo = true;
		}
=======
			// deteccion de enemigos, mediante acercamiento.
			detected = raycastFront(currentDirection);
			
			updateModelWeapon();
>>>>>>> origin/devel-A
		
			float rawHori = Input.GetAxisRaw("Horizontal");
			float rawVert = Input.GetAxisRaw("Vertical");
	    
		
<<<<<<< HEAD
		if (isGround()) {
			if (Input.GetButtonDown("Jump")) {
				sonidoSalto.Play();
				rigid.velocity += Vector3.up * jumpHeight;
			}		
		}
		
		//Actualiza la posicion del personaje
		rigid.velocity = new Vector3((raw * speed * acceleration)*Time.deltaTime, rigid.velocity.y, 0);
		rigid.velocity += (Vector3.up * -gravity * Time.deltaTime);
=======
			if (!ataque && Input.GetButtonDown("Fire1")) {
				ataque = true;		
			}
				
			if (!ataque && Input.GetButtonDown("Fire2")) {
				ataque = true;
			}
			
			
			if (isGround()) {
				if (rawVert > 0) {
					sonidoSalto.Play();
					rigid.velocity += Vector3.up * jumpHeight;
				}
				else if(rawVert < 0 && esBajable) {
					transform.position += Vector3.down*50;
				}
			}
			
			//Actualiza la posicion del personaje
			
			rigid.velocity = new Vector3((rawHori * speed * acceleration)*Time.deltaTime, rigid.velocity.y, 0);
			
			float velX = rigid.velocity.x;
			float velY = rigid.velocity.y;
			
			controlDeAnimaciones(velX, velY);
			
		}else{
			currentState = STATE_DEAD;
			if (currentDirection == DIR_DERECHA)
				doAnim("muerteDerecha");
			else 
				doAnim("muerteIzquierda");
		}
	}

	void Update () {
		// Va muy rapido, nada aqui :D
	}
>>>>>>> origin/devel-A
	
	/********* CODIGO AUXILIAR **************/
	
	void controlDeAnimaciones(float velX, float velY) {
		if(velY > 65) {
		//Si estamos en el aire de subida
			
			if(currentDirection == DIR_DERECHA) 
				doAnim("saltoVerticalDer");
			else 
				doAnim("saltoVerticalIzq");
			
		}else if(velY < -65){
		//Si estamos en el aire de bajada
			
			if(currentDirection == DIR_DERECHA) 
				doAnim("caidaDerecha");
			else 
				doAnim("caidaIzquierda");
			
		}else {
			
<<<<<<< HEAD
			if (lastDirection == movDer) {
				if (rigid.velocity.x > 0) {
					if(currentTime > animDuration){
						if(disparo) {
							myAnim["atacarDerCorriendo"].speed = 3;
							myAnim.Play("atacarDerCorriendo",PlayMode.StopAll);
							GameObject nouTirEscopeta = (GameObject) Instantiate (balaEscopeta, sortidaBalaDretaCorrent.transform.position, sortidaBalaDretaCorrent.transform.rotation);
							GestioTir b = nouTirEscopeta.GetComponent("GestioTir") as GestioTir;
							b.setEquip(1);
							nouTirEscopeta.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
							sonidoDisparoEscopeta.Play();
							disparo = false;
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
							myAnim.Play("atacarIzqCorriendo",PlayMode.StopAll);;
							GameObject nouTirEscopeta = (GameObject) Instantiate (balaEscopeta, sortidaBalaEsquerraCorrent.transform.position, sortidaBalaEsquerraCorrent.transform.rotation);
							GestioTir b = nouTirEscopeta.GetComponent("GestioTir") as GestioTir;
							b.setEquip(1);
							nouTirEscopeta.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
							sonidoDisparoEscopeta.Play();
							disparo = false;
							disparo = false;
						}
						else myAnim.Play("correrIzquierda");
						animTime = Time.time;
=======
			if (velX != 0) {
				//Hay movimiento, STATE_RUNNING
				
				if(currentState == STATE_RUNNING) {
					if(currentDirection == DIR_DERECHA && velX > 0) {
						if (!ataque) 
							doAnim("correrDerecha");
						else {
							doAnim("atacarDerCorriendo");
							realizarAtaque();
						}	
					} else if(currentDirection == DIR_DERECHA && velX == 0) {
						currentState = STATE_STOP;
					} else if(currentDirection == DIR_DERECHA && velX < 0) {
						doAnim("giroDerIzq");
						currentDirection = DIR_IZQUIERDA;
					} else if(currentDirection == DIR_IZQUIERDA && velX < 0) {
						if (!ataque) 
							doAnim("correrIzquierda");
						else {
							doAnim("atacarIzqCorriendo");
							realizarAtaque();
						}
					} else if(currentDirection == DIR_IZQUIERDA && velX == 0) {
						currentState = STATE_STOP;
					} else {
						doAnim("giroIzqDer");
						currentDirection = DIR_DERECHA;
>>>>>>> origin/devel-A
					}
				}else{
					currentState = STATE_RUNNING;
				}
				
			}else {
<<<<<<< HEAD
				if (lastDirection == stopDer) {
					if (disparo && currentTime > animDuration) {
						myAnim["atacarDer"].speed = 3;
						myAnim.Play("atacarDer",PlayMode.StopAll);
						GameObject nouTirPistola = (GameObject) Instantiate (balaPistola, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
						GestioTir b = nouTirPistola.GetComponent("GestioTir") as GestioTir;
						b.setEquip(1);
						nouTirPistola.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
						sonidoDisparoPistola.Play();
						disparo = false;
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
						GameObject nouTirEscopeta = (GameObject) Instantiate (balaEscopeta, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
						GestioTir b = nouTirEscopeta.GetComponent("GestioTir") as GestioTir;
						b.setEquip(1);
						nouTirEscopeta.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
						sonidoDisparoEscopeta.Play();
						disparo = false;
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
=======
				//No hay movimiento; STATE_STOP
				
				if (currentState == STATE_STOP) {
					
					if(currentDirection == DIR_DERECHA) {
						if (!ataque) 
							doAnim("paradaDerecha");
						else {
							doAnim("atacarDer");
							realizarAtaque();
						}
						
					} else {
						if(!ataque)
							doAnim("paradaIzquierda");
						else{
							doAnim("atacarIzq");
							realizarAtaque();
>>>>>>> origin/devel-A
						}
					}
				
				}else{
					currentState = STATE_STOP;
				}	
			}	
		}
		
	}
	
<<<<<<< HEAD
	public int getHealthPoints() {
		return health;
	}
	
	public void setHealthPoints(int n) {
		health = n;
		fireHealthNotification();
		if (health <= 0) {
			fireDeathNotification();
=======
	void doAnim(string animName) {
		float currentTime = Time.time - animTime;
		bool permitido = animName == "giroDerIzq"||
							animName == "giroIzqDer" ||
							animName == "atacarIzq" ||
							animName == "atacarDer" ||
							animName == "atacarIzqCorriendo" ||
							animName == "atacarDerCorriendo";
		if ((currentTime > animDuration || permitido) && !dead) {
			myAnim.Play(animName, PlayMode.StopAll);
			animTime = Time.time;
			dead = animName == "muerteDerecha" || animName == "muerteIzquierda";
		}
	}
	
	void initAnimations() {
		
		for (int i = 0; i < 24; ++i) {
			gre.animation[animNames[i]].speed = animSpeed[i];
			grk.animation[animNames[i]].speed = animSpeed[i];
			grp.animation[animNames[i]].speed = animSpeed[i];
		}

	}
	
	
	private bool isGround() {
		bool ret = false;
		for (int i = -2; i < 2 && !ret; ++i) {
			ret = ret || Physics.Raycast((transform.position + new Vector3(i,0,0)), Vector3.down, team==ROBOT_TEAM? heightHero+ 0.1f:3f);
			
>>>>>>> origin/devel-A
		}
	}
	
<<<<<<< HEAD
	public void heal(int n){
		setHealthPoints(Mathf.Min(100,health+n));
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
=======
	
	
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
				disparoActivo = false;
			
				break;
			case WEAPON_ESCOPETA:
				myAnim = gre.animation;
				gre.SetActive(true);
				grk.SetActive(false);
				grp.SetActive(false);
				disparoActivo = true;
				break;
		case WEAPON_PISTOLA:
				myAnim = gre.animation;
				gre.SetActive(false);
				grk.SetActive(false);
				grp.SetActive(true);
				disparoActivo = true;
				break;
			default:
				break;
		}
		
	}
	
	void realizarAtaque() {
		if(disparoActivo) {
			GameObject nouTir = null;
			
			switch(currentDirection){
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
		} else {
			
			if(detected != null){
				
				Actor actor = detected.GetComponent(typeof(Actor)) as Actor;
				if(isEnemy(actor)) {
					actor.dealDamage(100);
				}
				
			}
		}
		ataque = false;
	}
	
	private GameObject raycastFront(int dir){
		RaycastHit hit;
		
		//float mitadAltura = rigidbody.collider.bounds.extents.y*0.7f;
		float mitadAltura = heightHero;
		float ancho = rigidbody.collider.bounds.extents.x;
		//Debug.Log("ANCHO"+ancho);
		Vector3 pos = this.gameObject.transform.position;
		Vector3 currentPos;
		bool trobat = false;
		
		for (int i=-30; i<30 && !trobat; i+=5){
			
			currentPos = pos + Vector3.up*i;
			
			if(Physics.Raycast(currentPos, dir==DIR_DERECHA?Vector3.right:Vector3.left, out hit, ancho*2)) {
				trobat = true;
			}
		}

		if (!trobat)
			return null;
		return hit.collider.gameObject;
	}
	
	
	private bool isEnemy(Actor a){
		if (a == null) return false;
		return getTeam() != a.getTeam();
	}

>>>>>>> origin/devel-A
	
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
