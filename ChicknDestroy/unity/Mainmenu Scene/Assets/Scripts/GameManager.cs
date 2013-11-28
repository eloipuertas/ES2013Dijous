using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static int SCORE_TO_WIN = 800;
	//public GameObject player;
	private GameCamera cam;
	public bool GameSelRobot = true;
	public int gravity = 800;
	private float loseOrWinTime = 5F;//time later the lose or win of a player (5 seconds Timer)
	private bool winConditionLastUpdate;
	private bool looseConditionLastUpdate;
	
	public AudioSource audioLoose, audioAmbient, audioWin;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		this.winConditionLastUpdate = false;
		this.looseConditionLastUpdate = false;
		Physics.gravity = new Vector3(0, -gravity, 0);
		if(PlayerPrefs.GetInt("Team") == 1)
			cam.transform.position = new Vector3(15700F,26.20233F,-643.3362F);
		/*
		GameObject go;
		
		if (GameSelRobot)
			go = GameObject.FindGameObjectWithTag("Player");
		else 
			go = GameObject.FindGameObjectWithTag("NPC");
		
		
		cam.SetTarget(go.transform);*/
	}
	// Update is called once per frame
	void Update () {
		bool endRequested = false;
		if (winCondition()) {
			if (!audioWin.isPlaying) {
				audioAmbient.Stop();
				audioWin.Play();
			}
			endRequested = true;
			fireWinNotification();
		}
		if (looseCondition()){
			if (!audioLoose.isPlaying) {
				audioAmbient.Stop();
				audioLoose.Play();
			}
			endRequested = true;
			fireLooseNotification();
		}
		if (endRequested) {
			endGame();
		}
		if(looseCondition() || winCondition())//when win or lose condition is true countdown the timer
			loseOrWinTime -= Time.deltaTime;
	}
	
	void OnGUI(){//Show the banner only when win condition or lose condition is true
		if(looseCondition()){
			if(PlayerPrefs.GetInt("Team") == 2)
				GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("RoboLose") as  Texture);
			else
				GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("PhiloLose") as  Texture);
		}
		else if(winCondition()){
			if(PlayerPrefs.GetInt("Team") == 2)
				GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("RoboWin") as  Texture);
			else
				GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("PhiloWin") as  Texture);
			
		}
	}
	
	private void fireWinNotification() {
		
	}
	private void fireLooseNotification() {
		
	}
	
	public void notifyPlayerDeath() {
		this.looseConditionLastUpdate = true;
	}
	
	public void notifyScoreChange(int score) {
		this.winConditionLastUpdate = score >= SCORE_TO_WIN;			
	}
	
	public bool winCondition () {
		return winConditionLastUpdate;	
	}
	
	public bool looseCondition () {
		return looseConditionLastUpdate;	
	}
	
	private void endGame() {
		if(loseOrWinTime <= 0F || Input.anyKeyDown)//when 5 seconds of the timer elapsed return to main menu
			Application.LoadLevel(0);
	}
	
	public void setTarget(Transform tr){
		cam.SetTarget(tr.transform);
		cam.transform.position = new Vector3(tr.transform.position.x, tr.transform.position.y, cam.transform.position.z);
	}
}
