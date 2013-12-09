
var bandera : GameObject;

private var halo;
private var tempsCanvi = 1;
private var ultimCanvi = 0;
private var deshabilitat = 0;

function Update () {
	if (deshabilitat == 0) {
		if ((Time.time - ultimCanvi) > tempsCanvi) {
				
			if (bandera.GetComponent("Halo").enabled)
				bandera.GetComponent("Halo").enabled = false;
			else 
				bandera.GetComponent("Halo").enabled = true;
				
			ultimCanvi = Time.time;
		}
	}

}