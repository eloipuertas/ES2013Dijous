using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

	public bool isEscopeta = false, isRevolver = false, isMetralleta = false, isKatana = false, isRifle = false, isMina = false, isGranada = false;// booleans determinate what type of weapon is
	private bool isPlayer = false;//boolean that determinate if player is colliding or not with the weapon object
	WWW w;//WWW class search file with a url parameter in it's constructor to get the content, can use //http //https //ftp(this one a little bit limited) and //file protocols, so i've used in this case for get the textures from a path 
	Texture2D t;
	bool activeInfo = true;
	
	/*void OnCollisionEnter(Collision whoIs) {//we set at true when player enters in the weapon object box collider
        if(whoIs.gameObject.CompareTag("Player")){
			isPlayer = true;	
		}
    }
	
	void OnCollisionExit(Collision whoIs){//we set at false when player leaves  the weapon object box collider
		 if(whoIs.gameObject.CompareTag("Player")){
			isPlayer = false;	
		}
	}*/
	
	
	void OnTriggerEnter(Collider whoIs) {//we set at true when player enters in the weapon object box collider
        if(whoIs.CompareTag("Player")){
			isPlayer = true;	
		}
    }
	
	void OnTriggerExit(Collider whoIs){//we set at false when player leaves  the weapon object box collider
		 if(whoIs.CompareTag("Player")){
			isPlayer = false;	
		}
	}
	
	void OnGUI(){//show on gui
		if(isPlayer && activeInfo){//if player is over the weapon object
			if(isEscopeta){//if is escopeta type
				print("file://"+Application.dataPath+"/Resources/primarios/escopeta.png");
				w = new WWW("file://"+Application.dataPath+"/Resources/primarios/escopeta.png");//Loading the escopeta.png file
				t = w.texture;//getting the texture of the file
				Vector2 pos2d = GameObject.Find("Main Camera").camera.WorldToScreenPoint(this.transform.position);//getting the 2d possition of the weapon object at the main camera screen(this.transform.position is the 3d position of the weapon object)
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Escopeta",style);
				GUI.Label(pos2,"Damage: 30",style);
				//you could also add a gui text saying: "dude this is a shotgun" but i think that with an image of the weapon is already pretty obvious
			}
			
			if(isRevolver){//if is revolver type
				w = new WWW("file://"+Application.dataPath+"/Resources/primarios/revolver.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Revolver",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			if(isKatana){//if is katana type
				w = new WWW("file://"+Application.dataPath+"/Resources/primarios/katana.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find ("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Katana",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			if(isRifle){//if is rifle type
				w = new WWW("file://"+Application.dataPath+"/Resources/primarios/rifle.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find ("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Rifle",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			if(isMina){//if is mina type
				w = new WWW("file://"+Application.dataPath+"/Resources/secundarios/mina.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find ("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Mina",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			if(isGranada){//if is granada type
				w = new WWW("file://"+Application.dataPath+"/Resources/secundarios/granada.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find ("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Granada",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			if(isMetralleta){//if is metralleta type
				w = new WWW("file://"+Application.dataPath+"/Resources/primarios/metralleta.png");
				t = w.texture;
				Vector2 pos2d = GameObject.Find ("Main Camera").camera.WorldToScreenPoint(this.transform.position);
				Rect pos = new Rect(pos2d.x-80,pos2d.y-100,500,100);
				Rect pos1 = new Rect(pos2d.x-80,pos2d.y-130,500,100);
				Rect pos2 = new Rect(pos2d.x-80,pos2d.y-105,500,100);
				GUIStyle style = new GUIStyle();
				style.normal.textColor = Color.red;
				style.fontSize = 30;
				GUI.Label(pos,t);//painting the texture on gui
				GUI.Label(pos1,"Metralleta",style);
				GUI.Label(pos2,"Damage: 30",style);
				GUI.Label(pos,t);
			}
			
			
		}
	}
}