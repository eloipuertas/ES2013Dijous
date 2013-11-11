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
	
	//Next target 
	protected Vector3 nextTarget;
	protected Vector3 actualTarget;
	
	protected Vector3 direction;
	protected Vector3 relPos;
	public string direrutas = "C:/Users/ARocafort/Desktop/codibo/ES2013Dijous-devel-A/ChicknDestroy/unity/Mainmenu Scene/Assets/Scripts/RutasNpc";
	//Lectors de rutes
	private List <List<Vector3>> rutas = new List<List<Vector3>>();
	private List <Vector3> rutaActual  = new List<Vector3>();
	private int keyPosActual =0; 
	private int idruta = 0;
	//Npc propierties
	private Vector3 spawnPoint;
	private Collider col;
	private Rigidbody mas;
	private GameObject pla;
	private PlayerController playerScript;
	
	//Npc atributes
	
	public int health = 100;
	public int damage = 15;
	public int puntuacio = 0;
	public string primaryWeapon;
	public string secondaryWeapon;
	
	public float velocity = 1f;
	private bool Dead = false;
	
	
	private float dist;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	
	private Animator animator;
	private bool derecha = true;
	private bool canvia = false;
	public float stopDistance = 90;
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
//		//pla = GameObject.FindGameObjectWithTag("Player");
//		//playerScript = (PlayerController) pla.GetComponent(typeof(PlayerController));
		
		nextTarget = rutas[idruta][keyPosActual];
		Debug.Log("####NPC GO TO -------> "+nextTarget);
		relPos = nextTarget - transform.position;
		if(Mathf.Abs(relPos.x) <= 15) {
			keyPosActual+=1;
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
		
		foreach (string file in Directory.GetFiles(direrutas, "*.txt")){
			Debug.Log("NOVA RUTA-->ID::"+fileindex);
			string content = File.ReadAllText(file);
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
		}
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
			if(animation["Mover_Derecha"]!=null){
						//animation["Mover_Derecha"].wrapMode = WrapMode.Loop;
						animation["Mover_Derecha"].speed = 3.0f;
						animation.Play("Mover_Derecha");
						
			}

		
			

			if (derecha != (relPos.x > 0)){
				transform.Rotate (0,180,0);
				derecha = !derecha;			
			}
			
			transform.Translate(new Vector3(velocity,0,0) * Time.deltaTime);
	
		    //Si el objectiu canvia de lloc
			
//				if((transform.position.x - nextTarget.x)<stopDistance){  
//					if(derecha){	
//						animation.Play("Giro_Derecha");
//						while(animation.IsPlaying("Giro_Derecha"));
//						derecha = false;
//					}
//		  			transform.rotation = Quaternion.Euler(0, 0, 0);					
//				}
//				if((transform.position.x - nextTarget.x)>stopDistance){  
//					if(!derecha){	
//						animation.Play("Giro_Derecha");
//						while(animation.IsPlaying("Giro_Derecha"));
//						derecha = true;
//					}
//		  			transform.rotation = Quaternion.Euler(0, 180, 0);					
				
			
//				if(transform.position.y > nextTarget.y){
//					Invoke("UpdateJumpState",2);
//				}
				
		
			

					
			
	   
		
		
		updateNextTarget();
	}
	protected void UpdateAttackState(){
		//Destroy(mas);
		if(animation["Ataque_Derecha"]!=null){
			animation.Play("Ataque_Derecha");
			while(animation.IsPlaying("Ataque_Derecha"));
				curState = FSM.Run;
			
			
			
			
			
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
		
		//updatePlayerPosition();
		
	}
	
    protected void UpdateJumpState(){
		Debug.Log("Salte");
//		if(animation["Salto_Derecha"]!=null){
//				animation.Play("Salto_Derecha", PlayMode.StopAll);
//		}
		
		
	}
	protected void UpdateDeadState(){
		//Animacio morirs
		setInitialsAtributes();
		setInitialState();
		transform.position = spawnPoint;
		
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
		if(collision.gameObject.tag == "Player"){ 
				Debug.Log("A tocat player");
				curState = FSM.Attack;
 		}
		if(collision.gameObject.tag =="katana"){
			setHealthPoints(getHealthPoints()-25);
		}
		
		
	}
//	void OnCollisionExit(Collision collision){
//		
//	}
//	void OnCollisionStay(Collision collision){
//		
//	}
	
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

}

