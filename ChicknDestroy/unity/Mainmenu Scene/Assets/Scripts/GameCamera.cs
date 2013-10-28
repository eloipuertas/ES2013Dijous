using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {
	
	private Transform target;
	private float trackSpeed = 300;
	
	
	// Set target
	public void SetTarget(Transform t) {
		target = t;
	}
	
	// Track target
	void LateUpdate() {
		if (target) {
			//Funciona perfectamente para una distancia Z de la camara al personaje de 500 unidades
			
			float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
			float y = IncrementTowards(transform.position.y, (target.position.y + 50), trackSpeed);
			transform.position = new Vector3(x,y, transform.position.z);
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
