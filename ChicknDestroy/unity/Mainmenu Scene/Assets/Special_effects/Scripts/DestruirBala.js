﻿
//els segons que triga la bala en desapareixer
var tempsVida = 7;

function Start () {
	Destroy(this.gameObject, tempsVida);
}

function OnCollisionEnter(collision:Collision){
	/*
	if(collision.gameObject.tag =="NPC"){ //hauria de ser el player pero no te rigidbody i crec que per aquest motiu no detecta el power-up tot i tenir box collider
		audioPowerUp.Play();
	    var expl = Instantiate(explosion, transform.position, Quaternion.identity);
	    Destroy(expl, 3); // delete the explosion after 3 seconds
	    Destroy(gameObject,0.2);
	}
	*/
	Destroy(gameObject);
}