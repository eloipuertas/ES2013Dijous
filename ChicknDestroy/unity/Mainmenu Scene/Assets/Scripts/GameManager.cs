using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static int SCORE_TO_WIN = 1000;
	//public GameObject player;
	private GameCamera cam;
	public bool GameSelRobot = true;
	
	private bool winConditionLastUpdate;
	private bool looseConditionLastUpdate;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		this.winConditionLastUpdate = false;
		this.looseConditionLastUpdate = false;
		
		GameObject go;
		
		if (GameSelRobot)
			go = GameObject.FindGameObjectWithTag("Player");
		else 
			go = GameObject.FindGameObjectWithTag("NPC");
		
		
		cam.SetTarget(go.transform);
	}
	// Update is called once per frame
	void Update () {
		bool endRequested = false;
		if (winCondition()) {
			endRequested = true;
			fireWinNotification();
		}
		if (looseCondition()){
			endRequested = true;
			fireLooseNotification();
		}
		if (endRequested) {
			endGame();
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
		Application.LoadLevel(0);
	}
}
