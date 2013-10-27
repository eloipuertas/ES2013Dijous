var explosion: GameObject;
public var escopeta_off_3:GameObject;
var audioPowerUp:AudioSource;

function Start () {
	escopeta_off_3 = GameObject.Find("escopeta_off_3");
	var aSources = GetComponents(AudioSource);
	audioPowerUp = aSources[0];
}
 
 
function OnCollisionEnter(){
     audioPowerUp.Play();
     var expl = Instantiate(explosion, transform.position, Quaternion.identity);
     Destroy(expl, 3); // delete the explosion after 3 seconds
}
