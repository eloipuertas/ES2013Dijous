using UnityEngine;
using System.Collections;

public class DynamicObjects : MonoBehaviour {
	
	private bool [] flags = {true,true};//this bool array ask if is needed to create a flag
	//timers of the objects
	private float [] timeShotguns = {0F,0F,0F,0F};//30F
	private float [] timeKatanas = {0F};//30F
	private float [] timeFlags = {0F,0F};//10F
	private float [] timeLife = {0F,0F,0F};//30F
	private float [] timeShields = {0F};//60F
	private float [] timeGranades = {0F,0F,0F};//30F
	private float [] timeGuns = {0F,0F};//30F
	
	// Use this for initialization
	void Start () {//creation of all dyynamic objects
		createShotguns();
		createKatanas();
		createLives();
		createFlags();
		createShields();
		createGranades();
		createGuns();
	
	}
	
	// Update is called once per frame
	void Update () {//check all the dynamic objects if need to be replaced
		checkShotguns();
		checkKatanas();
		checkLives();
		checkFlags();
		checkShields();
		checkGranades();
		checkGuns();
	}
	
	void checkShotguns(){
		bool respawn = false;
		for(int i=0;i<4;i++){
			if(GameObject.FindWithTag("escopeta_off"+(i+1)) == null){
				timeShotguns[i] -= Time.deltaTime;
				if(timeShotguns[i] <= 0F)
					respawn = true;
			}
			
		}
		if(respawn)
			createShotguns();
	}
	
