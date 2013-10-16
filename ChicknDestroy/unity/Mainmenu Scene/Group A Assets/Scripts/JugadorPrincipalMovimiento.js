#pragma strict

function Start () {

}

function Update () {
	transform.Translate(0,0,Input.GetAxis("Horizontal")*60*Time.deltaTime);
}