
//els segons que triga la bala en desapareixer
var tempsVida = 0.6;

function Start () {
	Destroy(this.gameObject, tempsVida);
}

function OnCollisionEnter(collision:Collision){
	Destroy(gameObject);
}