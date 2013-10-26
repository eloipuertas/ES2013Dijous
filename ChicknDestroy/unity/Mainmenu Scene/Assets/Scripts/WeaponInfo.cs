using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

	public bool isEscopeta = false;
	private bool isPlayer = false;
	WWW w;
	Texture2D t;
	
	void OnTriggerEnter(Collider whoIs) {
        if(whoIs.CompareTag("Player")){
			isPlayer = true;	
		}
    }
	
	void OnTriggerExit(Collider whoIs){
		 if(whoIs.CompareTag("Player")){
			isPlayer = false;	
		}
	}
	
	void OnGUI(){
		if(isEscopeta && isPlayer){
			print ("hola");
			w = new WWW("file://"+Application.dataPath+"/Texture/hud/escopeta.png");
			t = w.texture;
			Vector2 pos2d = Camera.main.WorldToScreenPoint(this.transform.position);
			//Vector2 pos2d = new Vector2(0,0);
			Rect pos = new Rect(pos2d.x-50,pos2d.y+100,500,100);
			GUI.Label(pos,t);
		}
	}
}