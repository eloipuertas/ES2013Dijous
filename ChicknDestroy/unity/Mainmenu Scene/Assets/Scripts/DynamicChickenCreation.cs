using UnityEngine;
using System.Collections;

public class DynamicChickenCreation : MonoBehaviour {

	private int Chicken, Team;//Chicken means the chicken model's choice at StartGame scene, and Team the team's choice
	//private ArrayList TeamList = new ArrayList();//the first element at this list will be the team chosen at StartGame scene
	void Start () {//at start the scene
		Chicken = PlayerPrefs.GetInt("Chicken");//we get the values from the startGame scene through PlayerPrefs
		Team = PlayerPrefs.GetInt("Team");
		
		/*if(Team == 2){//asigning the team chosen by the user
			TeamList.Add ("RoboChicken");
			TeamList.Add ("PhiloChicken");
		}else{
			TeamList.Add ("PhiloChicken");
			TeamList.Add ("RoboChicken");
		}*/
		setPlayer();
		setAllyTeam();
		setEnemyTeam();
	}
	
	//This function sets the player inside the scene
	void setPlayer(){
		/*
		 * CREATE HERE THE PLAYER DINAMICALLY
		 */
	}
	
	void setEnemyTeam(){
		/*
	 	* CREATE HERE THE ENEMY TEAM DINAMICALLY
		*/
	}
	
	void setAllyTeam(){
		/*
	 	* CREATE HERE THE ALLY TEAM DINAMICALLY
		*/
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
		return null;
	}
	
	GameObject setPhiloChicken2(){
		return null;
	}
	
	GameObject setPhiloChicken3(){
		return null;
	}
	
	GameObject setPhiloChicken4(){
		return null;
	}
	
	GameObject setPhiloChicken5(){
		return null;
	}
	
	GameObject CreateChicken(int team,int chickenNum){
		
		GameObject chicken = null;
		
		if(team == 1){
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
		}else if(team == 2){
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
