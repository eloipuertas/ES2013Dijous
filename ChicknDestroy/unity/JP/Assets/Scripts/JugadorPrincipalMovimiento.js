#pragma strict

var radio = 5;
var velocidad = 10;
var saltoFuerza = 100;

var localScaleY;

function Start () {
	
	var hit : RaycastHit;

	if(Physics.Linecast(transform.position, transform.position - Vector3.up * 500, hit)){
        transform.position = hit.point + Vector3.up * radio;
    }

    if(!rigidbody)
        gameObject.AddComponent(Rigidbody);
	
	

    rigidbody.mass = 1;
    
    rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
   
    
    Time.timeScale =1;
}

function Update () {
	
    var estaEnTierra = rigidbody.velocity.y >= 0 && rigidbody.velocity.y <= 1;
    
    
    var direction = Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    
    if(estaEnTierra) {
    	
    	if (direction.y > 0) {
    		rigidbody.AddForce(Vector3.up * rigidbody.mass * saltoFuerza*5);
    		
    	}
    	
    }
    
    if(direction.magnitude > 0){
   		var modifier = estaEnTierra ? 6.0 : 1.0;
   		
   		direction = direction * velocidad;
   		
   		if(!estaEnTierra) {
        	direction.y = rigidbody.velocity.y;
        }
        direction.x = 0;
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction, modifier * Time.deltaTime);
        
        if(estaEnTierra) {
        	rigidbody.velocity.y = 0;
        }
    }
}