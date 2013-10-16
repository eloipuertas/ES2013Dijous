using UnityEngine;
using System.Collections;

public class AgentNpc : MonoBehaviour {
	
	private Rigidbody peso= new Rigidbody();
	//Npc propierties
	private float velocity = 2f;
	private bool detect = false;
	private Vector3 playereulerAngles;
	private bool direccio;
	private float rangeWarp = 100;
	//Agent variables
	
	void run(){
		GameObject pla = GameObject.FindGameObjectWithTag("Player");
		Vector3 playerposition = pla.transform.localPosition;
		
		
		if (direccio != (playerposition.x > this.transform.localPosition.x)){
			transform.Rotate (new Vector3(0,180,0));
			direccio = !direccio;
		}

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

	 			direccio = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		run();
		
	
	}
}
