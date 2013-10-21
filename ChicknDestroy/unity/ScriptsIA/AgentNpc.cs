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
	protected Rigidbody mas;
	
	//Next target
	protected Vector3 target;
	protected Vector3 direction;
	
	//Npc atributes
	protected int health;
	protected int damage;
	
	//Npc propierties
	private float velocity = 20f;
	private bool Dead = false;
	
	private Vector3 playereulerAngles;
	//private float rangeWarp = 100;
	private FSM curState;
	
	
	
	// -------Agent arquitecture--------//
	
	
	protected void UpdateNoneState(){
	}
	protected void UpdateRunState(){
			mas = new Rigidbody();
	
			
			if(target.z > playerTransform.localPosition.z){
			
		  	transform.Translate(new Vector3(0,0,-velocity) * Time.deltaTime);
					
			}else{
			transform.Translate(new Vector3(0,0,velocity) * Time.deltaTime);	
				
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
	void OnCollisionEnter(Collision collision){
		Debug.Log("SHOCK");
		Destroy(mas);
		if(collision.gameObject.tag=="Player"){ 
			Debug.Log("Shock2");
 		}
	}
	
	void updatePlayerPosition(){
		GameObject pla = GameObject.FindGameObjectWithTag("Player");
		playerTransform = pla.transform;
		target = pla.transform.localPosition;
		direction = pla.transform.localEulerAngles;
	}
	void manDistance(Vector3 p1,Vector3 p2){
		return sqrt
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
		updatePlayerPosition();
		Debug.Log(curState);
	}

}
