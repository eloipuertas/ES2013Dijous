
//els segons que triga la bala en desapareixer
var tempsVida = 2;

function Start () {
	Destroy(this.gameObject, tempsVida);
}
