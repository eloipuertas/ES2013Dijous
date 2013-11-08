
var ascensor : GameObject;
private var velocitat = 50;
private var pujant = 0;

function Update () {
	if (ascensor.transform.position.y >= 460)
		pujant = 0;
	else if (ascensor.transform.position.y <= 218)
		pujant = 1;

	if (pujant == 1)
		ascensor.transform.Translate(Vector3.up*Time.deltaTime*velocitat);
	else 
		ascensor.transform.Translate(Vector3.down*Time.deltaTime*velocitat);
}