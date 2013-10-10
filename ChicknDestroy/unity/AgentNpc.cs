using UnityEngine;
using System.Collections;

public class AgentNpc : MonoBehaviour {
	
	private Rigidbody peso= new Rigidbody();
	//Npc propierties
	private float velocity = 20f;
	private bool detect = false;
	private Vector3 playerposition;
	private Vector3 playereulerAngles;
	private float rangeWarp = 100;
	//Agent variables
	
	void run(){
		if(playerposition.z > this.transform.localPosition.z){
	  	transform.Translate(new Vector3(0,0,velocity) * Time.deltaTime);
				
		}else{
		transform.Translate(new Vector3(0,0,-velocity) * Time.deltaTime);	
		}
	}
	GameObject warpNpc(Vector3 p,Vector3 s){
		GameObject npc = GameObject.CreatePrimitive(PrimitiveType.Cube);
		npc.transform.localPosition = p;
		npc.transform.localScale = s;
		return npc;
	}
	// Use this for initialization
	void Awake(){
	
		
		 
		
	}
	void Start () {
		GameObject pla = GameObject.FindGameObjectWithTag("player");
		playerposition = pla.transform.localPosition;
	 	
	
	}
	
	// Update is called once per frame
	void Update () {
		run();
		
	
	}
}
