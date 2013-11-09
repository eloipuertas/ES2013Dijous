﻿var explosion: GameObject;
var audioPowerUp : AudioSource;
var powerUp : GameObject;

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="NPC"){ //hauria de ser el player pero no te rigidbody i crec que per aquest motiu no detecta el power-up tot i tenir box collider
		audioPowerUp.Play();
    	var expl = Instantiate(explosion, transform.position, Quaternion.identity);
    	Destroy(expl, 3); // delete the explosion after 3 seconds
    	//Destroy(gameObject,0.12);
    	powerUp.SetActive(false);
	}
}