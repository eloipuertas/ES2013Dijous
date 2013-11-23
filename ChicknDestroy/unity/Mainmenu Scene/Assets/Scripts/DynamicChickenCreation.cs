using UnityEngine;
using System.Collections;

public class DynamicChickenCreation : MonoBehaviour {
	public const int PHILO_TEAM = 1;
	public const int ROBOT_TEAM = 2;
	
	private int Chicken, Team;//Chicken means the chicken model's choice at StartGame scene, and Team the team's choice
	//private ArrayList TeamList = new ArrayList();//the first element at this list will be the team chosen at StartGame scene
	void Start () {//at start the scene
		
		Chicken = PlayerPrefs.GetInt("Chicken");//we get the values from the startGame scene through PlayerPrefs
		Team = PlayerPrefs.GetInt("Team");
		
		Chicken = 1;
		Team = ROBOT_TEAM;	
		
		//setPlayer();
		//setAllyTeam();

		
		setEnemyTeam();
	}
	
	//This function sets the player inside the scene
	void setPlayer(){
		GameObject ob = CreateChicken(Team,Chicken);	
		initAsPlayer(ob);	// EQUIPO A ACABAD ESTA FUNCION
	}
	
	void setEnemyTeam(){
		/*
	 	* CREATE HERE THE ENEMY TEAM DINAMICALLY
		*/
		int enemyTeam = (Team == PHILO_TEAM)? ROBOT_TEAM:PHILO_TEAM;
		for(int i=1; i<6; i++){
			GameObject ob = CreateChicken(enemyTeam,i);	
			initAsNPC(ob,false,i);	
		}
	}
	
	void setAllyTeam(){
		/*
	 	* CREATE HERE THE ALLY TEAM DINAMICALLY
		*/
		for(int i=1; i<6; i++){
			if (i==Chicken) continue;
			GameObject ob = CreateChicken(Team,i);	
			initAsNPC(ob,true,i);
		}
	}
	
	
	/*NOTE: Check out which chicken number is related to the choose menu*/
	/* Example code:
	 * 
	 * GameObject c = Instantiate(Resource.Load("ChickenPrefabs/RoboChicken5")) as GameObject;//this means the prefabs must be at the Resources/chickenPrefabs folder
	 * 
	 * 
	 * */
	
	
	GameObject setRoboChicken1(){
		return null;
	}
	
	GameObject setRoboChicken2(){
		return null;
	}
	
	GameObject setRoboChicken3(){
		return null;
	}
	
	GameObject setRoboChicken4(){
		return null;
	}
	
	GameObject setRoboChicken5(){
		return null;
	}
	
	GameObject setPhiloChicken1(){
		GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
		return c;
	}
	
	GameObject setPhiloChicken2(){
		GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_crazy")) as GameObject;
		return c;
	}
	
	GameObject setPhiloChicken3(){
		GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
		return c;
	}
	
	GameObject setPhiloChicken4(){
		GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
		return c;
	}
	
	GameObject setPhiloChicken5(){
		GameObject c = Instantiate(Resources.Load("ChickenPrefabs/Philo_leader")) as GameObject;
		return c;
	}
	
	
	void initAsPlayer(GameObject c){
		c.AddComponent<PlayerController>();
		// Y mas cosas
	}
	
	private int alliedNum = 1;
	void initAsNPC(GameObject c,bool allied, int chicken){
		if (allied){
			switch(alliedNum){
				case 1:
					c.transform.position = new Vector3(100f,-91.74977f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta1";
					break;
				case 2:
					c.transform.position = new Vector3(100f,-91.74977f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta2";
					break;
				case 3:
					c.transform.position = new Vector3(150f,-91.74977f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta1";
					break;
				default: // case 4
					c.transform.position = new Vector3(150f,-91.74977f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta2";
					break;
			}
			alliedNum++;
			
		}else{
			switch(chicken){
				case 1:
					c.transform.position = new Vector3(2884.105f,-91.74977f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta3";
					break;
				case 2:
					c.transform.position = new Vector3(6099.58f,118.5825f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta4";
					break;
				case 3:
					c.transform.position = new Vector3(7870.028f,-81.72346f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta5";
					break;
				case 4:
					c.transform.position = new Vector3(10051.11f,118.5825f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta6";
					break;
				default: // case 5
					c.transform.position = new Vector3(14806.89f,414.3137f,0.8170023f);
					c.AddComponent<AgentNpc>().direrutas = "ruta7";
					break;
			}
		}
	}
	
	GameObject CreateChicken(int team,int chickenNum){
		
		GameObject chicken = null;
		
		if(team == PHILO_TEAM){
			switch(chickenNum){
				case 1:
					chicken = setPhiloChicken1();
				break;
				
				case 2:
					chicken = setPhiloChicken2();
				break;
				
				case 3:
					chicken = setPhiloChicken3();
				break;
				
				case 4:
					chicken = setPhiloChicken4();
				break;
				
				case 5:
					chicken = setPhiloChicken5();
				break;
				
			
			}
		}else if(team == ROBOT_TEAM){
			switch(chickenNum){
				case 1:
					chicken = setRoboChicken1();
				break;
				
				case 2:
					chicken = setRoboChicken2();
				break;
				
				case 3:
					chicken = setRoboChicken3();
				break;
				
				case 4:
					chicken = setRoboChicken4();
				break;
				
				case 5:
					chicken = setRoboChicken5();
				break;
				
			
			}
		}
		return chicken;
	}
	
}
