﻿using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

	public bool isEscopeta = false, isRevolver = false, isMetralleta = false, isKatana = false, isRifle = false, isMina = false, isGranada = false;// booleans determinate what type of weapon is
	private bool isPlayer = false;//boolean that determinate if player is colliding or not with the weapon object
	private WWW w;//WWW class search file with a url parameter in it's constructor to get the content, can use //http //https //ftp(this one a little bit limited) and //file protocols, so i've used in this case for get the textures from a path 
	private Texture2D t;
	private bool activeInfo = true;
	private Rect posImg;
	private Rect posTextName;
	private Rect posTextDmg;
	private GUIStyle style;
	
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
	void Start(){
		style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 30;
	}
	
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
				printWeaponInfo("escopeta.png","Escopeta","Damage: 40");
			}
			
			if(isRevolver){//if is revolver type
				printWeaponInfo("revolver.png","Revolver","Damage: 15");
			}
			
			if(isKatana){//if is katana type
				printWeaponInfo("katana.png","Katana","Damage: 25");
			}
			
			if(isRifle){//if is rifle type
				printWeaponInfo("rifle.png","Rifle","Damage: 100");
			}
			
			if(isMina){//if is mina type
				printWeaponInfo("mina.png","Mina","Damage: 60");
			}
			
			if(isGranada){//if is granada type
				printWeaponInfo("granada.png","Granada","Damage: 60");
			}
			
			if(isMetralleta){//if is metralleta type
				printWeaponInfo("metralleta.png","Metralleta","Damage: 10");
			}
			
			
		}
	}
	
	//This function contains all is needed to paint on gui the weapon info
	void printWeaponInfo(string fileName,string TextName,string TextDmg){		
		w = new WWW("file://"+Application.dataPath+"/Resources/primarios/"+fileName);//Loading the weapon.png file
		t = w.texture;//getting the texture of the file
		Vector2 pos2d = GameObject.Find("Main Camera").camera.WorldToScreenPoint(this.transform.position);//getting the 2d possition of the weapon object at the main camera screen(this.transform.position is the 3d position of the weapon object)
		posImg = new Rect(pos2d.x-80,pos2d.y-100,500,100);
		posTextName = new Rect(pos2d.x-80,pos2d.y-130,500,100);
		posTextDmg = new Rect(pos2d.x-80,pos2d.y-105,500,100);
		GUI.Label(posImg,t);//painting the texture on gui
		GUI.Label(posTextName,TextName,style);//painting text info on gui
		GUI.Label(posTextDmg,TextDmg,style);
	
	}
}