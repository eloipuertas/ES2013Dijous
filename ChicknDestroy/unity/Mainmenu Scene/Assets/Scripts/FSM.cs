using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {

	//Next target
	protected Vector3 target;
	
	
	//Abstract methods
	
	protected virtual void Ini(){}
	protected virtual void FSMUpdate(){}
	protected virtual void FSMFixedUpdate(){}
	
	// Use this for initialization
	void Start () {
		Ini();
	}
	// Update is called once per frame
	void Update () {
		FSMUpdate();
	}
	void FixedUpdate(){
		FSMFixedUpdate();
	}
}
