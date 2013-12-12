using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Text.RegularExpressions;

//[RequireComponent(typeof(Grid))]
public class AgentNpc : FSM {
        

	//Agent finite stats
	public enum FSM{
			None,
			Run,
			Jump,
			Attack,
			Dead,
	}
	
	
	//Modelos de armas
	private GameObject[] weap_mod;
	//Lectors de rutes
	private int keyPosActual = 0;
	public string direrutas = "rutaRival1";
	private List <Vector3> rutaActual = new List<Vector3>();
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
	private int stopDistance = 50;
	private int rangeWeapon = 0;
	private int hpToRun = 30;
	public bool respawn = true;
	
	
	//Npc propierties
	private Vector3 spawnPoint;


	//Status
	private FSM curState;
	private Animation animations;
	private bool derecha = true;
	private int dead = 0;
	private float elapsedTime = 0;
	private bool haAtacado = true;
	

	
	private Vector3 lastPos;
	private bool changeDir = true;
	
	private Vector3[,] grid;
	private float targetTimer = 10f;
	private float timerDead = 0f;
	private string goingTo;
	private string lastZone = null;
	private RouteToZone rtz;
	
	
	/*################################################################
	###################### INITIALIZATION OF NPC #####################*/
	
	protected override void Ini(){
		this.gameManager = (GameManager) (GameObject.Find("Main Camera").GetComponent("GameManager"));
		grid = ((Grid)GameObject.Find ("GameStartUp").GetComponent("Grid")).getGrid();
		this.hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
		rtz = GameObject.Find ("GameStartUp").GetComponent("RouteToZone") as RouteToZone;
		loadRouteParam(getZone(getXGrid(),getYGrid()),Random.Range(1,rtz.thisZoneToThisZones(getZone(getXGrid(),getYGrid())).Length));
		//loadRoute();
		setInitialsAttributes();
		updateModelWeapon();
		updateNextTarget();
		initSounds ();
		initFlagManagement();
		
	}
	
	void setInitialsAttributes(){
		this.currentDirection = DIR_DERECHA;
		
		this.secondary = (ThrowableWeapon)WeaponFactory.instance ().create (WeaponFactory.WeaponType.GRANADE);
		nextTarget = rutaActual[keyPosActual];
		curState = FSM.Run;
				
		this.setHealth(100);
		this.setShield (0);
		sortidaBalaDreta = GameObject.Find(gameObject.name+"/sbd");
		sortidaBalaEsquerra = GameObject.Find(gameObject.name+"/sbe");
		
		p = GameObject.FindGameObjectWithTag("Player").GetComponent("Parpadeig") as Parpadeig;
		//playerController = GameObject.FindGameObjectWithTag("Player").GetComponent("PlayerController") as PlayerController;

	}
	
