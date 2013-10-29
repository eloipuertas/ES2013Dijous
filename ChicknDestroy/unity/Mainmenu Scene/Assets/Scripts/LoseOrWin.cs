using UnityEngine;
using System.Collections;

public class LoseOrWin : MonoBehaviour {
	float time;//this is the var of the countdown timer
	// Use this for initialization
	void Start () {
		this.time = 10;//we set this variable to 10s
	}
	
	// Update is called once per frame
	void Update () {
		bool endRequested = false;
		if (winCondition()) {
			endRequested = true;
		}
		if (looseCondition()){
			endRequested = true;
		}
		if (endRequested) {
			endGame();
		}
	}
	
	bool winCondition () {
		return false;	
	}
	
	bool looseCondition () {
		return true;	
	}
	
	
	void endGame() {
		Application.LoadLevel(0);
	}
}
