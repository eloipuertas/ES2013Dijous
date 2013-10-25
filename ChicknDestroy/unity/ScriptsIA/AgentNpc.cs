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
	protected Vector2 target;
	protected Vector2 direction;
	
	
	
	//Npc propierties
	
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
	
	
	
	// ######### Agent arquitecture #########//
	
	//--------Agents States------------//
	protected void UpdateNoneState(){
	}
	protected void UpdateRunState(){
			
		
			
			mas = this.gameObject.rigidbody;
			
			
			//transform.LookAt
		  	transform.Translate(new Vector2(-velocity,0) * Time.deltaTime);
			
					
			
	   
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
		if(dist >=0){
			curState = FSM.Run;
		}
		
	}
	protected void UpdateDeadState(){
	}
	//-----------Agent Perceptions------------
	protected override void FSMUpdate(){
		switch (curState){
			case FSM.None: UpdateNoneState(); break;
			case FSM.Jump: UpdateRunState(); break;
			case FSM.Run: UpdateRunState(); break;
			case FSM.Attack: UpdateAttackState(); break;
			case FSM.Dead: UpdateDeadState(); break;

 		}

 		//elapsedTime += Time.deltaTime;

 		//Go to dead state is no health left
		Debug.Log(this.getHeatlhPoints());
		if (this.getHeatlhPoints() <= 0) curState = FSM.Dead;
		Debug.Log("Current STATE: "+curState);

	}
	//-----------Gestio de collisions----------
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.collider){
			Debug.Log("A tocat terra");
		}
		if(collision.gameObject.tag=="player"){ 
				Debug.Log("A tocat player");
				curState = FSM.Attack;
 		}
		
	}
	
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
	protected Vector2 getCoordinates(){
		return new Vector2(transform.localPosition.x,transform.localPosition.y);
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
		Debug.Log(volum);
		dist = Mathf.Abs(Vector3.Distance(target,volum));
		
			
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
	
	
	//Initialization of NPC
	protected override void Ini(){
		setInitialsAtributes();
		setInitialCollider();
		setInitialState();
		updatePlayerPosition();
		Debug.Log(curState);
	}

}

