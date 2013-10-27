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
	
	//Player transform
	protected Transform playerTransform;
	
	//Animations 
	
	public Animation movRight;
	public Animation movLeft;
	public Animation rotRight;
	public  Animation rotLeft;
	public Animation jumpRight;
	public  AnimationClip jumpLeft;
	public AnimationClip atack;

	//Next target
	protected Vector3 target;
	protected Vector3 direction;
	
	
	
	//Npc propierties
	private Vector3 spawnPoint;
	private Collision collision;
	private Collider col;
	private Rigidbody mas;
	private GameObject pla;
	
	//Npc atributes
	
	public int health = 100;
	public int damage = 25;
	public string primaryWeapon;
	public string secondaryWeapon;
	
	public float velocity = 30f;
	private bool Dead = false;
	
	
	private float dist;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	
		// -------NPC interface----------
	
	protected int getHeatlhPoints(){
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
		playerTransform = pla.transform;
		target = pla.transform.localPosition;
		direction = pla.transform.localEulerAngles;
		Vector3 volum = transform.localPosition;
		//Debug.Log(volum);
		dist = Mathf.Abs(Vector3.Distance(target,volum));
		
			
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
	}
	void setInitialState(){
		curState = FSM.Run;
	}
	void setInitialCollider(){
		col = GetComponent<Collider>();
		Debug.Log(col);
		mas = this.gameObject.rigidbody;
		Debug.Log(mas.mass);
		
		//mas.GetComponent(RigidBody);
	}
	
	// ######### Agent arquitecture #########//
	
	//--------Agents States------------//
	protected void UpdateNoneState(){
		//Animation idle
	}
	protected void UpdateRunState(){
			
		
			
			//setInitialCollider();
			
			//Vector3 relPos = target - transform.position;
		    //transform.rotation = Quaternion.LookRotation(relPos);
			//transform.LookAt
//			if(getDistanceY(transform.position,target)>=60){
//				curState = FSM.Jump;
//			}
			this.animation["Mover_Izquierda"].wrapMode = WrapMode.Loop;
			this.animation.Play("Mover_Izquierda");
		 	
			Debug.Log("Distance: "+Vector3.Distance(transform.position,target));
			Debug.Log("Collider radius: "+GetComponent<CapsuleCollider>().radius);
			if(Vector3.Distance(transform.position,target)> GetComponent<CapsuleCollider>().radius){
				transform.Translate(new Vector3(-velocity,0,0) * Time.deltaTime);
				if(transform.position.x > target.x){
					
		  			transform.rotation = Quaternion.Euler(0, 0, 0);
//					this.animation["Mover_Derecha"].wrapMode = WrapMode.Loop;
//					this.animation.Play("Mover_Derecha");
					
				}
				if(transform.position.x < target.x){
					
		  			transform.rotation = Quaternion.Euler(0, 180, 0);
					
				}
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
		Destroy(mas);
		Debug.Log("KILL THEM");
		
//		pla.setHealtPoints((pla.getHealthPoints - damage));
		if(
		WaitForSeconds(0.5f);
		pla.setHeatlhPoints(pla.getHealhpoints()-damage);
		Debug.Log("Damage: 25");
		if(Vector3.Distance(transform.position,target)> GetComponent<CapsuleCollider>().radius){
			curState = FSM.Run;
		}
		
		updatePlayerPosition();
		
	}
    protected void UpdateJumpState(){
		}
	protected void UpdateDeadState(){
		//Animacio morir
		Destroy(this.gameObject);
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
		Debug.Log(this.getHeatlhPoints());
		if (this.getHeatlhPoints() <= 0) curState = FSM.Dead;
		//if (pla.getHeatlhPoints() <=0) curState = FSM.None;
		Debug.Log("Current STATE: "+curState);

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
			setHealthPoints(getHeatlhPoints()-25);
		}
		
		
	}
	
	//Initialization of NPC
	protected override void Ini(){
		setInitialsAtributes();
		setInitialCollider();
		setInitialState();
		updatePlayerPosition();
		Debug.Log(curState);
	}

}

