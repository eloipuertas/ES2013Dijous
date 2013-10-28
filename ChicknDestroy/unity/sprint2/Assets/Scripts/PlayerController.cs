using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerPhysics))]
public class PlayerController : MonoBehaviour {
	
	// Player Handling
	public float gravity = 40;
	public float speed = 30;
	public float acceleration = 30;
	public float jumpHeight = 60;
	
	private float currentSpeed;
	private float targetSpeed;
	private Vector2 amountToMove;
	
	
	private int movDer = 1;
	private int movIzq = 2;
	private int stopDer = 3;
	private int stopIzq = 4;

	private int lastDirection;
	
	private PlayerPhysics playerPhysics;

	
	
	void Start () {
		playerPhysics = GetComponent<PlayerPhysics>();
		animation.Play("paradaDerecha");
		lastDirection = stopDer;
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
		
		
		if(amountToMove.y > 1) {
		//Si estamos en el aire de subida
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("saltoVerticalDer");
			else animation.Play("saltoVerticalIz");
		}else if(amountToMove.y < -1){
		//Si estamos en el aire de bajada
			if(lastDirection == movDer || lastDirection == stopDer) animation.Play("caidaDerecha");
			else animation.Play("caidaIzquierda");
		}else {
		//Si estamos en el suelo
			if (lastDirection == movDer) {
				if (amountToMove.x > 0) animation.Play("correrDerecha");
				else if(amountToMove.x < 0) animation.Play("giroDerechaIzq");
				else {
					animation.Play("paradaDerecha");
					lastDirection = stopDer;
				}
			}else if (lastDirection == movIzq) {
				if (amountToMove.x < 0) animation.Play("correrIzquierda");
				else if(amountToMove.x > 0) animation.Play("giroIzquierdaDerecha");
				else {
					animation.Play("paradaIzquierda");
					lastDirection = stopIzq;
				}
			}else {
				if (lastDirection == stopDer) animation.Play("paradaDerecha");
				else animation.Play("paradaIzquierda");
			}
		
			if (amountToMove.x != 0) 
				lastDirection = (amountToMove.x < 0)?movIzq:movDer;
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
