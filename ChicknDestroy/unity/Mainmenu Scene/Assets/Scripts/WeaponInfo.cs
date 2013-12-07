using UnityEngine;
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
	private HUD hud;
	
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
		hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
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
	
	void OnTriggerStay(Collider whoIs){
		if(isPlayer && Input.GetKey(KeyCode.E)){//if player is over the weapon object
			if(isEscopeta){//if is escopeta type
                changeWeapon(2, true, whoIs);
            }
            if(isRevolver){//if is revolver type
				changeWeapon(3, true, whoIs);
            }
            if(isKatana){//if is katana type
                changeWeapon(1, true, whoIs);
            }
            if(isRifle){//if is rifle type
                    
            }
            if(isMina){//if is mina type
                    
            }
            if(isGranada){//if is granada type
                //c.playAnimation();
                //c.weapon = 7;
                hud.notifySecondaryWeapon(1);
            }
            if(isMetralleta){//if is metralleta type
            }
            Destroy(this.gameObject);//once the weapon is picked up the object is destroyed			
		}
		else if(!isPlayer) {
			if(isEscopeta){//if is escopeta type
                changeWeapon(2, false, whoIs);
            }
            if(isRevolver){//if is revolver type
				changeWeapon(3, false, whoIs);
            }
            if(isKatana){//if is katana type
                changeWeapon(1, false, whoIs);
            }
            if(isRifle){//if is rifle type
                    
            }
            if(isMina){//if is mina type
                    
            }
            if(isGranada){//if is granada type
                //c.playAnimation();
                //c.weapon = 7;
                //hud.notifySecondaryWeapon(1);
            }
            if(isMetralleta){//if is metralleta type
            }
            Destroy(this.gameObject);//once the weapon is picked up the object is destroyed(isMetralleta){//if is metralleta type
		}
		
	}
	
	private void changeWeapon(int weapon, bool p, Collider whoIs) {
		if (p) { //player
			PlayerController c = whoIs.gameObject.GetComponent("PlayerController") as PlayerController;
			c.setWeapon(weapon);//we report to the Player that must change the weapon
			c.updateModelWeapon();
	        hud.notifyPrimaryWeapon(weapon);//we notify the the hud to change te primaryWeapon image
		}else {
			AgentNpc c = whoIs.gameObject.GetComponent("AgentNpc") as AgentNpc;
			c.setWeapon(weapon);//we report to the Player that must change the weapon
			c.updateModelWeapon();
		}
	}
	
	void OnGUI(){//show on gui
		if(isPlayer && activeInfo){//if player is over the weapon object
			if(isEscopeta){//if is escopeta type
				printWeaponInfo("primarios/escopeta.png","Escopeta","Damage: 40");
			}
			
			if(isRevolver){//if is revolver type
				printWeaponInfo("primarios/revolver.png","Revolver","Damage: 15");
			}
			
			if(isKatana){//if is katana type
				printWeaponInfo("primarios/katana.png","Katana","Damage: 25");
			}
			
			if(isRifle){//if is rifle type
				printWeaponInfo("primarios/rifle.png","Rifle","Damage: 100");
			}
			
			if(isMina){//if is mina type
				printWeaponInfo("secundarios/mina.png","Mina","Damage: 60");
			}
			
			if(isGranada){//if is granada type
				printWeaponInfo("secundarios/granada.png","Granada","Damage: 60");
			}
			
			if(isMetralleta){//if is metralleta type
				printWeaponInfo("primarios/metralleta.png","Metralleta","Damage: 10");
			}
			
			
		}
	}
	
	//This function contains all is needed to paint on gui the weapon info
	void printWeaponInfo(string fileName,string TextName,string TextDmg){		
		w = new WWW("file://"+Application.dataPath+"/Resources/"+fileName);//Loading the weapon.png file
		t = w.texture;//getting the texture of the file
		Vector2 pos2d = GameObject.Find("Main Camera").camera.WorldToScreenPoint(this.transform.position);//getting the 2d possition of the weapon object at the main camera screen(this.transform.position is the 3d position of the weapon object)
		posImg = new Rect(pos2d.x-50,pos2d.y+150,500,100);
		posTextName = new Rect(pos2d.x-50,pos2d.y+120,500,100);
		posTextDmg = new Rect(pos2d.x-50,pos2d.y+145,500,100);
		GUI.Label(posImg,t);//painting the texture on gui
		GUI.Label(posTextName,TextName,style);//painting text info on gui
		GUI.Label(posTextDmg,TextDmg,style);
	
	}
}