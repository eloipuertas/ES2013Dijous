using UnityEngine;
using System.Collections;

public class AgentNpc : FSM {
	
	//Agent finite stats
	public enum FSM{
		None,
		Run,
		Jump,
		Attack,
		Dead,
	}
	
	
	//Animations 
	/*
	public Animation movRight;
	public Animation movLeft;
	public Animation rotRight;
	public  Animation rotLeft;
	public Animation jumpRight;
	public  AnimationClip jumpLeft;
	public AnimationClip atack;*/
	
	//Player transform
	protected Transform playerTransform;
	
	//Next target 
	protected Vector3 nextTarget;
	protected Vector3 actualTarget;
	
	protected Vector3 direction;
	protected Vector3 relPos;
	
	
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
	
	public float velocity = 175f;
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
	void updatePlayerPosition(){
		pla = GameObject.FindGameObjectWithTag("Player");
		playerScript = (PlayerController) pla.GetComponent(typeof(PlayerController));
		actualTarget = nextTarget;
		
		nextTarget = pla.transform.localPosition;
		relPos = nextTarget - transform.position;
		
		
		
			
	}
	float getDistanceX(Vector3 npcPos,Vector3 playerPos){
		float distX = 0.0f;
		if (Mathf.Abs(npcPos.x) > Mathf.Abs(playerPos.x)){
		 distX = Mathf.Abs(npcPos.x - playerPos.x);
		}
		if (Mathf.Abs(npcPos.x) < Mathf.Abs(playerPos.x)){
			distX = Mathf.Abs(playerPos.x - npcPos.x);
		}	
		
		return distX;
	}
	
	float getDistanceY(Vector3 npcPos,Vector3 playerPos){
		float distY = 0.0f;
		if (Mathf.Abs(npcPos.y) > Mathf.Abs(playerPos.y)){
		 distY = Mathf.Abs(npcPos.y - playerPos.y);
		}
		if (Mathf.Abs(npcPos.y) < Mathf.Abs(playerPos.y)){
			distY = Mathf.Abs(playerPos.y - npcPos.y);
		}	
		
		return distY;
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
		StartCoroutine("canviaSentit");
		
		
		
	}
	void setInitialSpawnPoints(){
		spawnPoint = transform.position;
	}
	void setInitialState(){
		curState = FSM.Run;
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

	 IEnumerator canviaSentit() {
		if(canvia){
			Debug.Log("##CANVIA sentit##");
        	animation.Play("Giro_Derecha");
        	yield return new WaitForSeconds(animation.clip.length);
		} else yield return null;
    }
	IEnumerator WaitAndCallback(float waitTime){
		Debug.Log("Time "+Time.time);
		yield return new WaitForSeconds(waitTime);
		Debug.Log("Time 3 "+Time.time);
		//gobool = true;
	}

	
	// ######### Agent arquitecture #########//
	
	//--------Agents States------------//
	
	//#########################################
	//##NOMES CALCULS AMB RIGIDBODY & COLLIDERS
	//#########################################
	protected void UpdateFixedNoneState(){
		//Animation idle
	}
	protected void UpdateFixedRunState(){
		//Animation idle
	}
	protected void UpdateFixedJumpState(){
		//Animation idle
		Debug.Log("rigidbody velocity:"+mas.velocity);
		if (Physics.Raycast(transform.position, -Vector3.up, 10) && mas.velocity.y < 500/2){
			//animation.Play("Salto_Derecha", PlayMode.StopAll);
			Debug.Log("Entra salta");
			mas.velocity += Vector3.up *500;	
			
		}
	}
	protected void UpdateFixedAttackState(){
		//Animation idle
	}
	protected void UpdateFixedDeadState(){
		//Animation idle
	}
	
	//#########################################
	//###NOMES TRANSFORMACIONS DE POSICIONS####
	//#########################################
	protected void UpdateNoneState(){
		//Animation idle
	}
	protected void UpdateRunState(){
			
//			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Mover_Derecha"))
//				animator.SetBool("move",false);
			//animation.Play("Correr_Derecha");
		
			setInitialCollider();
			Vector3 relPos = actualTarget - transform.position;
		    //transform.rotation = Quaternion.LookRotation(relPos);
			//transform.LookAt(target);
//			if(getDistanceY(transform.position,target)>=60){
//				curState = FSM.Jump;
//			}

			//Debug.Log("Distance: "+Vector3.Distance(transform.position,target));
			//Debug.Log("Collider radius: "+GetComponent<CapsuleCollider>().radius);
			//if(Vector3.Distance(transform.position,target)> 0){
				///transform.Translate(new Vector3(velocity,0,0) * Time.deltaTime);
//			if (derecha != (relPos.x > 0)){
//				transform.Rotate (0,180,0);
//				derecha = !derecha;			
//			}
			if(animation["Mover_Derecha"]!=null){
						//animation["Mover_Derecha"].wrapMode = WrapMode.Loop;
						animation.Play("Mover_Derecha");
						
			}
			transform.Translate(new Vector3(velocity,0,0) * Time.deltaTime);
			
//			if (Mathf.Abs(relPos.x) <= stopDistance){
//				animator.SetBool("attack",true);
//				curState = FSM.Attack;
//			}
	

			if(actualTarget != nextTarget){
				if(transform.position.x < nextTarget.x){  
					if(derecha){	
						canvia = true;
						derecha = false;
					}
		  			transform.rotation = Quaternion.Euler(0, 0, 0);					
				}
				if(transform.position.x > nextTarget.x){  
					if(!derecha){	
						canvia = true;
						derecha = true;
					}
		  			transform.rotation = Quaternion.Euler(0, 180, 0);					
				}
				if(transform.position.y > nextTarget.y){
					Invoke("UpdateJumpState",2);
				}
				
				canvia = false;
		
		}
					
			
	   
		//Debug.Log("Target Pos z: " + (Mathf.Abs(target.z)));
		//Debug.Log("PlayerTransform Pos z:" + (Mathf.Abs(transform.localPosition.z)));
		//Debug.Log("Distance: " + (Mathf.Abs(target.z) - Mathf.Abs(transform.localPosition.z)));
		//if(Mathf.Abs(target.z) - Mathf.Abs(transform.localPosition.z) >= 1){
		//	curState = FSM.Attack;
		//}
		
		updatePlayerPosition();
	}
	protected void UpdateAttackState(){
		//Destroy(mas);
		if(animation["Ataque_Derecha"]!=null){
			animation.Play("Ataque_Derecha");
			if(Mathf.Abs(relPos.x) <=stopDistance){
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
		
		updatePlayerPosition();
		
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
	protected override void FSMFixedUpdate(){
		switch (curState){
			case FSM.None: UpdateFixedNoneState(); break;
			case FSM.Jump: UpdateFixedJumpState(); break;
			case FSM.Run: UpdateFixedRunState(); break;
			case FSM.Attack: UpdateFixedAttackState(); break;
			case FSM.Dead: UpdateFixedDeadState(); break;

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
	
	//Initialization of NPC
	protected override void Ini(){
		setInitialsAtributes();
		setInitialCollider();
		setInitialState();
		
		setInitialSpawnPoints();

		updatePlayerPosition();
		
		Debug.Log(curState);
	}

}

