using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	// Player Handling
	public float gravity = 40;
	public float speed = 15;
	public float acceleration = 30;
	public float jumpHeight = 30;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	
	private int mov_derecha = 1;
	private int mov_izquierda = 2;

	private int mov_actual;
	
	private PlayerPhysics playerPhysics;

	
	
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animation.Play("paradaDerecha");
		
	}
	
	void Update () {
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed,acceleration);
		
		if (playerPhysics.grounded) {
			amountToMove.y = 0;
			
			// Jump
			if (Input.GetButtonDown("Jump")) {
				amountToMove.y = jumpHeight;	
			}
		}
		
		
		amountToMove.x = currentSpeed;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.Move(amountToMove * Time.deltaTime);
		
		
		 Debug.Log("Caida: "+amountToMove.y);
		
		if(amountToMove.y > 1) {
			if(mov_actual == mov_derecha) animation.Play("saltoVerticalDer");
			else if(mov_actual == mov_izquierda) animation.Play("saltoVerticalIz");
		}else if(amountToMove.y < -1){
			if(mov_actual == mov_derecha) animation.Play("caidaDerecha");
			else if(mov_actual == mov_izquierda) animation.Play("caidaIzquierda");
		}else {
			if(amountToMove.x > 1) {
				if(mov_actual == mov_derecha) animation.Play("correrDerecha");
				else animation.Play("giroDerechaIzq", PlayMode.StopAll);
				mov_actual = mov_derecha;
			}
			else if (amountToMove.x <= 1 && amountToMove.x >= -1) {
				if(mov_actual == mov_derecha ) animation.Play("paradaDerecha");
				else animation.Play("paradaIzquierda");
			}else{
				
				if(mov_actual == mov_izquierda) animation.Play ("correrIzquierda");
				else animation.Play("giroIzquierdaDerecha", PlayMode.StopAll);
				mov_actual = mov_izquierda;
			}
		}
		
	}
	
	// Increase n towards target by speed
	private float IncrementTowards(float n, float target, float a) {
		if (n == target) {
			return n;	
		}
		else {
			float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n))? n: target; // if n has now passed target then return target, otherwise return n
		}
	}
}
