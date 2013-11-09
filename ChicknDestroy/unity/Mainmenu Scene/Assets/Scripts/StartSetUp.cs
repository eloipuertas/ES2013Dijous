using UnityEngine;
using System.Collections;

public class StartSetUp : MonoBehaviour {

	private int Chicken, Team;
	private ArrayList TeamList = new ArrayList();
	void Start () {
		Chicken = PlayerPrefs.GetInt("Chicken");
		Team = PlayerPrefs.GetInt("Team");
		
		if(Team == 2){
			TeamList.Add ("RoboChicken");
			TeamList.Add ("PhiloChicken");
		}else{
			TeamList.Add ("PhiloChicken");
			TeamList.Add ("RoboChicken");
		}
		setPlayer();
		setEnemyTeam();
	}
	
	void setPlayer(){
		GameObject player = Instantiate(Resources.Load(TeamList[0]+""+Chicken)) as GameObject;
		player.transform.position = new Vector3((float)14700.0,(float)32.39664,(float)341.8011);
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
	
	void setEnemyTeam(){
		GameObject npc = Instantiate(Resources.Load(TeamList[1]+""+1)) as GameObject;
		npc.transform.position = new Vector3((float)15700.0,(float)32.39664,(float)341.8011);
		npc.name = "NPC";
		npc.tag = "NPC";
		AgentNpc AN = (AgentNpc)npc.AddComponent("AgentNpc");
	}
	
	void setAllyTeam(){
	}
}
