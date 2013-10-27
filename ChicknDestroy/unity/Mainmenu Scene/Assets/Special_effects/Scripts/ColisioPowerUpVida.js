var explosion: GameObject;
public var powerUp7:GameObject;
var audioPowerUp:AudioSource;

function Start () {
	powerUp7 = GameObject.Find("upVida_animacio_rotacio_7");
	var aSources = GetComponents(AudioSource);
	audioPowerUp = aSources[0];
}
 
 
function OnCollisionEnter(){
     audioPowerUp.Play();
     var expl = Instantiate(explosion, transform.position, Quaternion.identity);
     Destroy(expl, 3); // delete the explosion after 3 seconds
}