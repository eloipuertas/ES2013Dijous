#pragma strict

var camaraVelocidad = 5;
var radio = 10;
var velocidad = 60;
var gravedad = 10;
var saltoFuerza = 10;
var saltoTiempo = 0.0;
var saltoDuracion = 1;

var estaSaltando = false;
var localScaleY;

private var velocity : Vector3 = Vector3.zero;


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


    rigidbody.mass = gravedad * radio;
   
    localScaleY = transform.localScale.y; 
    Time.timeScale =1;
}

function Update () {
	var hit : RaycastHit;
	
    var estaEnTierra = Physics.Raycast(transform.position, -Vector3.up, hit, radio);
    
    var direction = Vector3(0, Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"));
    if(direction.magnitude > 1.0) direction.Normalize();
    
    if(estaEnTierra && direction.y > 0) {
    	rigidbody.AddForce(Vector3.up * rigidbody.mass * 100);
    }
    
    if(direction.magnitude > 0){
   		var modifier = estaEnTierra ? 3.0 : 1.0;
   		direction = Camera.main.transform.TransformDirection(direction) * camaraVelocidad * 2;
   		
   		if(!estaEnTierra)
        	direction.y = rigidbody.velocity.y;
        
        //direction = Vector3.one;
        direction.x = 0;
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction, modifier * Time.deltaTime);
        
        if(estaEnTierra)
        	rigidbody.velocity.y = 0;
    }
}