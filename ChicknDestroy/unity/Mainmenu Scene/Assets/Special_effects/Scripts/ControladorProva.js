var Personatge:GameObject;

function Update () {
	if(Input.GetKeyUp(KeyCode.Space)) {
		Personatge.rigidbody.AddForce(new Vector3(0,50,0), ForceMode.VelocityChange);
	}

	if(Input.GetKey("left")){
		Personatge.transform.position.x -= 1;
	}
	
	if(Input.GetKey("right")){
		Personatge.transform.position.x += 1;
	}

}