var entrar = 0;
var audioAcceptar: AudioSource;
var audioOpcions: AudioSource;


function Start(){
    var aSources = GetComponents(AudioSource);
    audioAcceptar = aSources[0];
    audioOpcions = aSources[1];
}

function OnMouseEnter(){
	renderer.material.color = Color.blue;
	audioOpcions.Play();
	entrar = 1;		
}
	
function OnMouseExit(){
	renderer.material.color = Color.white;
	entrar = 0;
}

function OnMouseDown() {
	if(entrar)
		audioAcceptar.Play();
}
