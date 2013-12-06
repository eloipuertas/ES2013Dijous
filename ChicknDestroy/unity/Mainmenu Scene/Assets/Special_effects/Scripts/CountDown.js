var text1:GameObject;
var text2:GameObject;
var foc1:GameObject;
var boto:GameObject;
var audioCountdown:AudioSource;
var obstacle1:GameObject;
var obstacle2:GameObject;

private var timer:float;
private var startTimer;
private var countdown_activada;


function Start () {
	obstacle1.active = false;
	obstacle2.active = false;
	text1.active = false;
	text2.active = false;
	countdown_activada = 0;
	startTimer = 0;
	timer = 15;
}

/* MILLORAR CODI !! */
function Update () {
	if (startTimer == 1) {
		timer -= Time.deltaTime;
		if (timer > 0){
			var numero = timer.ToString("F0");
			switch(numero) {
			
				case "0": //afegir so error. potser colocar final fora if timer > 0 ??
						startTimer = 0;
						countdown_activada = 0;
						text1.active = false;
						text2.active = false;
						foc1.active = true;
						obstacle1.active = false;
						obstacle2.active = false;
						break;
						
				case "9":  //canviar colors per cada numero, funcio o assignant directament el color?
				case "8":
				case "7":
				case "6":
				case "5":
				case "4":
						text1.GetComponent(TextMesh).text = "0" + numero;
						text2.GetComponent(TextMesh).text = "0" + numero;
						break;
				case "3":
				case "2":
				case "1": 
						text1.GetComponent(TextMesh).text = "0" + numero;
						text2.GetComponent(TextMesh).text = "0" + numero;
						text1.renderer.material.color = Color(255,0,0);
						text2.renderer.material.color = Color(255,0,0);
						break;
				
				case "14": boto.animation.Stop();
						   text1.GetComponent(TextMesh).text = numero;
						   text2.GetComponent(TextMesh).text = numero;
						   break;
						   
				case "13": obstacle1.active = true;
						   obstacle2.active = true;
				case "15":
				case "12":
				case "11":
				case "10":
						
						text1.GetComponent(TextMesh).text = numero;
						text2.GetComponent(TextMesh).text = numero;
						break;
				
				default:break;
		    	
			}
		}
	}
}


function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="Player" || collision.gameObject.tag =="NPC"){
	
		if (countdown_activada == 0) {
		
			boto.animation.Play();
			audioCountdown.Play();
			
			startTimer = 1;
			countdown_activada = 1;
			timer = 15;
						
			text1.active = true;
			text2.active = true;
			obstacle1.active = false;
			obstacle2.active = false;
			foc1.active = false;
			
			text1.renderer.material.color = Color.blue;
			text2.renderer.material.color = Color.blue;
			
		}
	}
}