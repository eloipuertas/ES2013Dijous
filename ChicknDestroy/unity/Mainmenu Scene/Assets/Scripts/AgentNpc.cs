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

	
	//Lectors de rutes
	private int keyPosActual =0; 
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
	
	//Npc propierties
	private Vector3 spawnPoint;
	private Collider col;
	private Rigidbody mas;

	//Status
	private FSM curState;
	private Animator animator;
	private bool derecha = true;
	

	
	private Vector3 lastPos;
	private bool changeDir = true;
	

	//-------------Utility Functions---------------
	
	//Actualiza la posicio del objectiu

	void updateNextTarget(){
		//Raycast en esfera. Para detectar multiples enemigos
		Collider[] colls = Physics.OverlapSphere(transform.position,radioVision);

		GameObject closestEnemy = null;
		float d,distance = float.PositiveInfinity;
		// detecta el enemigo mas cercano
		for (int i=0; i<colls.Length; i++){
			if(colls[i].CompareTag("Player")){ // Comprobar rivales
				d = distance3D(transform.position, colls[i].transform.position);
				if (d < distance){
					d = distance;
					closestEnemy = colls[i].gameObject;
				}
			}
		}
		
		if (closestEnemy != null){
			nextTarget = closestEnemy.transform.position;
			nextTarget.z = 0;
			relPos = nextTarget - transform.position;
			targetEnemy = true;
			return;
		}
		
		targetEnemy = false;
		nextTarget = rutaActual[keyPosActual];
		Debug.Log("####NPC GO TO -------> "+nextTarget);
		relPos = nextTarget - transform.position;
		bool next = false;
		
		// Pasar al siguiente target.
			// Si z=3 espera a que haya algo en (x,y)
		if (((nextTarget.z == 3) && (anythingOn(nextTarget))) ||			
			// Si z=2 debera acercarse al punto (x,y) almenos en 30
			((nextTarget.z == 2) && (distance3D(nextTarget,transform.position) <= 30)) ||
			// Si z=1 debera estar cerca respecto al eje x almenos en 15, y tendra que estar tocando el suelo
			((nextTarget.z == 1) && (Mathf.Abs(relPos.x) <= 15) && onGround()) || 
			// Si z=0 debera estar cerca respecto al eje x almenos en 15
			((nextTarget.z == 0) && (Mathf.Abs(relPos.x) <= 15))){
			
			keyPosActual+=1;
			if (keyPosActual == rutaActual.Count)
				keyPosActual = 0;
		}

	
	}
	
	GameObject warpNpc(Vector3 p,Vector3 s){
		GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Cube);
		npc.transform.localPosition = p;
		npc.transform.localScale = s;
		return npc;
	}
	void setInitialsAtributes(){
		try{
			animator = GetComponent<Animator>();
			animation["correrDerecha"].wrapMode = WrapMode.Loop;
			animation["correrIzquierda"].wrapMode = WrapMode.Loop;
			animation["golpearKatanaDer"].speed = 1.2f;
			animation["golpearKatanaIzq"].speed = 1.2f;
			
			animation["giroIzquierdaDerecha"].speed = 5f;
			animation["giroDerechaIzq"].speed = 5f;
			this.setHealth(100);
			
		}catch{
			//print("Exception GetComponent animator");
		}
		

		

	}

	void setInitialState(){
		nextTarget = rutaActual[keyPosActual];
		Debug.Log("###INITIAL TARGET"+rutaActual[keyPosActual]);
		curState = FSM.Run;
	}
	

	void loadRoute(){
		rutaActual = new List<Vector3>();
		int fileindex = 0;
		int posindex = 0;
		
		Debug.Log("NOVA RUTA-->ID::"+fileindex);
		
		
		TextAsset bindata= (TextAsset) Resources.Load("routes/"+direrutas, typeof(TextAsset));
		print ("textASSERT: "+bindata);
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
	
	void setWeapon(string tag){
		GameObject arma  ;
	}
	void setInitialCollider(){
		col = GetComponent<Collider>();
		Debug.Log(col);
		mas = gameObject.rigidbody;
		Debug.Log(mas);
		
		//mas.GetComponent(RigidBody);
	}

	
	

	
	// ######### Agent arquitecture #########//
	
	//--------Agents States------------//
	
	//#########################################
	//##NOMES CALCULS AMB RIGIDBODY & COLLIDERS
	//#########################################
	
	
	//#########################################
	//###NOMES TRANSFORMACIONS DE POSICIONS####
	//#########################################
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
			if (animation.IsPlaying(((derecha)? "giroIzquierdaDerecha":"giroDerechaIzq"))){
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
				float posX = transform.position.x - lastPos.x;
				if(Mathf.Abs(posX) >= 80) {
					changeDir = true;	// Permite cambiar de direccion
				}
			}
		 
		
			// Cambio de direccion si changeDir me lo permite, y estoy de espaldas al target
			if ((!targetEnemy || changeDir) && derecha != (relPos.x > 0)){
				derecha = !derecha;
				lastPos = transform.position;
				changeDir = false;	// No voy a permitir cambiar de direccion en la siguiente iteracion
				if(ground){
					animateIfExist("giroIzquierdaDerecha","giroDerechaIzq");
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
			GameObject detected = raycastFront(stopDistance);
			if(detected != null){	
				switch(detected.tag){
					case "Player":
						curState = FSM.Attack;
						animateIfExist("golpearKatanaDer","golpearKatanaIzq");
						
						break;
					case "NPC":
						curState = FSM.Jump;

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
		//Destroy(mas);
		string anim = (derecha)? "golpearKatanaDer":"golpearKatanaIzq";
		
		if(!animation.IsPlaying(anim) && animation[anim]!=null){
			
			GameObject detected = raycastFront(stopDistance+5);
			if(detected != null){	
				if (detected.tag == "Player"){
						PlayerController playerScript = (PlayerController) detected.GetComponent(typeof(PlayerController));
					
						animation.Play(anim);
						playerScript.dealDamage(25);
					
						print("HIT");
				} else {
						curState = FSM.Run;
				}
				
			} else {
				curState = FSM.Run;
			}
		}
		
		
		
	}
	
    protected void UpdateJumpState(){
		Debug.Log("Salte");

		
			GameObject detected = raycastFront(stopDistance+5);
			if(detected != null){
			
				switch(detected.tag){
					case "Player":
						curState = FSM.Attack;
						animateIfExist("golpearKatanaDer","golpearKatanaIzq");
				
						//print("Player in front me!");
					 	//Invoke("UpdateAttackState",0.5f);
						break;
					case "NPC":
						jump ();
						//GameObject npc = GameObject.FindGameObjectWithTag("NPC");
						//Physics.IgnoreCollision(npc.collider,col);
						break;
					default:
						jump ();
						//print("There is something in front of the object!");
						//Invoke("UpdateJumpState",0.4f);
						break;
				}

			} else
				curState = FSM.Run;
		
//		if(animation["Salto_Derecha"]!=null){
//				animation.Play("Salto_Derecha", PlayMode.StopAll);
//		}
		
	}
	protected void UpdateDeadState(){
		//Animacio morirs
		Destroy(gameObject);
		// Dar puntos a player
		
		/*
		setInitialsAtributes();
		setInitialState();
		transform.position = spawnPoint;
		*/
		
	}
	//-----------Agent Perceptions------------
	protected override void FSMUpdate(){
		switch (curState){
			case FSM.None: UpdateNoneState(); break;
			case FSM.Jump: UpdateJumpState(); break;
			case FSM.Run: UpdateRunState(); break;
			case FSM.Attack: UpdateAttackState(); break;
			case FSM.Dead: UpdateDeadState(); break;

 		}
		
 		//elapsedTime += Time.deltaTime;

 		//Go to dead state is no health left
		//Debug.Log(this.getHealthPoints());
		if (this.getHealth() <= 0) curState = FSM.Dead;
		//if (pla.getHealthPoints() <=0) curState = FSM.None;
		Debug.Log("Current STATE: "+curState);

	}
	
	//-----------Gestio de collisions----------
	
	void OnCollisionEnter(Collision collision){
		/*
		if(collision.gameObject.collider){
			Debug.Log("A tocat alguna cosa");
			
		}
		*/
		
		/*
		if(collision.gameObject.tag == "Player"){ 
				Debug.Log("A tocat player");
				curState = FSM.Attack;
 		}
		if(collision.gameObject.tag =="katana"){
			setHealthPoints(getHealthPoints()-25);
		*/
		
		if(collision.gameObject.tag =="bala"){
		    dealDamage(60);
		}
		
				
	}
//	void OnCollisionExit(Collision collision){}
//	void OnCollisionStay(Collision collision){}
	
	//Initialization of NPC
	protected override void Ini(){
		Physics.gravity = new Vector3(0,-800,0);
		loadRoute();
		setInitialsAtributes();
		setInitialCollider();
		setInitialState();

		updateNextTarget();
		
		Debug.Log(curState);
	}
	
	public bool jump(){
		
		string anim = (derecha)? "caidaDerecha":"caidaIzquierda";
		//Si esta tocando suelo -> Salta
		if (onGround() && rigidbody.velocity.y < jumpForce/2){	
			
			rigidbody.velocity = rigidbody.velocity + Vector3.up *jumpForce;	
			
			anim = (derecha)? "saltoVerticalDer":"saltoVerticalIzq";
			
			if(animation[anim]!=null){
				animation.Play(anim);
				
			}
			return true;
			
		//Si esta caiendo:
		} else if (rigidbody.velocity.y < -10 && animation[anim]!=null)
			animation.Play(anim);
		return false;
	}
	
	
	private bool animateIfExist(string der,string izq){
		string anim = (derecha)?der:izq;
		if (animation[anim]!=null)
			animation.Play(anim);
		else
			return false;
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
		Vector3 pos = transform.position;
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
			ground = Physics.Raycast(transform.position+Vector3.right*mitadAmplada*i, Vector3.down, 3);
		}
		return ground;
	}
	
}

