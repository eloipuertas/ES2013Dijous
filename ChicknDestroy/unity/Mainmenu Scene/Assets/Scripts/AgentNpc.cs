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
	//Player transform
	protected Transform playerTransform;
	private GameObject pla;
	private PlayerController playerScript;
	
	//Next target 
	protected Vector3 nextTarget;
	protected Vector3 actualTarget;
	
	protected Vector3 direction;
	protected Vector3 relPos;
	public string direrutas = "ruta1.txt";
	//Lectors de rutes
	private List <List<Vector3>> rutas = new List<List<Vector3>>();
	private List <Vector3> rutaActual  = new List<Vector3>();
	private int keyPosActual =0; 
	private int idruta = 0;
	//Npc propierties
	private Vector3 spawnPoint;
	private Collider col;
	private Rigidbody mas;
	
	//Npc atributes
	
	public int health = 100;
	public int damage = 15;
	public int puntuacio = 200;
	public int jumpForce = 500;
	public int gravity = -800;
	public int radioVision = 275;
	public string primaryWeapon;
	public string secondaryWeapon;
	
	public float velocity = 175f;
	private bool Dead = false;
	
	
	private float dist;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	private Animator animator;
	private bool derecha = true;
	private bool canvia = false;
	public float stopDistance = 50;
	private bool attacked = false;
	
	// -------NPC interface----------
	
	protected int getHealthPoints(){
		return health;
	}
	protected void setHealthPoints(int n){
		health = n;
	}
	protected string getPrimaryWeapon(){
		return primaryWeapon;
	}
	protected string getSecondaryWeapon(){
		return secondaryWeapon;
	}
	protected Vector3 getCoordinates(){
		return transform.position;
	}
	protected int getPuntuacio(){
		return puntuacio;
	}

	//-------------Utility Functions---------------
	
	//Actualiza la posicio del objectiu
	void printRuta(){
		foreach(Vector3 pos in rutaActual){
			Debug.Log(pos);
		}
	}
	void updateNextTarget(){
		pla = GameObject.FindGameObjectWithTag("Player");
		playerScript = (PlayerController) pla.GetComponent(typeof(PlayerController));
		playerTransform = pla.transform;
		relPos = playerTransform.localPosition - transform.position;
		if(Mathf.Abs(relPos.x) <= radioVision) {
			return;
		}
		
		
		nextTarget = rutas[idruta][keyPosActual];
		Debug.Log("####NPC GO TO -------> "+nextTarget);
		relPos = nextTarget - transform.position;
		if(Mathf.Abs(relPos.x) <= 15) {
			keyPosActual+=1;
			if (keyPosActual == rutas[idruta].Count)
				keyPosActual = 0;
			Debug.Log("###NEXT KEY###");
		}
//		
//		actualTarget = nextTarget;
//		if(keyPosActual == rutas[0].Count) curState = FSM.None;
		
//		relPos = nextTarget - transform.position;		
	}
	
	GameObject warpNpc(Vector3 p,Vector3 s){
		GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Cube);
		npc.transform.localPosition = p;
		npc.transform.localScale = s;
		return npc;
	}
	void setInitialsAtributes(){
		health = 100;
		//damage = 25;
		primaryWeapon = "katana";
		try{
			animator = GetComponent<Animator>();
		}catch{
			//print("Exception GetComponent animator");
		}
		
		pla = GameObject.FindGameObjectWithTag("Player");
		playerScript = (PlayerController) pla.GetComponent(typeof(PlayerController));
		
		Physics.gravity = new Vector3(0, gravity, 0);
		animation["correrDerecha"].wrapMode = WrapMode.Loop;
		animation["correrIzquierda"].wrapMode = WrapMode.Loop;
		animation["golpearKatanaDer"].speed = 0.7f;
		animation["golpearKatanaIzq"].speed = 0.7f;
		
	}
	void setInitialSpawnPoints(){
		spawnPoint = transform.position;
	}
	void setInitialState(){
		nextTarget = rutaActual[keyPosActual];
		Debug.Log("###INITIAL TARGET"+rutaActual[keyPosActual]);
		curState = FSM.Run;
	}
	
	void selectRoute(){
		int op = Random.Range(0,rutas.Count);
		Debug.Log("### NPC HA SELECIONAT RUTA "+op);
		Debug.Log(rutas.Count);
		Debug.Log(rutas[0]);
		int i = 0;
		foreach(Vector3 p in rutas[0]){
			Debug.Log(p);
			rutaActual.Insert(i,p);
			i+=1;
		}
//		
//		
//		//rutaActual = (Vector3[])rutas[op];
//		//Debug.Log("###POSICIONS DE LA RUTA####"+rutaActual);
	}
	void loadRoutes(){
		List<Vector3> positions = new List<Vector3>();
		int fileindex = 0;
		int posindex = 0;
		
		
		
		direrutas = Application.dataPath + "/Scripts/RutasNpc/" + direrutas;

		Debug.Log("NOVA RUTA-->ID::"+fileindex);
		string content = File.ReadAllText(direrutas);
		string []lines = content.Split('|');
		foreach(string s in lines){
			string []pos = s.Split(',');
			//Debug.Log(pos.Length);
			float x = float.Parse(pos[0]);
			float y = float.Parse(pos[1]);
			float z = float.Parse(pos[2]);
			positions.Insert(posindex,(Vector3)new Vector3(x,y,z));
			Debug.Log("ADD NEW POS"+positions[posindex]);
			posindex +=1;
			Debug.Log("NOVA POSICIO -->"+x+" "+y+" "+z);
		}
		rutas.Insert(fileindex,positions);
		
		content ="";
		posindex = 0;
		
		fileindex +=1;

		foreach(List<Vector3> ruta in rutas){
			foreach(Vector3 pos in ruta){
				Debug.Log("posi,"+pos);
			}
		}
		
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
	}
	protected void UpdateRunState(){
			setInitialCollider();
		
			if (derecha != (relPos.x > 0))
				derecha = !derecha;			
			
			string anim = (derecha)? "correrDerecha":"correrIzquierda";
		
			// Si esta tocando suelo:
			if(Physics.Raycast(transform.position, -Vector3.up, 10) && rigidbody.velocity.y < jumpForce/2 && animation[anim]!=null)
				animation.Play(anim);
		
			// Si esta caiendo:
			else if (rigidbody.velocity.y < -10)
					animation.Play((derecha)? "caidaDerecha":"caidaIzquierda");
			// else -> estoy haciendo la animacion de salto
			
			transform.Translate(new Vector3((derecha)?velocity:-velocity,0,0) * Time.deltaTime);
		
	
		
			RaycastHit hit;
			if(Physics.Raycast(transform.position+Vector3.up*30, (derecha)?Vector3.right:Vector3.left, out hit,stopDistance)){	
			
				switch(hit.transform.gameObject.tag){
					case "Player":
						curState = FSM.Attack;
				
						string anima = (derecha)? "golpearKatanaDer":"golpearKatanaIzq";
						if(animation[anima]!=null)
							animation.Play(anima);
						
						//print("Player in front me!");
					 	//Invoke("UpdateAttackState",0.5f);
						break;
					case "NPC":
						curState = FSM.Jump;
						//GameObject npc = GameObject.FindGameObjectWithTag("NPC");
						//Physics.IgnoreCollision(npc.collider,col);
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
			
			RaycastHit hit;
			if(Physics.Raycast(transform.position+Vector3.up * 30, (derecha)?Vector3.right:Vector3.left, out hit,stopDistance+5)){
				if (hit.transform.gameObject.tag == "Player"){
						
						animation.Play(anim);
						playerScript.setDamage(damage);
					
						print("HIT");
				} else {
						curState = FSM.Run;
				}
				
			} else {
				curState = FSM.Run;
			}
		}
		
		
	
		
		
		//Debug.Log("Relpos:"+Mathf.Abs(relPos.x));
		

		
		//this.animation.Stop ("Mover_Izquierda");
		
		/*
		StartCoroutine(WaitAndCallback(0.5f));
		Debug.Log("Time 2 "+Time.time);
		playerScript.setHealthPoints(playerScript.getHealthpoints()-damage);
		Debug.Log("Damage: 25");
		/*if(Vector3.Distance(transform.position,target) >pla.GetComponent<BoxCollider>().size.x * pla.transform.localScale.x+ GetComponent<BoxCollider>().size.x * transform.localScale.x  ){
			Debug.Log("CHANGE TO RUN");
			Debug.Log(Vector3.Distance(transform.position,target));
			Debug.Log(GetComponent<BoxCollider>().size.x * transform.localScale.x);
			curState = FSM.Run;
		}*/
		
		//UpdateRunState();
		
		
	}
	
    protected void UpdateJumpState(){
		Debug.Log("Salte");

		
			RaycastHit hit;
			if(Physics.Raycast(transform.position+Vector3.up * 30, (derecha)?Vector3.right:Vector3.left, out hit,stopDistance)){
			
				switch(hit.transform.gameObject.tag){
					case "Player":
						curState = FSM.Attack;
						string anima = (derecha)? "golpearKatanaDer":"golpearKatanaIzq";
						if(animation[anima]!=null)
							animation.Play(anima);
				
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
		if (this.getHealthPoints() <= 0) curState = FSM.Dead;
		//if (pla.getHealthPoints() <=0) curState = FSM.None;
		Debug.Log("Current STATE: "+curState);

	}
	
	//-----------Gestio de collisions----------
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.collider){
			Debug.Log("A tocat alguna cosa");
			
		}
		/*
		if(collision.gameObject.tag == "Player"){ 
				Debug.Log("A tocat player");
				curState = FSM.Attack;
 		}
		if(collision.gameObject.tag =="katana"){
			setHealthPoints(getHealthPoints()-25);
		*/
	}
//	void OnCollisionExit(Collision collision){}
//	void OnCollisionStay(Collision collision){}
	
	//Initialization of NPC
	protected override void Ini(){
		
		loadRoutes();
		selectRoute();
		
		setInitialsAtributes();
		setInitialCollider();
		setInitialState();
		setInitialSpawnPoints();

		updateNextTarget();
		
		Debug.Log(curState);
	}
	
	public void jump(){
		string anim = (derecha)? "caidaDerecha":"caidaIzquierda";
		//Si esta tocando suelo -> Salta
		if (Physics.Raycast(transform.position, -Vector3.up, 10) && rigidbody.velocity.y < jumpForce/2){			
			rigidbody.velocity = rigidbody.velocity + Vector3.up *jumpForce;	
			
			anim = (derecha)? "saltarDerecha":"saltarIzquierda";
			if(animation[anim]!=null)
				animation.Play(anim);
			
		//Si esta caiendo:
		} else if (rigidbody.velocity.y < -10 && animation[anim]!=null)
			animation.Play(anim);
	}

}

