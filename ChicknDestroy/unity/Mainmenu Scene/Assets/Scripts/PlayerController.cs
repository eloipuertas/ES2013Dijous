using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Parpadeig))]
public class PlayerController : Actor {
	
	private int[] animSpeed = {4,4,4,4,2,2,3,3,2,2,2,2,2,2,2,2,2,2,4,4,4,4,2,2};
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
	
	private float speed = 300;
	private float jumpHeight = 500;
	private float acceleration = 50;
	private float heightHero;
	
	//sonido
	private AudioSource sonidoSalto, sonidoPowerUp, sonidoEscudo, sonidoDisparoPistola, sonidoDisparoEscopeta, sonidoBandera, audioKatana, audioGrenade, audioMachineGun;
	Parpadeig p;
	private GameObject bala, granada, sortidaBalaDreta, sortidaBalaEsquerra, detected;
	
	//indica el tiempo transcurrido de animacion
	private float animTime;
	private float damageTime;
	
	//indica el tiempo de duracion de animacion
	private float animDuration;
	private float damageDuration;
	
	private int currentDirection;
	private int currentState;
	
	private const int STATE_STOP = 1;
	private const int STATE_RUNNING = 2;
	private const int STATE_DEAD = 3;
	
	private const int DIR_IZQUIERDA = 1;
	private const int DIR_DERECHA = 2;
	
	private Animation myAnim;
	
	private Rigidbody rigid;
	
	private GameObject gre, grk, grp;
	private bool esBajable, disparoActivo, ataque, dead, ataqueSecundario;
	
	void Start () {
		
		rigid =	GetComponent<Rigidbody>();
		
		gre = GameObject.Find(gameObject.name+"/gre");
		grk = GameObject.Find(gameObject.name+"/grk");
		grp = GameObject.Find(gameObject.name+"/grp");
		myAnim = gre.animation;
		
		sortidaBalaDreta =  GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		
		gameManager.setTarget(this.transform);
		
		
		initAnimations();
		animDuration = 0.3f;
		
		initSounds();
		
		health = 100;
		shield = 0;
		currentState = STATE_STOP;
		
		heightHero = rigid.collider.bounds.extents.y;
		
		//weapon = WEAPON_ESCOPETA;
		updateModelWeapon();
		
		granada = Resources.Load("ChickenPrefabs/weapons/granada") as GameObject;
		
		currentDirection = DIR_DERECHA;
		
		esBajable = false;
		ataque = false;
		dead = false;
		ataqueSecundario = false;
		
		damageDuration = 1.0f;
		damageTime = Time.time;
	}
	
	void walkDamage() {
		float currentTimeDamage = Time.time - damageTime;
		string tagHit = raycastVertical();
		
		if (tagHit.Equals("punxes")){
			if (currentTimeDamage > damageDuration) {
				dealDamage(5);
				p.mostrarDany();
				damageTime = Time.time;
			}
		}
		
	}
	
	void initSounds() {
		sonidoSalto = gameObject.AddComponent<AudioSource>();
		sonidoSalto.clip = Resources.Load("sounds/saltar") as AudioClip;
		
		sonidoPowerUp = gameObject.AddComponent<AudioSource>();
		sonidoPowerUp.clip = Resources.Load("sounds/power_up_curt") as AudioClip;
		
		sonidoEscudo = gameObject.AddComponent<AudioSource>();
		sonidoEscudo.clip = Resources.Load("sounds/so_escut") as AudioClip;
		
		sonidoDisparoPistola = gameObject.AddComponent<AudioSource>();
		sonidoDisparoPistola.clip = Resources.Load("sounds/tir_pistola_015") as AudioClip;
		
		sonidoDisparoEscopeta = gameObject.AddComponent<AudioSource>();
		sonidoDisparoEscopeta.clip = Resources.Load("sounds/tir_escopeta_0849") as AudioClip;
		
		sonidoBandera = gameObject.AddComponent<AudioSource>();
		sonidoBandera.clip = Resources.Load("sounds/flag") as AudioClip;
		
		audioMachineGun = gameObject.AddComponent<AudioSource>();
		audioMachineGun.clip = Resources.Load("sounds/machine_gun") as AudioClip;
		
		audioMachineGun = gameObject.AddComponent<AudioSource>();
		audioMachineGun.clip = Resources.Load("sounds/machine_gun") as AudioClip;
		
		audioKatana = gameObject.AddComponent<AudioSource>();
		audioKatana.clip = Resources.Load("sounds/katana") as AudioClip;

		audioGrenade = gameObject.AddComponent<AudioSource>();
		audioGrenade.clip = Resources.Load("sounds/granada_voice") as AudioClip;
		
	}
	
