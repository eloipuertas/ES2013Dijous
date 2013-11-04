
var bloodSplat : GameObject;

function OnCollisionEnter(collision:Collision){
	
	//colisio del NPC amb la bala (s'hauria de gestionar la perdua de vida aqui dins)
	if(collision.gameObject.tag =="bala"){ //hauria de ser el player pero no te rigidbody i crec que per aquest motiu no detecta el power-up tot i tenir box collider
	    //obtenim el contacte de la bala amb el personatge
	    var contact = collision.contacts[0];
	    //indiquem que el prefab de sang es produeixi en el punt de contacte de la colisio
	    var expl = Instantiate(bloodSplat, contact.point, Quaternion.identity);
	    Destroy(expl, 0.2);
	}
}