var Personatge:GameObject;
var audioSalt: AudioSource;
var audioCaminarRobot: AudioSource;

function Start () {
	var aSources = GetComponents(AudioSource);
	//assignem cada so. L'ordenacio correspon a l'ordre en que arrastrat els sons a cada objecte
	audioSalt = aSources[0];
	audioCaminarRobot = aSources[1];
	Personatge=GameObject.Find("Personatge");

}

function Update () {
	//Barra espaciadora fa saltar el personatge
	if(Input.GetKeyUp(KeyCode.Space)) {
		//Força veloctiat constant, moviment sobre eix Y
		Personatge.rigidbody.AddForce(new Vector3(0,30,0), ForceMode.VelocityChange);
		audioCaminarRobot.Stop();
		audioSalt.Play();
	}
	
	//Boto esquerra i dret fan moure el personatge
	if(Input.GetKey("left")){
		Personatge.transform.position.x -= 1;
		//per evitar la superposicio del so de caminar i perque no soni mentre s'esta a l'aire i s'apreta moure's a l'esquerra
		//if (!audioCaminarRobot.isPlaying && Personatge.transform.position.y < 2.5)
		if (!audioCaminarRobot.isPlaying)
			audioCaminarRobot.Play();
	}
	
	if(Input.GetKey("right")){
		Personatge.transform.position.x += 1;
		//if (!audioCaminarRobot.isPlaying && Personatge.transform.position.y < 2.5)
		if (!audioCaminarRobot.isPlaying)
			audioCaminarRobot.Play();
	}

}