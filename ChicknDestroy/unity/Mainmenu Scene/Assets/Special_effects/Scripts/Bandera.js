var audioPowerUp : AudioSource;

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="Player"){
		audioPowerUp.Play();
    	Destroy(gameObject, 0.2);
	}
}