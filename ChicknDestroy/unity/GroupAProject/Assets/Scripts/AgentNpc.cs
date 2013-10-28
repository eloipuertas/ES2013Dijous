using UnityEngine;
using System.Collections;

public class AgentNpc : MonoBehaviour {
	
	private Rigidbody peso= new Rigidbody();
	//Npc propierties
	private float velocity = 100f;
	private bool direccio;
	private float stopRange = 100f;
	//Agent variables
	
	void run(){
		// USING Z AXIS!
		GameObject pla = GameObject.FindGameObjectWithTag("Player");
		Vector3 playerposition = pla.transform.localPosition;
		
		if (direccio != (playerposition.z > this.transform.localPosition.z)){
			transform.Rotate (new Vector3(0,180,0));
			direccio = !direccio;
		}
		
		float dist = playerposition.z-this.transform.localPosition.z;
		if (dist < 0) dist = -dist;
		if (dist > stopRange)
			transform.Translate(new Vector3(0,0,velocity) * Time.deltaTime);	

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

	 			direccio = true;
	
	}
	
	// Update is called once per frame
	void Update () {
		run();
		
	
	}
}
