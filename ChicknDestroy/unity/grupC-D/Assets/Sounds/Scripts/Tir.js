﻿
public var sortidaBala:GameObject;
public var escopeta:GameObject;
public var bala:GameObject;
 
var audioTir: AudioSource;
var audioRecarga: AudioSource;
var velocitatBala = 5;
 
function Start () {
	//obtenim tots els audioSources
	var aSources = GetComponents(AudioSource);
	//assignem cada so
	audioTir = aSources[0];
    audioRecarga = aSources[1];
    //assignem els objectes involucrats
	escopeta = GameObject.Find("escopeta");
	sortidaBala = GameObject.Find("sortidaBala");
	bala = GameObject.Find("bala");
}

function Update () {
	//Per disparar apretar la tecla T
	if (Input.GetKeyDown(KeyCode.T)) {
		var nouTir:GameObject=Instantiate (bala, sortidaBala.transform.position, escopeta.transform.rotation);
		//per fer que la bala desapareixi al cap de X segons
		nouTir.AddComponent("DestruirBala");
		//segons l'eix on es col·loca la velocitat de la bala en Vector3, aquesta es desplaça cap a una direccio o una altre
		nouTir.rigidbody.AddRelativeForce(new Vector3(0, 0, velocitatBala), ForceMode.VelocityChange);
		audioTir.Play();
	}
	
	if (Input.GetKeyDown(KeyCode.R)) {;
		audioRecarga.Play();
	}

}