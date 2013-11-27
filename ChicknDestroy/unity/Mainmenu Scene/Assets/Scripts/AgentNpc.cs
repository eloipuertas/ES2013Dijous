using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class AgentNpc : FSM {
	
	//Agent finite stats
	public enum FSM{
		None,
		Run,
		Jump,
		Attack,
		Dead,
	}
	
	//sonidos
	private AudioSource sonidoDisparoPistola, sonidoDisparoEscopeta;
	
	private PlayerController playerController;
	
	//Modelos de armas
	private GameObject[] weap_mod;
	private GameObject bala, granada, sortidaBalaDreta, sortidaBalaEsquerra;
	private Parpadeig p;

	//Lectors de rutes
	private int keyPosActual = 0; 
	public string direrutas = "ruta1";
	private List <Vector3> rutaActual  = new List<Vector3>();
	private bool routeJump = false;	
	
	//Target
	private Vector3 nextTarget;
	private Vector3 relPos;
	private bool targetEnemy = false;
	//Especifications
	private int jumpForce = 500;
	private float velocity = 175f;
	
	//Npc atributes
	private int puntuacio = 200;
	private int radioVision = 425;
	private int stopDistance = 40;
	private int rangeWeapon = 0;
	
	
	//Npc propierties
	GameObject nouTir;
	private Vector3 spawnPoint;

	//Status
	private FSM curState;
	private Animation animations;
	private bool derecha = true;
	private bool dead = false;
	private float elapsedTime = 0;
	private bool haAtacado = true;
	

	
	private Vector3 lastPos;
	private bool changeDir = true;
	
	
	
	/*################################################################
	###################### INITIALIZATION OF NPC #####################*/
	 
	protected override void Ini(){
		loadRoute();
		setInitialsAttributes();

		updateNextTarget();
	}
	
	void setInitialsAttributes(){
		nextTarget = rutaActual[keyPosActual];
		curState = FSM.Run;
			
		this.setHealth(100);
		sortidaBalaDreta =  GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		AudioSource[] audios = GetComponents<AudioSource>();
		sonidoDisparoPistola = audios[3];
		sonidoDisparoEscopeta = audios[4];
		
		p = GameObject.FindGameObjectWithTag("Player").GetComponent("Parpadeig") as Parpadeig;
		playerController = GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerController") as PlayerController;
	
	}
	
	void loadRoute(){
		rutaActual = new List<Vector3>();
		int fileindex = 0;
		int posindex = 0;
		
		
		TextAsset bindata= (TextAsset) Resources.Load("routes/"+direrutas, typeof(TextAsset));
		string content = bindata.text;
		
		string []lines = content.Split('|');
		foreach(string s in lines){
			string []pos = s.Split(',');
			//Debug.Log(pos.Length);
			float x = float.Parse(pos[0]);
			float y = float.Parse(pos[1]);
			float z = float.Parse(pos[2]);
			rutaActual.Insert(posindex,(Vector3)new Vector3(x,y,z));
			posindex +=1;
		}

		
		content ="";
		posindex = 0;
		
		fileindex +=1;
	}	
	
	
	


	



	/*################################################################
	############################# UPDATE #############################*/
	
	protected override void FSMFixedUpdate(){
		if (animations != null){
			switch (curState){
				case FSM.None: UpdateNoneState(); break;
				case FSM.Jump: UpdateJumpState(); break;
				case FSM.Run: UpdateRunState(); break;
				case FSM.Attack: UpdateAttackState(); break;
				case FSM.Dead: UpdateDeadState(); break;
	
	 		}
			
	 		elapsedTime += Time.deltaTime;
	
			if (this.getHealth() <= 0) curState = FSM.Dead;

		}
	}
	

	void updateNextTarget(){
		//Raycast en esfera. Para detectar multiples enemigos
		Collider[] colls = Physics.OverlapSphere(getPosition(),radioVision);

		GameObject closestEnemy = null;
		float d,distance = float.PositiveInfinity;
		// detecta el enemigo mas cercano
		for (int i=0; i<colls.Length; i++){
			Actor a = colls[i].gameObject.GetComponent(typeof(Actor)) as Actor;
			if(isEnemy(a)){ // Comprobar rivales
				d = distance3D(getPosition(), colls[i].transform.position);
				if (d < distance){
					d = distance;
					closestEnemy = colls[i].gameObject;
				}
			}
		}
		
		if (closestEnemy != null){
			nextTarget = closestEnemy.transform.position;
			nextTarget.z = 0;
			relPos = nextTarget - getPosition();
			targetEnemy = true;
			return;
		}
		
		targetEnemy = false;
		nextTarget = rutaActual[keyPosActual];
		//Debug.Log("####NPC GO TO -------> "+nextTarget);
		relPos = nextTarget - getPosition();
		bool next = false;
		
		// Pasar al siguiente target.
			// Si z=3 espera a que haya algo en (x,y)
		if (((nextTarget.z == 3) && (anythingOn(nextTarget))) ||			
			// Si z=2 debera acercarse al punto (x,y) almenos en 30
			((nextTarget.z == 2) && (distance3D(nextTarget,getPosition()) <= 30)) ||
			// Si z=1 debera estar cerca respecto al eje x almenos en 15, y tendra que estar tocando el suelo
			((nextTarget.z == 1) && (Mathf.Abs(relPos.x) <= 15) && onGround()) || 
			// Si z=0 debera estar cerca respecto al eje x almenos en 15
			((nextTarget.z == 0) && (Mathf.Abs(relPos.x) <= 15))){
			
			keyPosActual+=1;
			if (keyPosActual == rutaActual.Count)
				keyPosActual = 0;
		}
	}
	
	/*################################################################
	############################# STATUS ############################*/
	
	protected void UpdateNoneState(){
		//Animation idle
		animateIfExist("paradaDerecha","paradaIzquierda");
		updateNextTarget();
		if (nextTarget.z != 3){
			curState = FSM.Run;
		}
	}
	protected void UpdateRunState(){		
			// Si esta activa la animacion de girar, no me puedo mover
			if (animations.IsPlaying(((derecha)? "giroIzqDer":"giroDerIzq"))){
				return;
			}
		
			if (nextTarget.z == 3){
				animateIfExist("paradaDerecha","paradaIzquierda");
				updateNextTarget();
				return;
			}
		
			bool ground = onGround();
		

		
			// Sistema para que no cambie de direccion muy rapido cuando el pollo se encuentre en
			// las mismas x, pero en diferentes y. 
			if (!changeDir){
				float posX = getPosition().x - lastPos.x;
				if(Mathf.Abs(posX) >= 80) {
					changeDir = true;	// Permite cambiar de direccion
				}
			}
		 
		
			// Cambio de direccion si changeDir me lo permite, y estoy de espaldas al target
			if ((!targetEnemy || changeDir) && derecha != (relPos.x > 0)){
				derecha = !derecha;
				lastPos = getPosition();
				changeDir = false;	// No voy a permitir cambiar de direccion en la siguiente iteracion
				if(ground){
					animateIfExist("giroIzqDer","giroDerIzq");
					return;
				}
			}
			
			if (nextTarget.z == 2 && ground){
				//curState = FSM.Jump;
				jump();
				return;
			}

			// Si esta tocando suelo:
			if(ground)
				animateIfExist("correrDerecha","correrIzquierda");		
			// Si esta caiendo:
			else if (rigidbody.velocity.y < -10)
				animateIfExist("caidaDerecha","caidaIzquierda");
			// else -> estoy haciendo la animacion de salto

		
			// Camino:
			if (nextTarget.z < 1 || Mathf.Abs(relPos.x) > 15)
				transform.Translate(new Vector3((derecha)?velocity:-velocity,0,0) * Time.deltaTime);

		
		
		
			// Ha detectado algo a distancia "stopDistance" delante del player?:
			switch(getWeapon()){
				case WEAPON_KATANA:
						rangeWeapon = 0;
						break;
				case WEAPON_PISTOLA:
						rangeWeapon = 300;
						break;
				case WEAPON_ESCOPETA:
						rangeWeapon = 100;
						break;
				default:
						rangeWeapon = 0;
						break;
			}

			GameObject detected = raycastFront(stopDistance+rangeWeapon);
			if(detected != null){	
				switch(detected.tag){
				 	case "Player":
					case "NPC":
					case "Allied":
						Actor a = detected.GetComponent(typeof(Actor)) as Actor;
						if(isEnemy(a)){ // Comprobar rivales
							curState = FSM.Attack;
							//animateIfExist("atacarDer","atacarIzq");
						}
						break;
				
					default:
						break;
				}

			}
		
			detected = raycastFront(stopDistance);
			if(detected != null){	
				switch(detected.tag){
				 	case "Player":
					case "NPC":
					case "Allied":
						Actor a = detected.GetComponent(typeof(Actor)) as Actor;
						if(isEnemy(a)){ // Comprobar rivales
							curState = FSM.Attack;
							//animateIfExist("atacarDer","atacarIzq");
						} else {
							curState = FSM.Jump;
						}
						break;
				
					default:
						curState = FSM.Jump;
						//print("There is something in front of the object!");
						//Invoke("UpdateJumpState",0.4f);
						break;
				}

			}
		
			updateNextTarget();

	}
	protected void UpdateAttackState(){
		//Debug.Log("WEAPON: "+getWeapon());
		string anim = (derecha)? "atacarDer":"atacarIzq";
		
		if(getWeapon() == WEAPON_KATANA){

			
			GameObject detected = raycastFront(stopDistance+rangeWeapon);

			if(detected != null){
				Actor actor = detected.GetComponent(typeof(Actor)) as Actor;
				if(isEnemy(actor)){

					if (!animations.IsPlaying(anim)){
						animations.Play(anim);
						haAtacado = false;
					} else if (!haAtacado && animations[anim].time > (animations[anim].length*2f/3f)){
						haAtacado = true;
						actor.dealDamage(25);
						p.mostrarDany();
					}

				} else {
					if (!animations.IsPlaying(anim))
						curState = FSM.Run;
				}
				
			} else {
				if (!animations.IsPlaying(anim))
					curState = FSM.Run;
			}


		}
		
		if(getWeapon() == WEAPON_PISTOLA){
			
			//Animacion disparar

			

			GameObject detected = raycastFront(stopDistance+rangeWeapon);
			if(detected != null){
				Actor actor = (Actor) detected.GetComponent(typeof(Actor));
				if (isEnemy(actor)){
					

					if (!animations.IsPlaying (anim)){
						animations.Play(anim);
						haAtacado = false;
					} else if (!haAtacado && animations[anim].time > 0.5f){
						haAtacado = true;
						
						if(derecha){
							nouTir = (GameObject) Instantiate(bala, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
							nouTir.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
						}else{
							nouTir = (GameObject) Instantiate(bala, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
							nouTir.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
						}
						
						GestioTir b = nouTir.GetComponent("GestioTir") as GestioTir;
						b.setEquip(team);
						b.setArma(weapon);
						
						sonidoDisparoPistola.Play();
						
					}
					

				} else {
						curState = FSM.Run;
				}
			} else {
				curState = FSM.Run;
			}
			
		}
		
		if(getWeapon() == WEAPON_ESCOPETA){
			//Animacion disparar
			

			GameObject detected = raycastFront(stopDistance+rangeWeapon);
			if(detected != null){
				Actor actor = (Actor) detected.GetComponent(typeof(Actor));
				if (isEnemy(actor)){
					
					if (!animations.IsPlaying (anim)){
						animations.Play(anim);
						haAtacado = false;
					} else if (!haAtacado && animations[anim].time > 0.5f){
						GameObject nouTir = null;
						haAtacado = true;
						if(derecha){
							nouTir = (GameObject) Instantiate(bala, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
							nouTir.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange);
						}else{
							nouTir = (GameObject) Instantiate(bala, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
							nouTir.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange);
						}
						
						GestioTir b = nouTir.GetComponent("GestioTir") as GestioTir;
						b.setEquip(team);
						b.setArma(weapon);
						
						sonidoDisparoEscopeta.Play();
						
					}
					

				} else {
						curState = FSM.Run;
				}
			} else {
				curState = FSM.Run;
			}
					

		}
		
	}
	
    protected void UpdateJumpState(){

		
			GameObject detected = raycastFront(stopDistance+5);
			if(detected != null){
			
				switch(detected.tag){
				 	case "Player":
					case "NPC":
					case "Allied":
						Actor a = detected.GetComponent(typeof(Actor)) as Actor;
						if(isEnemy(a)){ // Comprobar rivales
							curState = FSM.Attack;
							//animateIfExist("atacarDer","atacarIzq");
						} else {
							jump();
						}
						break;
					default:
						jump ();
						//print("There is something in front of the object!");
						//Invoke("UpdateJumpState",0.4f);
						break;
				}

			} else
				curState = FSM.Run;
		
		
	}
	protected void UpdateDeadState(){
		//Animacio morirs
		if(!animations.IsPlaying((derecha)?"muerteDerecha":"muerteIzquierda")){
			if (!dead){
				animateIfExist("muerteDerecha","muerteIzquierda");
				dead = true;
				if (playerController.getTeam() != team)
					notifyDeadtoPlayer();
			}else
				Destroy(gameObject);
		}
		
		
		// Dar puntos a player
		
		/*
		setInitialsAttributes();
		transform.position = spawnPoint;
		*/
		
	}
	
	private void notifyDeadtoPlayer() {
		playerController.notifyDeadNPC();
	}

	
	/*################################################################
	###################### GESTIO DE COLLISIONS ######################*/
	
//	void OnCollisionExit(Collision collision){}
//	void OnCollisionStay(Collision collision){}
//	void OnCollisionEnter(Collision collision){}
	
	/*################################################################
	######################## UTILITY FUNCTIONS ######################*/
	
	public bool jump(){
		
		//Si esta tocando suelo -> Salta
		if (onGround() && rigidbody.velocity.y < jumpForce/2){	
			
			rigidbody.velocity = rigidbody.velocity + Vector3.up *jumpForce;	

			animateIfExist ("saltoVerticalDer","saltoVerticalIzq");

			return true;
			
		//Si esta caiendo:
		} else if (rigidbody.velocity.y < -10)
			animateIfExist("caidaDerecha","caidaIzquierda");
		return false;
	}
	
	
	private bool animateIfExist(string der,string izq){
		string anim = (derecha)?der:izq;

		if (animations[anim]!=null){
			//Debug.Log("PLAYING ANIMATION - "+anim);	
			animations.Play(anim);
		}else{
			//print("ANIMATION DOESNT EXIST - "+anim);
			return false;
		}
		return true;
	}
	
	
	
	private float distanceX(Vector3 a, Vector3 b){
		return Mathf.Abs (a.x - b.x);
	}
	private float distance3D(Vector3 a, Vector3 b){
		float distX = a.x - b.x, distY = a.y - b.y;
		return Mathf.Sqrt (distX*distX + distY*distY);
	}
	private GameObject raycastFront(int dist){
		RaycastHit hit;
		float mitadAltura = rigidbody.collider.bounds.extents.y*0.7f;
		Vector3 pos = getPosition();
		bool trobat = false;
		for (int i=0;i<3 && !trobat;i++){
			if(Physics.Raycast(pos, (derecha)?Vector3.right:Vector3.left, out hit,dist))
				trobat = true;
			else
				pos+=Vector3.up*mitadAltura;
		}
		if (!trobat)
			return null;
		return hit.collider.gameObject;
	}
	private bool anythingOn(Vector3 pos){
		Vector3 vec = new Vector3(pos.x,pos.y,-200);
		Vector3 vec2 = vec; vec2.z = 200;
		return Physics.Linecast(vec,vec2);
	}
	private bool onGround(){
		float mitadAmplada = rigidbody.collider.bounds.extents.x;
		bool ground = false;
		for (int i=-1;i<2 && !ground;i++){
			ground = Physics.Raycast(getPosition()+Vector3.right*mitadAmplada*i, Vector3.down, 3);
		}
		return ground;
	}
	private bool isEnemy(Actor a){
		if (a == null) return false;
		return getTeam() != a.getTeam();
	}
	
	protected override void updateModelWeapon(){
		if (weap_mod == null){
			weap_mod = new GameObject[3];
			weap_mod[WEAPON_KATANA-1]   = GameObject.Find(gameObject.name+"/grk");
			weap_mod[WEAPON_ESCOPETA-1] = GameObject.Find(gameObject.name+"/gre");
			weap_mod[WEAPON_PISTOLA-1]  = GameObject.Find(gameObject.name+"/grp");
			initAnimationsSettings();
		}
		
		for (int i=0;i< weap_mod.Length; i++){

			if ((weapon-1) == i)
				weap_mod[i].SetActive(true);
			else
				//Destroy(weap_mod[i]);			
				weap_mod[i].SetActive(false);
			
			animations = weap_mod[weapon-1].animation;
		}
		
		if (weapon == WEAPON_ESCOPETA)
			bala = Resources.Load("ChickenPrefabs/weapons/balaEscopeta") as GameObject;
		
		else if (weapon == WEAPON_PISTOLA)
			bala = Resources.Load("ChickenPrefabs/weapons/balaPistola") as GameObject;
		
	}
	
	void initAnimationsSettings(){
		for (int i=0;i<weap_mod.Length; i++){

			weap_mod[i].animation["correrDerecha"].wrapMode = WrapMode.Loop;
			weap_mod[i].animation["correrIzquierda"].wrapMode = WrapMode.Loop;
			
			weap_mod[i].animation["caidaDerecha"].speed = 1.5f;
			weap_mod[i].animation["caidaIzquierda"].speed = 1.5f;
			
			weap_mod[i].animation["atacarDer"].speed = 1f;
			weap_mod[i].animation["atacarIzq"].speed = 1f;
			
			weap_mod[i].animation["muerteDerecha"].speed = 1.5f;
			weap_mod[i].animation["muerteIzquierda"].speed = 1.5f;

			weap_mod[i].animation["giroIzqDer"].speed = 5f;
			weap_mod[i].animation["giroDerIzq"].speed = 5f;
		}


	}
	
	private Vector3 getPosition(){
		if(getTeam() == PHILO_TEAM){
			return transform.position;
		} else {
			return transform.position + Vector3.down*(rigidbody.collider.bounds.extents.y-0.1f);
		}
	}
}

