var cosPlayer:GameObject;
var escopetaPlayer:GameObject;

private var fetDany;
private var tempsActual;
private var lastTime;
private var minTime;
private var tempsEntrat;
private var tempsParpadejant;

function Start () {
	fetDany = 0;
	minTime = 0.15;
	tempsParpadejant = 2;
}

function Update () {
	if (fetDany == 1) {
		if ((Time.time - lastTime) > minTime) {
			if (cosPlayer.renderer.enabled) {
				cosPlayer.renderer.enabled = false;
				escopetaPlayer.renderer.enabled = false;
			}
			else {
				cosPlayer.renderer.enabled = true;
				escopetaPlayer.renderer.enabled = true;
			}
			lastTime = Time.time;
		}
		
		if (Time.time - tempsEntrat > tempsParpadejant){
			fetDany = 0;
			cosPlayer.renderer.enabled = true;
			escopetaPlayer.renderer.enabled = true;
		}
	}
}

function OnCollisionEnter(collision:Collision){
	if(collision.gameObject.tag =="foc" || collision.gameObject.tag =="punxes"){
		if (fetDany == 0) {
			fetDany = 1;
			cosPlayer.renderer.enabled = false;
			escopetaPlayer.renderer.enabled = false;
			lastTime = Time.time;
			tempsEntrat = Time.time;
		}
	}
}