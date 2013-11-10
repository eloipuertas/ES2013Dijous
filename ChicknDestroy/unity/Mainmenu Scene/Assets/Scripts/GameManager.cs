using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	private static int SCORE_TO_WIN = 1000;
	//public GameObject player;
	private GameCamera cam;
	private float loseOrWinTime = 5F;//time later the lose or win of a player (5 seconds Timer)
	private bool winConditionLastUpdate;
	private bool looseConditionLastUpdate;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		this.winConditionLastUpdate = false;
		this.looseConditionLastUpdate = false;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
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
		if(looseCondition() || winCondition())//when win or lose condition is true countdown the timer
			loseOrWinTime -= Time.deltaTime;
	}
	
	void OnGUI(){//Show the banner only when win condition or lose condition is true
		if(looseCondition())
			GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("lose") as  Texture);
		else if(winCondition())
			GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load ("win") as  Texture);
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
		if(loseOrWinTime <= 0F)//when 5 seconds of the timer elapsed return to main menu
			Application.LoadLevel(0);
	}
}
