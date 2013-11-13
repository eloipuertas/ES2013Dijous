
var bandera : GameObject;
var audioPowerUp : AudioSource;

private var halo;
private var tempsCanvi = 1;
private var ultimCanvi = 0;
private var deshabilitat = 0;

function Update () {
	if (deshabilitat == 0) {
		if ((Time.time - ultimCanvi) > tempsCanvi) {
				
			if (bandera.GetComponent("Halo").enabled)
				bandera.GetComponent("Halo").enabled = false;
			else 
				bandera.GetComponent("Halo").enabled = true;
				
			ultimCanvi = Time.time;
		}
	}

}

function OnCollisionEnter(collision:Collision){
	
	if(collision.gameObject.tag =="Player"){
		//sistema provisional de deshabilitar el update, ja que no se si es vol que desaparegui l'halo o tota la bandera
		if (deshabilitat == 0) {
			audioPowerUp.Play();
		    bandera.GetComponent("Halo").enabled = false;
		    collider.active=false;
			deshabilitat = 1;
		}
	}
}