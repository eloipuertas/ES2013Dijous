using UnityEngine;
using System.Collections;

public class DynamicObjects : MonoBehaviour {
	
	private bool [] flags = {true,true};//this bool array ask if is needed to create a flag
	//timers of the objects
	private float [] timeShotguns = {0F,0F,0F,0F};//30F
	private float [] timeKatanas = {0F};//30F
	private float [] timeFlags = {0F,0F};//10F
	private float [] timeLife = {0F,0F,0F,0F,0F,0F,0F,0F,0F,0F};//30F
	private float [] timeShields = {0F};//60F
	private float [] timeGranades = {0F,0F,0F,0F,0F,0F};//30F
	private float [] timeGuns = {0F,0F,0F,0F};//30F
	private float [] timeAmmo = {0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F,0F};//20F
	
	// Use this for initialization
	void Start () {//creation of all dyynamic objects
		createShotguns();
		createKatanas();
		createLives();
		createFlags();
		createShields();
		createGranades();
		createGuns();
		createAmmo();
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
		checkAmmo();
	}
	
	void checkShotguns(){
		bool respawn = false;
		for(int i=0;i<4;i++){
			if(GameObject.Find("escopeta"+(i+1)) == null){
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
			if(GameObject.Find("katana"+(i+1)) == null){
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
		for(int i=0;i<10;i++){
			if(GameObject.Find("lifeUp"+(i+1)) == null){
				timeLife[i] -= Time.deltaTime;
				if(timeLife[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createLives();
	}
	
	void checkAmmo(){
		bool respawn = false;
		for(int i=0;i<16;i++){
			if(GameObject.Find("escopeta_off"+(i+1)) == null){
				timeAmmo[i] -= Time.deltaTime;
				if(timeAmmo[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createAmmo();
	}
	
	void checkFlags(){
		bool respawn = false;
		for(int i=0;i<2;i++){
			if(flags[i]){
				timeFlags[i] -= Time.deltaTime;
				if(timeFlags[i] <= 0F)
					respawn = true;
			}
		}
		if(respawn)
			createFlags();
	}
	
	void checkShields(){
		bool respawn = false;
		for(int i=0;i<1;i++){
			if(GameObject.Find("shield"+(i+1)) == null){
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
		for(int i=0;i<6;i++){
			if(GameObject.Find("granada"+(i+1)) == null){
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
		for(int i=0;i<4;i++){
			if(GameObject.Find("gun"+(i+1)) == null){
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
		if(flags[1] && timeFlags[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/Bandera2")) as GameObject;
			x.name = "Bandera";
			x.transform.position = new Vector3(16233.11F,802.9051F,-3.177107F);
			x.transform.eulerAngles = new Vector3(0F,-180F,0F);
			x.transform.localScale = new Vector3(1F,1F,0.8874608F);
			timeFlags[1] = 10F;
			flags[1] = false;
		}
		if(flags[0] && timeFlags[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/Bandera1")) as GameObject;
			x.name = "Bandera";
			x.transform.position = new Vector3(-693.6163F,802.9051F,-3.1771073F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,0.8874608F);
			timeFlags[0] = 10F;
			flags[0] = false;
		}
	}
	
	void createShotguns(){
		GameObject x;
		if(GameObject.Find("escopeta1") == null && timeShotguns[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta")) as GameObject;
			x.name = "escopeta1";
			x.tag = "escopeta";
			x.transform.position = new Vector3(1407.095F,495.142F,-16.07605F);
			x.transform.eulerAngles = new Vector3(0F,180F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeShotguns[0] = 30F;
		}
		if(GameObject.Find("escopeta2") == null && timeShotguns[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta")) as GameObject;
			x.name = "escopeta2";
			x.tag = "escopeta";
			x.transform.position = new Vector3(14049.33F,484.6809F,-1.233887F);
			x.transform.eulerAngles = new Vector3(0F,360F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeShotguns[1] = 30F;
		}
		if(GameObject.Find("escopeta3") == null && timeShotguns[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta")) as GameObject;
			x.name= "escopeta3";
			x.tag = "escopeta";
			x.transform.position = new Vector3(3505.329F,704.0554F,-20.12647F);
			x.transform.eulerAngles = new Vector3(0F,180F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeShotguns[2] = 30F;
		}
		
		if(GameObject.Find("escopeta4") == null && timeShotguns[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta")) as GameObject;
			x.name = "escopeta4";
			x.tag = "escopeta";
			x.transform.position = new Vector3(9819.947F,223.5915F,-10.07793F);
			x.transform.eulerAngles = new Vector3(0F,180F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeShotguns[3] = 30F;
		}

	}
	
	void createKatanas(){
		GameObject x;
		if(GameObject.Find("katana1") == null && timeKatanas[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/katana")) as GameObject;
			x.name = "katana1";
			x.tag = "katana";
			x.transform.position = new Vector3(5669.616F,212.7787F,-24.7574F);
			x.transform.eulerAngles = new Vector3(0F,45F,77.0473F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeKatanas[0] = 30F;
		}
		
	}
	
	void createLives(){//search is made with the name of the object in the Life case, because tag 'UpVida' is needed for interaction
		GameObject x;
		if(GameObject.Find("lifeUp1") == null && timeLife[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp1";
			x.transform.position = new Vector3(3997.094F,6.410399F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(793.3113F,1032.907F,1243.447F);
			x.animation.playAutomatically = true;
			timeLife[0] = 30F;
		}
		if(GameObject.Find("lifeUp2") == null && timeLife[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp2";
			x.transform.position = new Vector3(11937.47F,227.9631F,-17.9776F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(793.3113F,1032.907F,1243.447F);
			x.animation.playAutomatically = true;
			timeLife[1] = 30F;
		}
		if(GameObject.Find("lifeUp3") == null && timeLife[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp3";
			x.transform.position = new Vector3(12547.89F,666.6908F,-17.9776F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[2] = 30F;
		}
		if(GameObject.Find("lifeUp4") == null && timeLife[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp4";
			x.transform.position = new Vector3(10815.18F,2.100188F,3.6277F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[3] = 30F;
		}
		if(GameObject.Find("lifeUp5") == null && timeLife[4] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp5";
			x.transform.position = new Vector3(1402.825F,251.299F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[4] = 30F;
		}
		if(GameObject.Find("lifeUp6") == null && timeLife[5] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp6";
			x.transform.position = new Vector3(5355.549F,483.8425F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[5] = 30F;
		}
		if(GameObject.Find("lifeUp7") == null && timeLife[6] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp7";
			x.transform.position = new Vector3(1083.781F,1059.604F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[6] = 30F;
		}
		if(GameObject.Find("lifeUp8") == null && timeLife[7] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp8";
			x.transform.position = new Vector3(2930.491F,661.7186F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[7] = 30F;
		}
		if(GameObject.Find("lifeUp9") == null && timeLife[8] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp9";
			x.transform.position = new Vector3(174.4894F,-6.68808F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[8] = 30F;
		}
		if(GameObject.Find("lifeUp10") == null && timeLife[9] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/upVida")) as GameObject;
			x.name = "lifeUp10";
			x.transform.position = new Vector3(14652.32F,-88.96371F,-3.585088F);
			x.transform.eulerAngles = new Vector3(0F,20.76423F,0F);
			x.transform.localScale = new Vector3(780.8656F,1032.907F,1261.287F);
			x.animation.playAutomatically = true;
			timeLife[9] = 30F;
		}

	}
	
	void createAmmo(){
		GameObject x;
		if(GameObject.Find("escopeta_off1") == null && timeAmmo[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off1";
			x.transform.position = new Vector3(3792.807F,177.9243F,3.688934F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[0] = 20F;
		}
		if(GameObject.Find("escopeta_off2") == null && timeAmmo[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off2";
			x.transform.position = new Vector3(-1173.035F,228.3625F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[1] = 20F;
		}
		if(GameObject.Find("escopeta_off3") == null && timeAmmo[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off3";
			x.transform.position = new Vector3(1684.181F,-107.321F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[2] = 20F;
		}
		if(GameObject.Find("escopeta_off4") == null && timeAmmo[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off4";
			x.transform.position = new Vector3(9989.39F,-107.321F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[3] = 20F;
		}
		if(GameObject.Find("escopeta_off5") == null && timeAmmo[4] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off5";
			x.transform.position = new Vector3(14718.52F,-107.321F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[4] = 20F;
		}
		if(GameObject.Find("escopeta_off6") == null && timeAmmo[5] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off6";
			x.transform.position = new Vector3(16489.25F,82.34254F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[5] = 20F;
		}
		if(GameObject.Find("escopeta_off7") == null && timeAmmo[6] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off7";
			x.transform.position = new Vector3(2438.683F,113.2963F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[6] = 20F;
		}
		if(GameObject.Find("escopeta_off8") == null && timeAmmo[7] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off8";
			x.transform.position = new Vector3(4605.907F,367.8213F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[7] = 20F;
		}
		
		if(GameObject.Find("escopeta_off9") == null && timeAmmo[8] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off9";
			x.transform.position = new Vector3(2665.333F,645.7471F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[8] = 20F;
		}
		
		if(GameObject.Find("escopeta_off10") == null && timeAmmo[9] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off10";
			x.transform.position = new Vector3(7411.874F,-107.3926F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[9] = 20F;
		}
		if(GameObject.Find("escopeta_off11") == null && timeAmmo[10] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off11";
			x.transform.position = new Vector3(5537.748F,339.8465F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[10] = 20F;
		}
		if(GameObject.Find("escopeta_off12") == null && timeAmmo[11] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off12";
			x.transform.position = new Vector3(7618.475F,299.9227F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[11] = 20F;
		}
		if(GameObject.Find("escopeta_off13") == null && timeAmmo[12] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off13";
			x.transform.position = new Vector3(9815.473F,621.5377F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[12] = 20F;
		}
		if(GameObject.Find("escopeta_off14") == null && timeAmmo[13] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off14";
			x.transform.position = new Vector3(10484.96F,366.7649F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[13] = 20F;
		}
		if(GameObject.Find("escopeta_off15") == null && timeAmmo[14] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off15";
			x.transform.position = new Vector3(12811.26F,651.9682F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[14] = 20F;
		}
		if(GameObject.Find("escopeta_off16") == null && timeAmmo[15] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escopeta_off")) as GameObject;
			x.name = "escopeta_off16";
			x.transform.position = new Vector3(12481.17F,113.3352F,3.6889345F);
			x.transform.eulerAngles = new Vector3(90F,90F,0F);
			x.transform.localScale = new Vector3(1900F,1900F,350F);
			timeAmmo[15] = 20F;
		}

		
	}
	
	void createGranades(){
		GameObject x;
		if(GameObject.Find("granada1") == null && timeGranades[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada1";
			x.transform.position = new Vector3(14044.48F,218.9833F,-8.314926F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[0] = 30F;
		}
		if(GameObject.Find("granada2") == null && timeGranades[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada2";
			x.transform.position = new Vector3(11973.91F,706.0176F,-6.947636F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[1] = 30F;
		}
		if(GameObject.Find("granada3") == null && timeGranades[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada3";
			x.transform.position = new Vector3(972.5556F,1053.084F,-7.311157F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[2] = 30F;
		}
		if(GameObject.Find("granada4") == null && timeGranades[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada4";
			x.transform.position = new Vector3(14502.08F,1059.225F,-9.998901F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[3] = 30F;
		}
		if(GameObject.Find("granada5") == null && timeGranades[4] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada5";
			x.transform.position = new Vector3(7300.961F,228.5064F,-9.998901F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[4] = 30F;
		}
		if(GameObject.Find("granada6") == null && timeGranades[5] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/granada")) as GameObject;
			x.name = "granada6";
			x.transform.position = new Vector3(1909.551F,205.0481F,-9.998901F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGranades[5] = 30F;
		}
	}
	
	void createShields(){
		GameObject x;
		if(GameObject.Find("shield1") == null && timeShields[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/escudo")) as GameObject;
			x.name = "shield1";
			x.transform.position = new Vector3(7556.927F,610.6569F,2.900288F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(36.87547F,19.41978F,30.45753F);
			timeShields[0] = 60F;
		}
	}
	
	void createGuns(){
		GameObject x;
		if(GameObject.Find("gun1") == null && timeGuns[0] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.name = "gun1";
			x.tag = "pistola";
			x.transform.position = new Vector3(-291.5449F,115.6344F,-8.241943F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGuns[0] = 30F;
		}
		if(GameObject.Find("gun2") == null && timeGuns[1] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.name = "gun2";
			x.tag = "pistola";
			x.transform.position = new Vector3(15587.34F,220.8885F,-8.241943F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGuns[1] = 30F;
		}
		if(GameObject.Find("gun3") == null && timeGuns[2] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.name = "gun3";
			x.tag = "pistola";
			x.transform.position = new Vector3(16131.87F,981.4404F,-8.241943F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGuns[2] = 30F;
		}
		if(GameObject.Find("gun4") == null && timeGuns[3] <= 0){
			x = Instantiate(Resources.Load("ObjectPrefabs/gun")) as GameObject;
			x.name = "gun4";
			x.tag = "pistola";
			x.transform.position = new Vector3(-626.856F,981.4404F,-8.241943F);
			x.transform.eulerAngles = new Vector3(0F,0F,0F);
			x.transform.localScale = new Vector3(1F,1F,1F);
			timeGuns[3] = 30F;
		}
	}
	
	public void notifyFlagDistroyed(int team){
		flags[team-1] = true;
	}
}
