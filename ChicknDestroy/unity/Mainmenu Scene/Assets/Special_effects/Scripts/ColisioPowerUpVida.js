var explosion: GameObject;

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="Player"){
    	var expl = Instantiate(explosion, transform.position, Quaternion.identity);
    	Destroy(expl, 2); // delete the explosion after 3 seconds
    	Destroy(gameObject);
	}
}