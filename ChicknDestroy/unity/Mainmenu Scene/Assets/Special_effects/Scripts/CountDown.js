var text1:GameObject;
var text2:GameObject;
var text3:GameObject;
var foc1:GameObject;
var foc2:GameObject;
var audioCountdown:AudioSource;

private var timer:float;
private var startTimer;
private var countdown_activada;


function Start () {
	text1.active = false;
	text2.active = false;
	text3.active = false;
	countdown_activada = 0;
	startTimer = 0;
	timer = 10;
}

function Update () {
	if (startTimer == 1) {
		timer -= Time.deltaTime;
		if (timer > 0){
			var numero = timer.ToString("F0");
			switch(numero) {
			
				case "0": //afegir so error. potser colocar final fora if timer > 0 ??
						startTimer = 0;
						text1.active = false;
						text2.active = false;
						text3.active = false;
						foc1.active = true;
						foc2.active = true;
						countdown_activada = 0;
						break;
						
				case "9":  //canviar colors per cada numero, funcio o assignant directament el color?
				case "8":
				case "7":
				case "6":
				case "5":
				case "4":
						text1.GetComponent(TextMesh).text = "0" + numero;
						text2.GetComponent(TextMesh).text = "0" + numero;
						text3.GetComponent(TextMesh).text = "0" + numero;
						break;
				case "3":
				case "2":
				case "1": 
						text1.GetComponent(TextMesh).text = "0" + numero;
						text2.GetComponent(TextMesh).text = "0" + numero;
						text3.GetComponent(TextMesh).text = "0" + numero;
						text1.renderer.material.color = Color(255,0,0);
						text2.renderer.material.color = Color(255,0,0);
						text3.renderer.material.color = Color(255,0,0);
						break;
				
				case "10":
						text1.GetComponent(TextMesh).text = numero;
						text2.GetComponent(TextMesh).text = numero;
						text3.GetComponent(TextMesh).text = numero;
						break;
				
				default:break;
		    	
			}
		}
	}
}


function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="Player"){
		if (countdown_activada == 0) {
			startTimer = 1;
			audioCountdown.Play();
			text1.active = true;
			text2.active = true;
			text3.active = true;
			foc1.active = false;
			foc2.active = false;
			countdown_activada = 1;
			timer = 10;
		}
	}
}