	void loadRouteParam(string zona,int ruta){
		rutaActual = new List<Vector3>();
		int fileindex = 0;
		int posindex = 0;
		
		
		TextAsset bindata= (TextAsset) Resources.Load("routes/"+zona+"Ruta"+ruta, typeof(TextAsset));
		string content = bindata.text;
		string []lines = content.Split('|');
		foreach(string s in lines){
			if(lines[0] != s){
				string []pos = s.Split(',');
				float x = grid[int.Parse(pos[0]),int.Parse(pos[1])].x;
				float y = grid[int.Parse(pos[0]),int.Parse(pos[1])].y;
				float z = float.Parse(pos[2]);
				rutaActual.Insert(posindex,(Vector3)new Vector3(x,y,z));
				posindex +=1;
			}else{
				goingTo = s;
			}
		}
		content ="";
		posindex = 0;
		
		fileindex +=1;
		lastZone = zona;
	}
	
        
	void loadRoute(){
		rutaActual = new List<Vector3>();
		int fileindex = 0;
		int posindex = 0;
		
		
		TextAsset bindata= (TextAsset) Resources.Load("routes/"+direrutas, typeof(TextAsset));
		string content = bindata.text;
		string []lines = content.Split('|');
		foreach(string s in lines){
			if(lines[0] != s){
				string []pos = s.Split(',');
				float x = grid[int.Parse(pos[0]),int.Parse(pos[1])].x;
				float y = grid[int.Parse(pos[0]),int.Parse(pos[1])].y;
				float z = float.Parse(pos[2]);
				rutaActual.Insert(posindex,(Vector3)new Vector3(x,y,z));
				posindex +=1;
			}else{
				goingTo = s;
			}
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
				case FSM.Run: UpdateRunState();break;
				case FSM.Attack: UpdateAttackState(); break;
				case FSM.Dead: UpdateDeadState(); break;
			}
			elapsedTime += Time.deltaTime;

			if (this.getHealth() <= 0) curState = FSM.Dead;

		}
		/*if(gameObject.name.CompareTo("Robot1")==0){
			print(nextTarget);
		}*/
		
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
			// Si la vida es inferior a "hpToRun". huye!
			if (getHealth() < hpToRun){
				relPos.x *= -1;
			}
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
			((nextTarget.z == 2) && (distance3D(nextTarget,getPosition()) <= 60)) ||
			// Si z=1 debera estar cerca respecto al eje x almenos en 15, y tendra que estar tocando el suelo
			((nextTarget.z == 1) && (Mathf.Abs(relPos.x) <= 50) && onGround()) ||
			// Si z=0 debera estar cerca respecto al eje x almenos en 15
			((nextTarget.z == 0) && (Mathf.Abs(relPos.x) <= 50))){
			
			keyPosActual+=1;
			targetTimer = 10f;
			if (keyPosActual == rutaActual.Count){
				keyPosActual = 0;
				goToRandomZone();
				//loadRandomRoute(getZone(getXGrid(),getYGrid()));
			}
			
		}
		if(targetTimer <= 0){
			keyPosActual = 0;
			targetTimer = 10f;
			goToRandomZone();
			//loadRandomRoute(getZone(getXGrid(),getYGrid()));
		}
		targetTimer-=Time.deltaTime;
		
	}
	
	
	void goToRandomZone(){
		if(lastZone.CompareTo(getZone(getXGrid(),getYGrid())) != 0){
			string [] Zones = rtz.thisZoneToThisZones(getZone(getXGrid(),getYGrid()));
			int randRoute = Random.Range(1,Zones.Length+1);
			while(Zones[randRoute-1].CompareTo(lastZone) == 0 && Zones.Length > 1){
				randRoute = Random.Range(1,Zones.Length+1);
			}
			loadRouteParam(getZone(getXGrid(),getYGrid()),randRoute);
		}
		
	}
	
	void loadRandomRoute(string zone){
		int randRoute = Random.Range(1,rtz.thisZoneToThisZones(zone).Length);
		loadRouteParam(zone,randRoute);
		
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
				changeDir = true;        // Permite cambiar de direccion
			}
		}

		bool girar = false;
		// Cambio de direccion si changeDir me lo permite, y estoy de espaldas al target
		if ((!targetEnemy || changeDir) && derecha != (relPos.x > 0)){
			derecha = !derecha;
			if (derecha) this.currentDirection = DIR_DERECHA; // Necessary to refactor the attack methods.
			else this.currentDirection = DIR_IZQUIERDA;
			lastPos = getPosition();
			changeDir = false;        // No voy a permitir cambiar de direccion en la siguiente iteracion
			if(ground){
				girar = true;
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
		GameObject detected = raycastFront(stopDistance+rangeWeapon);
		if(detected != null){        
			switch(detected.tag){
				case "Player":
				case "NPC":
				case "Allied":
					Actor a = detected.GetComponent(typeof(Actor)) as Actor;
					if(isEnemy(a) && getHealth()>=hpToRun){ // Comprobar rivales
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
						if (getHealth()>=hpToRun)
							curState = FSM.Attack;
								//animateIfExist("atacarDer","atacarIzq");
					} else if (derecha){
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
		if(girar)
			animateIfExist("giroIzqDer","giroDerIzq");
		
		updateNextTarget();

	}
    protected void UpdateAttackState(){
		if (getHealth()<hpToRun){
			curState = FSM.Run;
			return;
		}
        //Debug.Log("WEAPON: "+getWeapon());
        string anim = (derecha)? "atacarDer":"atacarIzq";
		if(this.primary.GetType () == typeof(MeleeWeapon)) {
            GameObject detected = raycastFront(stopDistance+rangeWeapon);
            if(detected != null){
                Actor actor = detected.GetComponent(typeof(Actor)) as Actor;
                if(isEnemy(actor)){
                    if (!animations.IsPlaying(anim)){
                        animations.Play(anim);
                        haAtacado = false;
                    } else if (!haAtacado && animations[anim].time > (animations[anim].length*2f/3f)){
                        haAtacado = true;
						if(this.primary.attack ()) {
	                        actor.dealDamage(this.primary.getDamage());
	                        if(actor.GetType() == typeof(PlayerController)) p.mostrarDany();
						}
                    }
                } else {
                    if (!animations.IsPlaying(anim))
                            curState = FSM.Run;
                }
            } else {
                if (!animations.IsPlaying(anim))
                        curState = FSM.Run;
            }
		} else if (this.primary.GetType () == typeof(DistanceWeapon)) {
			GameObject detected = raycastFront(stopDistance+rangeWeapon);
			if(detected != null){
				Actor actor = (Actor) detected.GetComponent(typeof(Actor));
				if (isEnemy(actor)){
					if (!animations.IsPlaying (anim)){
						animations.Play(anim);
						haAtacado = false;
					} else if (!haAtacado && animations[anim].time > 0.5f){
						haAtacado = true;
						doPrimaryAttack ();
						//if(doPrimaryAttack())haAtacado = true;
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
                    } else if (derecha) {
                            jump();
                    }
                    break;
	            default:
                    jump ();
                    //print("There is something in front of the object!");
                    //Invoke("UpdateJumpState",0.4f);
                    break;
            }
	
	    } else curState = FSM.Run;
                
    }
    protected void UpdateDeadState(){
		//Animacio morirs
		switch (dead){
			case 0:
				animateIfExist("muerteDerecha","muerteIzquierda");
				dead = 1;
				int t;
				if (this.team == 1) t = 2;
				else t = 1;
				notifyHudPoints(t,100);
				break;
			case 1:
				if(!animations.IsPlaying((derecha)?"muerteDerecha":"muerteIzquierda")){
					if (!respawn){
						Destroy(gameObject);
						return;
					}
					weap_mod[weapon-1].SetActive(false);	// desaparece visualmente
					rigidbody.isKinematic = true;			// no caigas al infinito... (ya que no tiene collider)
					collider.enabled = false;				// fuera Collider
					dead = 2;
					timerDead = 50f;
				}
				break;
			default: //case 2:
				Vector3 playerDist = GameObject.FindGameObjectWithTag("Player").transform.position;
				if (timerDead < 0 && distance3D(playerDist,getPosition()) > 2000){
				// RESURECCION!
					heal (100);
					weap_mod[weapon-1].SetActive(true);
					collider.enabled = true;
					rigidbody.isKinematic = false;
					dead = 0;
					curState = FSM.Run;
				} else
					timerDead -= Time.deltaTime;
				break;
			
		}

		

		
		
		// Dar puntos a player
		
		/*
		setInitialsAttributes();
		transform.position = spawnPoint;
		*/
		
    }
        
	private void notifyDeadtoPlayer() {
		//playerController.notifyHudPoints(100);
	}

        
        /*################################################################
        ###################### GESTIO DE COLLISIONS ######################*/
        
//        void OnCollisionExit(Collision collision){}
//        void OnCollisionStay(Collision collision){}
//        void OnCollisionEnter(Collision collision){}
        
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

		Vector3 pos = getPosition();
		Vector3 posAux = pos;
		Vector3 endpos;
		bool trobat = false;
		float altura = rigidbody.collider.bounds.extents.y * 2;
		int ndiv = 3;
		float deltaAltura = (altura-5.0f)/(float)ndiv;
		
		pos = pos + Vector3.down*(altura/2.0f);
		
		for (int i=1;i <= ndiv && !trobat; i+=1){
			
			posAux = pos + Vector3.up*i*deltaAltura;
			
			endpos = posAux + ((derecha)?Vector3.right:Vector3.left) * dist;
			Debug.DrawLine(posAux, endpos, Color.blue, 0.01f);
			if(Physics.Raycast(posAux, (derecha)?Vector3.right:Vector3.left, out hit,dist) && notAPickUpObject(hit.collider.gameObject))
			trobat = true;
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
		float ancho = rigidbody.collider.bounds.extents.x * 2;
		float alto = rigidbody.collider.bounds.extents.y;
		Vector3 pos = getPosition() + Vector3.left*(ancho/2.0f);
		Vector3 posAux;
		
		int ndiv = 3;
		float deltaAncho = ancho/(float)ndiv;
		
		bool ground = false;
		for (int i=0;i<ndiv && !ground; i+=1){
			posAux = pos + Vector3.right*i*deltaAncho;
			ground = Physics.Raycast(posAux, Vector3.down, alto);
			Debug.DrawLine(posAux, posAux + Vector3.down*(alto), Color.red, 0.01f);
		}
		return ground;
	}

        
	public void updateModelWeapon(){
		if (weap_mod == null){
			weap_mod = new GameObject[3];
			weap_mod[WEAPON_KATANA-1] = GameObject.Find(gameObject.name+"/grk");
			weap_mod[WEAPON_ESCOPETA-1] = GameObject.Find(gameObject.name+"/gre");
			weap_mod[WEAPON_PISTOLA-1] = GameObject.Find(gameObject.name+"/grp");
			initAnimationsSettings();
		}
			
		for (int i=0;i< weap_mod.Length; i++){

			if ((weapon-1) == i)
					weap_mod[i].SetActive(true);
			else
					//Destroy(weap_mod[i]);                        
					weap_mod[i].SetActive(false);
		}
		for (int i=0;i< weap_mod.Length; i++){
			weap_mod[weapon-1].animation.cullingType = AnimationCullingType.AlwaysAnimate;
		}
		
			
		animations = weap_mod[weapon-1].animation;
		
		if (weapon == WEAPON_ESCOPETA){
			bala = Resources.Load("ChickenPrefabs/weapons/balaEscopeta") as GameObject;
			rangeWeapon = 100;
		}else if (weapon == WEAPON_PISTOLA){
			bala = Resources.Load("ChickenPrefabs/weapons/balaPistola") as GameObject;
			rangeWeapon = 200;
		}else
			rangeWeapon = 0;
                
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
		//if(getTeam() == PHILO_TEAM){
				return transform.position;
		//} else {
		//        return transform.position + Vector3.down*(rigidbody.collider.bounds.extents.y-0.1f);
		//}
	}
		
	private int getXGrid(){//return the X grid position where gameobject is at the scene 
		int x;
		x = (int)gameObject.transform.position.x;
		x = x-(-1221);//-1221 is 0 X value
		
		x = x/200;
		if(x>89)//error control
			x = 89;
		
		return x;
	}
	
	private int getYGrid(){//return the Y grid position where gameobject is at the scene 
		int y;
		y = (int)gameObject.transform.position.y;
		y = y-(-110);//-110 is 0 Y value
		
		y = y/200;
		if(y>6)//error control
			y = 6;
		
		return y;
	}
	
	private string getZone(int x,int y){//Tranlate X and Y grid position to zone
		if((x>=0 && x<=6) && (y>=0 && y<=3))
			return "Zona1";
		if((x>=0 && x<=7) && (y>=4 && y<=6))
			return "Zona2";
		if((x>=7 && x<=80) && (y>=0 && y<=0))
			return "Zona3";
		if((x>=8 && x<=80) && (y>=1 && y<=1))
			return "Zona4";
		if((x>=16 && x<=31) && (y>=2 && y<=2))
			return "Zona5";
		if((x>=17 && x<=36) && (y>=3 && y<=4))
			return "Zona6";
		if((x>=41 && x<=47) && (y>=2 && y<=4))
			return "Zona7";
		if((x>=50 && x<=72) && (y>=2 && y<=2))
			return "Zona8";
		if((x>=74 && x<=77) && (y>=2 && y<=4))
			return "Zona9";
		if((x>=78 && x<=80) && (y>=2 && y<=3))
			return "Zona10";
		if((x>=81 && x<=88) && (y>=0 && y<=3))
			return "Zona11";
		if((x>=82 && x<=88) && (y>=4 && y<=6))
			return "Zona12";
		if((x>=77 && x<=81) && (y>=5 && y<=6))
			return "Zona13";
		if((x>=53 && x<=71) && (y>=3 && y<=5))
			return "Zona14";
		if((x>=7 && x<=9) && (y>=2 && y<=3))
			return "Zona15";
		if((x>=8 && x<=11) && (y>=5 && y<=6))
			return "Zona16";
		if((x>=10 && x<=15) && (y>=2 && y<=3))
			return "Zona17";
		
		return "Zona0";
	}
	
	private bool notAPickUpObject(GameObject o){//Check if the object is an able to be picked up object
		bool weapon = true;
		
		if(new Regex("escopeta.").IsMatch(o.name))
			weapon = false;
		if(new Regex("gun.").IsMatch(o.name))
			weapon = false;
		if(new Regex("katana.").IsMatch(o.name))
			weapon = false;
		if(new Regex("granada.").IsMatch(o.name))
			weapon = false;
		if(new Regex("lifeUp.").IsMatch(o.name))
			weapon = false;
		if(new Regex("shield.").IsMatch(o.name))
			weapon = false;
		if(new Regex("Bandera.").IsMatch(o.name))
			weapon = false;
		return weapon;
	}
	
	private string[] getNeighbors(string zona){//returns de Neighbors of a zone
		string [] neighbors;
		
		switch(zona){
			case "Zona1":
			neighbors = new string[3];
			neighbors[0] = "Zona3";neighbors[1] = "Zona14";neighbors[2] = "Zona15";
			break;
			
			case "Zona2":
			neighbors = new string[2];
			neighbors[0] = "Zona15";neighbors[1] = "Zona16";
			break;
			
			case "Zona3":
			neighbors = new string[4];
			neighbors[0] = "Zona1";neighbors[1] = "Zona4";neighbors[2] = "Zona8";neighbors[3] = "Zona10";
			break;
			
			case "Zona4":
			neighbors = new string[7];
			neighbors[0] = "Zona1";neighbors[1] = "Zona3";neighbors[2] = "Zona5";neighbors[3] = "Zona6";neighbors[4] = "Zona7";neighbors[5] = "Zona11";neighbors[6] = "Zona17";
			break;
			
			case "Zona5":
			neighbors = new string[3];
			neighbors[0] = "Zona4";neighbors[1] = "Zona6";neighbors[2] = "Zona17";
			break;
			
			case "Zona6":
			neighbors = new string[2];
			neighbors[0] = "Zona4";neighbors[0] = "Zona5";
			break;
			
			case "Zona7":
			neighbors = new string[1];
			neighbors[0] = "Zona4";
			break;
			
			case "Zona8":
			neighbors = new string[4];
			neighbors[0] = "Zona3";neighbors[1] = "Zona9";neighbors[2] = "Zona14";
			break;
			
			case "Zona9":
			neighbors = new string[2];
			neighbors[0] = "Zona8";neighbors[1] = "Zona10";
			break;
			
			case "Zona10":
			neighbors = new string[3];
			neighbors[0] = "Zona9";neighbors[1] = "Zona11";neighbors[2] = "Zona12";
			break;
			
			case "Zona11":
			neighbors = new string[3];
			neighbors[0] = "Zona3";neighbors[1] = "Zona4";neighbors[2] = "Zona10";
			break;
			
			case "Zona12":
			neighbors = new string[2];
			neighbors[0] = "Zona10";neighbors[1] = "Zona13";
			break;
			
			case "Zona13":
			neighbors = new string[1];
			neighbors[0] = "Zona12";
			break;
			
			case "Zona14":
			neighbors = new string[1];
			neighbors[0] = "Zona8";
			break;
			
			case "Zona15":
			neighbors = new string[3];
			neighbors[0] = "Zona1";neighbors[1] = "Zona2";neighbors[2] = "Zona17";
			break;
			
			case "Zona16":
			neighbors = new string[1];
			neighbors[0] = "Zona2";
			break;
			
			case "Zona17":
			neighbors = new string[3];
			neighbors[0] = "Zona4";neighbors[1] = "Zona5";neighbors[2] = "Zona15";
			break;
			
			default://default is Zona0
			neighbors = new string[1];
			neighbors[0] = "Zona0";
			break;

		}
		return neighbors;
	}

}

