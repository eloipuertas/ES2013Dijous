
var bloodSplat : GameObject;
var audioImpacte : AudioSource;

function OnCollisionEnter(collision:Collision){
	
	if(collision.gameObject.tag =="bala"){
	    audioImpacte.Play();
	    //obtenim el contacte de la bala amb el personatge
	    var contact = collision.contacts[0];
	    //indiquem que el prefab de sang es produeixi en el punt de contacte de la colisio
	    var expl = Instantiate(bloodSplat, contact.point, Quaternion.identity);
	    Destroy(expl, 0.2);
	}
}