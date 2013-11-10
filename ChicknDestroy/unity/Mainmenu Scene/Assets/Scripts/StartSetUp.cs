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
		setPlayer();//setting Player
		setEnemyTeam();//setting enemy team
	}
	
	//This function sets the player inside the scene
	void setPlayer(){
		GameObject player = Instantiate(Resources.Load(TeamList[0]+""+Chicken)) as GameObject;//this function create a gameObject dynamically charging a prefab(prefabricated) at Resources folder
		player.transform.position = new Vector3((float)14700.0,(float)32.39664,(float)341.8011);//then we set all the parametrs the gameObject needs
		player.name = "Player";
		player.tag = "Player";
		PlayerPhysics PF = (PlayerPhysics)player.AddComponent("PlayerPhysics");
		PF.collisionMask = 256;
		PlayerController PC = (PlayerController)player.AddComponent("PlayerController");
		PC.gravity = 50;
		PC.speed = 200;
		PC.acceleration = 1000;
		PC.jumpHeight = 60;
		PC.sonidoSalto = (AudioSource)player.GetComponent("AudioSource");
	}
	//This function sets the enemy team, will be changed forward when more enemies will be needed
	void setEnemyTeam(){
		GameObject npc = Instantiate(Resources.Load(TeamList[1]+""+5)) as GameObject;//this function create a gameObject dynamically charging a prefab(prefabricated) at Resources folder
		npc.transform.position = new Vector3((float)15700.0,(float)32.39664,(float)341.8011);//then we set all the parametrs the gameObject needs
		npc.name = "NPC";
		npc.tag = "NPC";
		AgentNpc AN = (AgentNpc)npc.AddComponent("AgentNpc");
	}
	
	void setAllyTeam(){
	}
}
