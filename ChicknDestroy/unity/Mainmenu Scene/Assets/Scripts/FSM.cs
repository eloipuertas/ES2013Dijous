using UnityEngine;
using System.Collections;

public class FSM : MonoBehaviour {
	/*
	//Player transform
	protected Transform playerTransform;
	
	//Next target
	protected Vector3 target;
	
	//Npc atributes
	protected int health;
	protected int damage;
	*/
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
