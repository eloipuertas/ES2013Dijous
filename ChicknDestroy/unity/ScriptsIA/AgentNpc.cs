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
	
	public AnimationClip movRight;
	public AnimationClip movLeft;
	public AnimationClip rotRight;
	public  AnimationClip rotLeft;
	public AnimationClip jumpRight;
	public  AnimationClip jumpLeft;
	public AnimationClip atack;

	//Next target
	protected Vector3 target;
	protected Vector3 direction;
	
	//Npc atributes
	protected int health;
	protected int damage;
	
	//Npc propierties
	private Collision collision;
	private Rigidbody mas;
	
	public float velocity = 30f;
	private bool Dead = false;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	
	
	
	// -------Agent arquitecture--------//
	
	
	protected void UpdateNoneState(){
	}
	protected void UpdateRunState(){
			
			mas = this.gameObject.rigidbody;
			transform.LookAt(target);
		  	transform.Translate(new Vector3(0,0,velocity) * Time.deltaTime);
			
					
			
	   
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
		
	}
	protected void UpdateDeadState(){
	}
	//Agent Perceptions
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
		if (health <= 0) curState = FSM.Dead;
		Debug.Log("Current STATE: "+curState);

	}
	//Gestio de collisions
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.collider){
			Debug.Log("A tocat terra");
		}
		if(collision.gameObject.tag=="Player"){ 
				Debug.Log("A tocat player");
				curState = FSM.Attack;
 		}
		
	}
	//Actualiza la posicio del objectiu
	void updatePlayerPosition(){
		GameObject pla = GameObject.FindGameObjectWithTag("Player");
		playerTransform = pla.transform;
		target = pla.transform.localPosition;
		direction = pla.transform.localEulerAngles;
		
			
	}
	void manDistance(Vector3 p1,Vector3 p2){
		//return Mathf.Sqrt(Mathf.Exp(2.0)*(p1.x -p2.x) +(p1.y - p2.y));
	}
	//General Funcionts
	
	GameObject warpNpc(Vector3 p,Vector3 s){
		GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Cube);
		npc.transform.localPosition = p;
		npc.transform.localScale = s;
		return npc;
	}
	protected override void Ini(){
		curState = FSM.Run;
		health = 100;
		damage = 2;
		mas = this.gameObject.rigidbody;
		Debug.Log(mas.mass);
		
		//mas.GetComponent(RigidBody);
		
		updatePlayerPosition();
		Debug.Log(curState);
	}

}

