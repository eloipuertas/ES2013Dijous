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
	protected Vector3 target;
	protected Vector3 direction;
	
	
	
	//Npc propierties
	private Vector3 spawnPoint;
	private Collision collision;
	private Collider col;
	private Rigidbody mas;
	private GameObject pla;
	private PlayerController playerScript;
	
	//Npc atributes
	
	public int health = 100;
	public int damage = 25;
	public string primaryWeapon;
	public string secondaryWeapon;
	
	public float velocity = 80f;
	private bool Dead = false;
	
	
	private float dist;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	
	private Animator animator;
	private bool derecha = true;
	public float stopDistance = 60;
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
	
	//Return 
//	float getcolliderType(){
//		if(col == null) return 0;
//		if(col.GetType() == typeof(MeshCollider)){
//			print("mesh");
//			MeshCollider f = col.GetComponent<MeshCollider>();
//			return f;
//			
//		}
//		if(col.GetType() == typeof(CapsuleCollider)){
//			print("capsule");
//			CapsuleCollider f = col.GetComponent<CapsuleCollider>();
//			return f;
//			
//		}
//		if(col.GetType() == typeof(SphereCollider)){
//			print("sphere");
//			SphereCollider d = col.GetComponent<SphereCollider>();
//			return d;
//		}
//		if(col.GetType() == typeof(BoxCollider)){
//			print("box");
//			BoxCollider f = col.GetComponent<CapsuleCollider>();
//			return f;
//		}
//		if(col.GetType() == typeof(MeshCollider)) print("mesh");
//	}
	//void manDistance(Vector3 p1,Vector3 p2){
		//return Mathf.Sqrt(Mathf.Exp(2.0)*(p1.x -p2.x) +(p1.y - p2.y));
	//}
	//-------------Utility Functions---------------
	
	//Actualiza la posicio del objectiu
	void updatePlayerPosition(){
		pla = GameObject.FindGameObjectWithTag("Player");
		playerScript = (PlayerController) pla.GetComponent(typeof(PlayerController));
		playerTransform = pla.transform;
		target = pla.transform.localPosition;
		
		
		
			
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
		damage = 25;
		primaryWeapon = "katana";
		
		animator = GetComponent<Animator>();
		
	}
	void setInitialSpawnPoints(){
		spawnPoint = transform.position;
	}
	void setInitialState(){
		curState = FSM.Run;
	}
	void setInitialCollider(){
		col = GetComponent<Collider>();
		Debug.Log(col);
		mas = this.gameObject.rigidbody;
		Debug.Log(mas);
		
		//mas.GetComponent(RigidBody);
	}
	IEnumerator WaitAndCallback(float waitTime){
		Debug.Log("Time "+Time.time);
		yield return new WaitForSeconds(waitTime);
		Debug.Log("Time 3 "+Time.time);
		//gobool = true;
	}

	
	// ######### Agent arquitecture #########//
	
	//--------Agents States------------//
	protected void UpdateNoneState(){
		//Animation idle
	}
	protected void UpdateRunState(){
			
			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Mover_Derecha"))
				animator.SetBool("move",false);
		
			setInitialCollider();
			
			Vector3 relPos = target - transform.position;
		    //transform.rotation = Quaternion.LookRotation(relPos);
			//transform.LookAt(target);
//			if(getDistanceY(transform.position,target)>=60){
//				curState = FSM.Jump;
//			}

			
		 	
			//Debug.Log("Distance: "+Vector3.Distance(transform.position,target));
			//Debug.Log("Collider radius: "+GetComponent<CapsuleCollider>().radius);
			//if(Vector3.Distance(transform.position,target)> 0){
				///transform.Translate(new Vector3(velocity,0,0) * Time.deltaTime);
		
		
				if (derecha != (relPos.x > 0)){
					transform.Rotate (0,180,0);
					derecha = !derecha;			
				}
		
				transform.Translate(new Vector3(velocity,0,0) * Time.deltaTime);

				if (Mathf.Abs(relPos.x) <= stopDistance){
					animator.SetBool("attack",true);
					curState = FSM.Attack;
				}


//				if(transform.position.x < target.x){
//					
//		  			transform.rotation = Quaternion.Euler(0, 0, 0);
//				    
////					this.animation["Mover_Derecha"].wrapMode = WrapMode.Loop;
////					this.animation.Play("Mover_Derecha");
//					
//				}
//				if(transform.position.x > target.x){
//					
//		  			transform.rotation = Quaternion.Euler(0, 180, 0);
//					
//				}
			//}
		
			
					
			
	   
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
		
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Ataque_Derecha"))
			animator.SetBool("attack",false);

		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle")){
			
			Vector3 relPos = target - transform.position;
			
			if (Mathf.Abs(relPos.x) > stopDistance+5){
				animator.SetBool("move",true);
				curState = FSM.Run;
				
			} else if (!attacked){
				animator.SetBool("attack",true);
				playerScript.setDamage(damage);
				print("HIT");
				attacked = true;
				
			} else if(attacked){
				attacked = false;
			}
			
			
		}

		
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
	 
//	private void KillThatMotherfucker(){
//		pla.setHeatlhPoints(pla.getHealhpoints()-damage);
//	}
		
    protected void UpdateJumpState(){
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
		//Debug.Log("Current STATE: "+curState);

	}
	//-----------Gestio de collisions----------
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.collider){
			Debug.Log("A tocat terra");
		}
		if(collision.gameObject.tag=="Player"){ 
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

