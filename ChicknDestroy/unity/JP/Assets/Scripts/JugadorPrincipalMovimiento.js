#pragma strict

var radio = 5;
var velocidad = 1.0;
var gravedad = 10;
var saltoFuerza = 100;

var saltoTiempo = 0.0;
var saltoDuracion = 1;

var estaSaltando = false;
var localScaleY;

function Start () {
	transform.localScale = Vector3.one * radio;
	var hit : RaycastHit;
	
	//Si se ha topado con algo verticalmente, lo pone encima
	if(Physics.Linecast(transform.position, transform.position - Vector3.up * 500, hit)){
        transform.position = hit.point + Vector3.up * radio;
    }

	//En el caso de no tener cuerpo rigido, colisionable
    if(!rigidbody)
        gameObject.AddComponent(Rigidbody);


    rigidbody.mass = 10 * radio;
   
    localScaleY = transform.localScale.y; 
    Time.timeScale =1;
}

function Update () {
	var hit : RaycastHit;
	
    var estaEnTierra = Physics.Raycast(transform.position, -Vector3.up, hit, 5);
    
    var direction = Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    if(estaEnTierra) {
    	
    	if (direction.y > 0) {
    		rigidbody.AddForce(Vector3.up * rigidbody.mass * saltoFuerza * 60);
    	}
    	
    }
    
    if(direction.magnitude > 0){
   		var modifier = estaEnTierra ? 1.0 : 0.2;
   		
   		direction = direction * velocidad;
   		
   		if(!estaEnTierra) {
        	direction.y = rigidbody.velocity.y;
        }
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction, modifier * Time.deltaTime);
        
        if(estaEnTierra) {
        	rigidbody.velocity.y = 0;
        	rigidbody.velocity.x = 0;
        }
        else {
        	rigidbody.velocity.y = gravedad;
        	rigidbody.velocity.x = 0;
        }
    }
}