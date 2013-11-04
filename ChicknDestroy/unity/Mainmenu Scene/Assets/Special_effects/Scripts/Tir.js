
public var sortidaBala:GameObject;
public var escopeta:GameObject;
public var bala:GameObject;
 
var audioTir: AudioSource;
var audioRecarga: AudioSource;
var velocitatBala = 1000;
 
function Start () {
	//obtenim tots els audioSources
	var aSources = GetComponents(AudioSource);
	//assignem cada so
	audioTir = aSources[0];
    audioRecarga = aSources[1];
    //assignem els objectes involucrats
	bala = GameObject.Find("bala");
}

function Update () {
	//Per disparar apretar la tecla T
	if (Input.GetKeyDown(KeyCode.T)) {
		var nouTir:GameObject=Instantiate (bala, sortidaBala.transform.position, escopeta.transform.rotation);
		//nouTir.transform.eulerAngles.z=90;
		//per fer que la bala desapareixi al cap de X segons
		nouTir.AddComponent("DestruirBala");
		//segons l'eix on es col·loca la velocitat de la bala en Vector3, aquesta es desplaça cap a una direccio o una altre
		if (escopeta.transform.rotation.y == 1)
			nouTir.rigidbody.AddForce(new Vector3(velocitatBala, 0, 0), ForceMode.VelocityChange);
		else
			nouTir.rigidbody.AddForce(new Vector3(-velocitatBala, 0, 0), ForceMode.VelocityChange);
		audioTir.Play();
	}
	
	if (Input.GetKeyDown(KeyCode.R)) {;
		audioRecarga.Play();
	}

}