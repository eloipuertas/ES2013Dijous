
var audioPowerUp: AudioSource;
//son els cartutxos de l'escopeta que estan al terra
public var escopeta_off:GameObject;

function Start () {
	var aSources = GetComponents(AudioSource);
	//el tercer so correspon al power-up. Per consultar l'ordre mirar els audio sources del personatge, l'ordre en que
	//estan assignats es l'ordre per a obtenir-los
	audioPowerUp = aSources[2];
	escopeta_off= GameObject.Find("escopeta_off");
}

function OnCollisionEnter(Colision : Collision) {
	if (Colision.gameObject.name == "escopeta_off") {
		audioPowerUp.Play();
		Destroy(escopeta_off);
	}
}