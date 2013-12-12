using UnityEngine;
using System.Collections;
using System.Timers;

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
	
	//indica el tiempo transcurrido de animacion
	private float animTime;
	
	//indica el tiempo de duracion de animacion
	private float animDuration;
	
	private int currentState;
	
	private const int STATE_STOP = 1;
	private const int STATE_RUNNING = 2;
	private const int STATE_DEAD = 3;
	
	private Animation myAnim;
	
	private Rigidbody rigid;
	
	private GameObject gre, grk, grp;
	private bool disparoActivo, ataque, ataqueSecundario;
	
	
	void Start () {
		hud =  (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		this.secondary = (ThrowableWeapon)(WeaponFactory.instance ().create (WeaponFactory.WeaponType.GRANADE));
		this.hud.notifyAmmo (2,this.secondary.getCAmmo ());
		
		rigid =	GetComponent<Rigidbody>();
		
		gre = GameObject.Find(gameObject.name+"/gre");
		grk = GameObject.Find(gameObject.name+"/grk");
		grp = GameObject.Find(gameObject.name+"/grp");
		myAnim = gre.animation;
		
		sortidaBalaDreta =  GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		sang = Resources.Load("effects_prefabs/sangPistola") as GameObject;

		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		
		//Asignamos la camara al personaje controlable.
		gameManager.setTarget(this.transform);
		
		
		initAnimations();
		animDuration = 0.3f;
		
		initSounds();
		initFlagManagement();
		
		health = 100;
		shield = 0;
		currentState = STATE_STOP;
		
		heightHero = rigid.collider.bounds.extents.y;
		
		updateModelWeapon();
		
		granada = Resources.Load("ChickenPrefabs/weapons/granada") as GameObject;
		
		currentDirection = DIR_DERECHA;
		
		esBajable = false;
		ataque = false;
		dead = false;
		ataqueSecundario = false;
		
		damageDuration = 1.0f;
		damageTime = Time.time; // Useless (used for active waiting)
		
		initTimers();
	}
	/* NEW CODE --- @LynosSorien
	 * Function to init the TimerPool.
	 * Refactorice to Actor.
	 */ 
	private void initTimers() {
		this.timers = new TimerPool(5);
	}
	
	void FixedUpdate(){
		
		if(health <= 0) currentState = STATE_DEAD;
		
		if(currentState != STATE_DEAD){	
			
			// deteccion de enemigos, mediante acercamiento.
			detected = raycastFront();
			
		
			float rawHori = Input.GetAxisRaw("Horizontal");
			float rawVert = Input.GetAxisRaw("Vertical");
	    
		
			if (!ataque && Input.GetButtonDown("Fire1")) {
				ataque = true;		
			}
			if (!ataqueSecundario && Input.GetButtonDown("Fire2")) {
				ataqueSecundario = true;
				if(doSecondaryAttack()) // Moved to this function.
					if(this.secondary.GetType() == typeof(ThrowableWeapon))
						this.hud.notifyAmmo(2,((ThrowableWeapon)this.secondary).getCAmmo());
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
		float currentTime = Time.time - animTime; // Active waiting
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
		// ?? 
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
		/*
		Add an special collider for the chickens when one is above from the other. We can make that the chicken
		that are above just jump and deal damage to the other chiken.
		For implement this, we can just add two new collider boxes, one on the top with ID = 1 and the other
		one on the bottom with ID = 2. (Or simply make a two specifications of object collider to diferenciate it).
		When there two collides, the chicken with collide ID = 2 will deal damage to the other and will jump (as a normal jump).
		Also can be added an special effect (like the chicken that have given the damage will turn a superdeformed chicken for a while).
		*/
	
	/*
	* mètode que carrega un model o un altre en funció de l'arma utilitzada.
	* a més, cambia alguns flags, com per exemple boolean disparoActivo.
	*/
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
	// Do an attack (For Player) -- Primary Weapon only (Guess)
	void realizarAtaque() {
		if(disparoActivo) { // View if the weapon is long range type.
			if(doPrimaryAttack()) {
				if (this.primary.GetType() == typeof(DistanceWeapon))
					this.hud.notifyAmmo (1,((DistanceWeapon)this.primary).getCAmmo());
			}
		} else { // Melee weapon.
			if (!audioKatana.isPlaying) audioKatana.Play();
			if(detected != null){
				Actor actor = detected.GetComponent(typeof(Actor)) as Actor;
				if(isEnemy(actor)) {
					actor.dealDamage(this.primary.getDamage()); // Katana damage
					p.mostrarDany();
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
			ret = ret || Physics.Raycast((transform.position + new Vector3(i,0,0)), Vector3.down, heightHero+ 0.1f);
			
		}
		return ret;
	}
	
	protected void fireHealthNotification(){ this.hud.notifyHealthChange(this.health);}
	protected void fireDeathNotification(){ this.gameManager.notifyPlayerDeath();}
	protected void fireShieldNotification(){ this.hud.notifyShieldChange(this.shield);}

}