	void FixedUpdate(){
		
		if(health <= 0) currentState = STATE_DEAD;
		
		if(currentState != STATE_DEAD){	
			
			// deteccion de enemigos, mediante acercamiento.
			detected = raycastFront();
			
			walkDamage();
			
		
			float rawHori = Input.GetAxisRaw("Horizontal");
			float rawVert = Input.GetAxisRaw("Vertical");
	    
		
			if (!ataque && Input.GetButtonDown("Fire1")) {
				ataque = true;		
			}
				
			if (!ataqueSecundario && Input.GetButtonDown("Fire2")) {
				ataqueSecundario = true;
				
				GameObject novaGranada = null;
				if (currentDirection == DIR_DERECHA) {
					novaGranada = (GameObject) Instantiate (granada, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
					novaGranada.rigidbody.AddForce(Vector3.up * 250, ForceMode.VelocityChange);
					novaGranada.rigidbody.AddForce(new Vector3(400, 0, 0), ForceMode.VelocityChange);
				} else {
					novaGranada = (GameObject) Instantiate (granada, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
					novaGranada.rigidbody.AddForce(Vector3.up * 250, ForceMode.VelocityChange);
					novaGranada.rigidbody.AddForce(new Vector3(-400, 0, 0), ForceMode.VelocityChange);
				}
				GestioTir b = novaGranada.GetComponent("GestioTir") as GestioTir;
				b.setEquip(1);
				
				
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
			
			if (velX != 0) {
				//Hay movimiento, STATE_RUNNING
				
				if(currentState == STATE_RUNNING) {
					if(currentDirection == DIR_DERECHA && velX > 0) {
						
						if (!ataque && !ataqueSecundario) {
							doAnim("correrDerecha");
						} else if (ataqueSecundario){
							/*
							 * 		"atacarSecIzq",
									"atacarSecDer",
									"atacarSecIzqCorriendo",
									"atacarSecDerCorriendo",
							 * */
							doAnim("atacarSecDerCorriendo");
							ataqueSecundario = false;
						}else {
							doAnim("atacarDerCorriendo");
							realizarAtaque();
						}	
					} else if(currentDirection == DIR_DERECHA && velX == 0) {
						currentState = STATE_STOP;
					} else if(currentDirection == DIR_DERECHA && velX < 0) {
						doAnim("giroDerIzq");
						currentDirection = DIR_IZQUIERDA;
					} else if(currentDirection == DIR_IZQUIERDA && velX < 0) {
						if (!ataque && !ataqueSecundario) {
							doAnim("correrIzquierda");
						} else if(ataqueSecundario) {
							doAnim("atacarSecIzqCorriendo");
							ataqueSecundario = false;
						} else {
							doAnim("atacarIzqCorriendo");
							realizarAtaque();
						}
					} else if(currentDirection == DIR_IZQUIERDA && velX == 0) {
						currentState = STATE_STOP;
					} else {
						doAnim("giroIzqDer");
						currentDirection = DIR_DERECHA;
					}
				}else{
					currentState = STATE_RUNNING;
				}
				
			}else {
				//No hay movimiento; STATE_STOP
				
				if (currentState == STATE_STOP) {
					
					if(currentDirection == DIR_DERECHA) {
						if (!ataque && !ataqueSecundario) 
							doAnim("paradaDerecha");
						else if (ataqueSecundario) {
							doAnim("atacarSecDer");
							ataqueSecundario = false;
						}
						else {
							doAnim("atacarDer");
							realizarAtaque();
						}
						
					} else {
						if(!ataque && !ataqueSecundario){
							doAnim("paradaIzquierda");
						}else if (ataqueSecundario) {
							doAnim("atacarSecIzq");
							ataqueSecundario = false;
						}
						else{
							doAnim("atacarIzq");
							realizarAtaque();
						}
					}
				
				}else{
					currentState = STATE_STOP;
				}	
			}	
		}
	}
	
	void doAnim(string animName) {
		float currentTime = Time.time - animTime;
		bool permitido = 
			animName == "giroDerIzq"||
			animName == "giroIzqDer" ||
			animName == "atacarIzq" ||
			animName == "atacarDer" ||
			animName == "atacarIzqCorriendo" ||
			animName == "atacarDerCorriendo" ||
			animName == "atacarSecIzq" ||
			animName == "atacarSecDer" ||
			animName == "atacarSecDerCorriendo" ||
			animName == "atacarSecIzqCorriendo";
		
			/*
							 * 		"atacarSecIzq",
									"atacarSecDer",
									"atacarSecIzqCorriendo",
									"atacarSecDerCorriendo",
							 * */
		
		if ((currentTime > animDuration || permitido) && !dead) {
			myAnim.Play(animName, PlayMode.StopAll);
			animTime = Time.time;
			dead = animName == "muerteDerecha" || animName == "muerteIzquierda";
		}
	}
	
	void initAnimations() {
		
		for (int i = 0; i < 24; ++i) {
			//int speed = this.team == Actor.ROBOT_TEAM? animSpeed[i]:animSpeed[i]*2;
			int speed = animSpeed[i];
			gre.animation[animNames[i]].speed = speed;
			grk.animation[animNames[i]].speed = speed;
			grp.animation[animNames[i]].speed = speed;
		}

	}
	
	

	
	
	void OnCollisionEnter(Collision collision){
		
		float currentTimeDamage = Time.time - damageTime;
		
		if (collision.gameObject.tag == "Bandera") {
			
			switch (team) {
				
				case 1: flag = collision.gameObject.transform.position.x < 0; break;
				case 2: flag = collision.gameObject.transform.position.x > 0; break;
				
				default: break;
				
			}
			
			if (flag) {
				sonidoBandera.Play();
				Destroy (collision.gameObject);
				hud.notifyFlag(true, true);
				notifyHudPoints(300);
			}
		}
		
		if (collision.gameObject.tag =="foc" || collision.gameObject.tag =="guillotina") {
			if (currentTimeDamage > damageDuration) {
				dealDamage(5);
				p.mostrarDany();
				damageTime = Time.time;
			}
		}
			
		
		if(collision.gameObject.tag == "escut") {
				sonidoEscudo.Play();
				hud.notifyShieldChange(100);
				addShield(100);
		}
		
		if(collision.gameObject.tag == "upVida"){ 
				sonidoPowerUp.Play();
 				heal(50);
		}
		
		if (collision.gameObject.tag == "escopeta_off") {
			sonidoPowerUp.Play();
		}

		if (collision.gameObject.layer == 8) {
			esBajable = true;
		}else {
			esBajable = false;
		}
	}
	
	/* Para que se mueva conjuntamente con las plataformas horizontales */
	
	void OnCollisionStay (Collision hit) { 
		
		
	    if (hit.gameObject.tag == "plataforma_moviment")
	        transform.parent = hit.transform ; 
		else
	        transform.parent = null;
		
	}
	
	
	public void updateModelWeapon() {
		
		p = GetComponent("Parpadeig") as Parpadeig;
		
		switch(weapon){
			case WEAPON_KATANA:
				myAnim = grk.animation;
				grk.SetActive(true);
				gre.SetActive(false);
				grp.SetActive(false);
				disparoActivo = false;
				p.setCos(GameObject.Find(gameObject.name+"/grk/body"));
				p.setArma(GameObject.Find(gameObject.name+"/grk/weapon"));
				audioKatana.Play();
				break;
			case WEAPON_ESCOPETA:
				myAnim = gre.animation;
				gre.SetActive(true);
				grk.SetActive(false);
				grp.SetActive(false);
				bala = Resources.Load("ChickenPrefabs/weapons/balaEscopeta") as GameObject;
				p.setCos(GameObject.Find(gameObject.name+"/gre/body"));
				p.setArma(GameObject.Find(gameObject.name+"/gre/weapon"));
				disparoActivo = true;
				audioMachineGun.Play();
				break;
		case WEAPON_PISTOLA:
				myAnim = grp.animation;
				gre.SetActive(false);
				grk.SetActive(false);
				grp.SetActive(true);
				bala = Resources.Load("ChickenPrefabs/weapons/balaPistola") as GameObject;
				p.setCos(GameObject.Find(gameObject.name+"/grp/body"));
				p.setArma(GameObject.Find(gameObject.name+"/grp/weapon"));
				disparoActivo = true;
				audioMachineGun.Play();
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
					nouTir = (GameObject) Instantiate(bala, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
					nouTir.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
					break;
				case DIR_DERECHA:
					nouTir = (GameObject) Instantiate (bala, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
					nouTir.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
					break;
				default:
					break;
			}
			
			GestioTir b = nouTir.GetComponent("GestioTir") as GestioTir;
			b.setEquip(team);
			b.setArma(weapon);
			
			if (weapon == WEAPON_ESCOPETA)
				sonidoDisparoEscopeta.Play();
			else
				sonidoDisparoPistola.Play();
				
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
	
	private GameObject raycastFront(){
		RaycastHit hit;
		
		
		float ancho = rigidbody.collider.bounds.extents.x;
		
		Vector3 pos = this.gameObject.transform.position;
		Vector3 currentPos;
		
		bool trobat = false;
		
		for (int i=-30; i<30 && !trobat; i+=5){
			
			currentPos = pos + Vector3.up*i;
			
			if(Physics.Raycast(currentPos, currentDirection == DIR_DERECHA?Vector3.right:Vector3.left, out hit, ancho*2)) {
				trobat = true;
			}
		}

		if (!trobat)
			return null;
		return hit.collider.gameObject;
	}
	
	private bool isGround() {
		bool ret = false;
		for (int i = -2; i < 2 && !ret; ++i) {
			ret = ret || Physics.Raycast((transform.position + new Vector3(i,0,0)), Vector3.down, team==ROBOT_TEAM? heightHero+ 0.1f:3f);
			
		}
		return ret;
	}
	
	private string raycastVertical() {
		RaycastHit hit;
		bool ret = false;
		
		for (int i = -2; i < 2 && !ret; ++i) {
			ret = ret || Physics.Raycast((transform.position + new Vector3(i,0,0)), Vector3.down, out hit, team==ROBOT_TEAM? heightHero+ 0.1f:3f);
			
		}
		
		if (!ret)
			return "";
		return hit.collider.tag;
	}
	
	
	private bool isEnemy(Actor a){
		if (a == null) return false;
		return getTeam() != a.getTeam();
	}
	
	public void notifyHudPoints(int p) {
		this.hud.notifyPoints(p);
		gameManager.notifyScoreChange(this.hud.getPoints());
	}
	
}
