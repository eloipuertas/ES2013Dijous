
private var tempsVida = 0.8;
var explosion:GameObject;

function Start () {
	Destroy(this.gameObject, tempsVida);
	explosion = GameObject.Find("explosio_granada");
}

function OnCollisionEnter(collision:Collision){
	explosion.particleEmitter.enabled = true;
	var expl = Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(expl, 1);
	Destroy(gameObject);
}