	void checkKatanas(){
		bool respawn = false;
		for(int i=0;i<1;i++){
			if(GameObject.FindWithTag("katana"+(i+1)) == null){
				timeKatanas[i] -= Time.deltaTime;
				if(timeKatanas[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createKatanas();
	}
	
	void checkLives(){
		bool respawn = false;
		for(int i=0;i<3;i++){
			if(GameObject.Find("lifeUp"+(i+1)) == null){
				timeLife[i] -= Time.deltaTime;
				if(timeLife[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createLives();
	}
	
	void checkFlags(){
		bool respawn = false;
		for(int i=0;i<2;i++){
			if(flags[i]){
				timeLife[i] -= Time.deltaTime;
				if(timeLife[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createLives();
	}
	
	void checkShields(){
		bool respawn = false;
		for(int i=0;i<1;i++){
			if(GameObject.FindWithTag("shield"+(i+1)) == null){
				timeShields[i] -= Time.deltaTime;
				if(timeShields[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createShields();
	}
	
	void checkGranades(){
		bool respawn = false;
		for(int i=0;i<3;i++){
			if(GameObject.FindWithTag("granada"+(i+1)) == null){
				timeGranades[i] -= Time.deltaTime;
				if(timeGranades[i] <= 0F)
					respawn = true;
			}
			
		}
		if(respawn)
			createGranades();
	}
	
	void checkGuns(){
		bool respawn = false;
		for(int i=0;i<2;i++){
			if(GameObject.FindWithTag("gun"+(i+1)) == null){
				timeGuns[i] -= Time.deltaTime;
				if(timeGuns[i] <= 0F)
					respawn = true;
			}
			
		}
		if(respawn)
			createGuns();
	
	}
	
	void createFlags(){
		GameObject x;
		if(flags[0] && timeFlags[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/Bandera1")) as GameObject;
			x.tag = "bandera1";
			x.transform.position = new Vector3(16233.11F,802.9051F,-3.177107F);
			x.transform.eulerAngles = new Vector3(0F,-180F,0F);
			x.transform.localScale = new Vector3(1F,1F,0.8874608F);
			timeFlags[0] = 10F;
		}
		if(flags[0] && timeFlags[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/Bandera2")) as GameObject;
			x.tag = "bandera2";
			x.transform.position = new Vector3(-693.6163F,802.9051F,-3.1771073F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,0.8874608F);
			timeFlags[1] = 10F;
		}
	}
	
	void createShotguns(){
		GameObject x;
		if(GameObject.FindWithTag("escopeta_off1") == null && timeShotguns[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.tag = "escopeta_off1";
			x.transform.position = new Vector3(1409.115F,495.142F,-10.07793F);
			x.transform.eulerAngles = new Vector3(0F,270F,0F);
			x.transform.localScale = new Vector3(4437.306F,5000F,5000F);
			timeShotguns[0] = 30F;
		}
		if(GameObject.FindWithTag("escopeta_off2") == null && timeShotguns[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.tag = "escopeta_off2";
			x.transform.position = new Vector3(14065.52F,476.3956F,-10.07793F);
			x.transform.eulerAngles = new Vector3(0F,270F,0F);
			x.transform.localScale = new Vector3(4437.306F,5000F,5000F);
			timeShotguns[1] = 30F;
		}
		if(GameObject.FindWithTag("escopeta_off3") == null && timeShotguns[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.tag = "escopeta_off3";
			x.transform.position = new Vector3(3506.292F,680.0528F,-10.07793F);
			x.transform.eulerAngles = new Vector3(0F,270F,0F);
			x.transform.localScale = new Vector3(4437.306F,5000F,5000F);
			timeShotguns[2] = 30F;
		}
		if(GameObject.FindWithTag("escopeta_off4") == null && timeShotguns[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.tag = "escopeta_off4";
			x.transform.position = new Vector3(9816.015F,208.1602F,-10.07793F);
			x.transform.eulerAngles = new Vector3(0F,270F,0F);
			x.transform.localScale = new Vector3(4437.306F,5000F,5000F);
			timeShotguns[3] = 30F;
		}

	}
	
	void createKatanas(){
		GameObject x;
		if(GameObject.FindWithTag("katana1") == null && timeKatanas[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/katana")) as GameObject;
			x.tag = "katana1";
			x.transform.position = new Vector3(5664.778F,187.8907F,-29.59519F);
			x.transform.eulerAngles = new Vector3(0F,45F,47.6F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeKatanas[0] = 30F;
		}
		
	}
	
	void createLives(){//search is made with the name of the object in the Life case, because tag 'UpVida' is needed for interaction
		GameObject x;
		if(GameObject.Find("lifeUp1") == null && timeLife[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp1";
			x.transform.position = new Vector3(1402.825F,240.4666F,-3.585022F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			timeLife[0] = 30F;
		}
		if(GameObject.Find("lifeUp2") == null && timeLife[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp2";
			x.transform.position = new Vector3(12598.17F,237.2142F,-1.79776F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			timeLife[1] = 30F;
		}
		if(GameObject.Find("lifeUp3") == null && timeLife[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp3";
			x.transform.position = new Vector3(10815.18F,2.100188F,3.6277F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			timeLife[2] = 30F;
		}

		
	}
	
	void createGranades(){
		GameObject x;
		if(GameObject.FindWithTag("granada1") == null && timeGranades[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.tag = "granada1";
			x.transform.position = new Vector3(972.5556F,1053.084F,-7.311157F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[0] = 30F;
		}
		if(GameObject.FindWithTag("granada2") == null && timeGranades[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.tag = "granada2";
			x.transform.position = new Vector3(11973.91F,707.6677F,-6.947636F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[1] = 30F;
		}
		if(GameObject.FindWithTag("granada3") == null && timeGranades[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.tag = "granada3";
			x.transform.position = new Vector3(14075.15F,225.6484F,-8.314926F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[2] = 30F;
		}
	}
	
	void createShields(){
		GameObject x;
		if(GameObject.FindWithTag("shield1") == null && timeShields[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escudo")) as GameObject;
			x.tag = "shield1";
			x.transform.position = new Vector3(7530.288F,595.5858F,2.900288F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(50.72319F,26.71242F,41.89515F);
			timeShields[0] = 60F;
		}
	}
	
	void createGuns(){
		GameObject x;
		if(GameObject.FindWithTag("gun1") == null && timeGuns[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.tag = "gun1";
			x.transform.position = new Vector3(7530.288F,595.5858F,2.900288F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(50.72319F,26.71242F,41.89515F);
			timeGuns[0] = 30F;
		}
		if(GameObject.FindWithTag("gun2") == null && timeGuns[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.tag = "gun2";
			x.transform.position = new Vector3(7530.288F,595.5858F,2.900288F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(50.72319F,26.71242F,41.89515F);
			timeGuns[1] = 30F;
		}
	}
	
	void notifyFlagDistroyed(int team){
		flags[team-1] = true;
	}
}
