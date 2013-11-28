var myLight : Light;
var audioLlamp : AudioSource ;

private var minTime = 0.3;
private var treshold = 0.5;
private var lastTime = 0;
private var lT = 0;
private var entrar = 0;
private var tempsEntrat = 120;
private var colorActual : Color;
private var tempsLlampAleatori;


function Start() {
	colorActual = light.color;
	tempsLlampAleatori = Random.Range(30,120);
}

function Update () {
	if ((Time.time - lT) > tempsLlampAleatori) {
		if (entrar == 0) {
			audioLlamp.Play();
			tempsEntrat = Time.time;
			entrar = 1;
		}
		if ((Time.time - lastTime) > minTime)
		var val = Random.value;
		if (val > treshold)
			light.color = Color.white;
		else
			light.color = colorActual;
		lastTime = Time.time;
	}
	
	if ((Time.time - tempsEntrat > 2.8) && entrar == 1){
		tempsLlampAleatori = Random.Range(30,120);
		//Debug.Log(tempsLlampAleatori);
		lT = Time.time;
		light.color = colorActual;
		entrar = 0;
	}
	
}