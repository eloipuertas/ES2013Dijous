using UnityEngine;
using System.Collections;

public class StartSetUp : MonoBehaviour {

	private int Chicken, Team;//Chicken means the chicken model's choice at StartGame scene, and Team the team's choice
	private ArrayList TeamList = new ArrayList();//the first element at this list will be the team chosen at StartGame scene
	void Start () {//at start the scene
		Chicken = PlayerPrefs.GetInt("Chicken");//we get the values from the startGame scene through PlayerPrefs
		Team = PlayerPrefs.GetInt("Team");
		
		if(Team == 2){//asigning the team chosen by the user
			TeamList.Add ("RoboChicken");
			TeamList.Add ("PhiloChicken");
		}else{
			TeamList.Add ("PhiloChicken");
			TeamList.Add ("RoboChicken");
		}
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
}
