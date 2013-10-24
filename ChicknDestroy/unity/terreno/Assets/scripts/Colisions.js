var audioPowerUp: AudioSource;
var audioPartidaPerduda: AudioSource;
var audioPartidaGuanyada: AudioSource;

//son els cartutxos de l'escopeta que estan al terra
public var escopeta_off:GameObject;
//fa una funcio de perdre el joc, simplement perque soni una determinada musica
public var Game_Over:GameObject;
//igual que anterior pero per musica de victoria
public var Win:GameObject;

function Start () {
	var aSources = GetComponents(AudioSource);
	//el tercer so correspon al power-up. Per consultar l'ordre mirar els audio sources del personatge, l'ordre en que
	//estan assignats es l'ordre per a obtenir-los
	audioPowerUp = aSources[2];
	audioPartidaPerduda = aSources[3];
	audioPartidaGuanyada = aSources[4];
	
	//trobem objectes
	escopeta_off = GameObject.Find("escopeta_off");
	Game_Over = GameObject.Find("Game_Over");
	Win = GameObject.Find("Win");
}

function OnCollisionEnter(Colision : Collision) {
	if (Colision.gameObject.name == "escopeta_off") {
		audioPowerUp.Play();
		Destroy(escopeta_off);
	}
	
	else if (Colision.gameObject.name == "Game_Over") {
		if (audioPartidaGuanyada.isPlaying)
			audioPartidaGuanyada.Stop();
		audioPartidaPerduda.Play();
	}
	
	else if (Colision.gameObject.name == "Win") {
		if (audioPartidaPerduda.isPlaying)
				audioPartidaPerduda.Stop();
		audioPartidaGuanyada.Play();
	}
	
}