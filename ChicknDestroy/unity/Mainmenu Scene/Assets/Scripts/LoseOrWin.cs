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
		time -= Time.deltaTime;//on frame update Time.deltatime returns de time elapsed since the last frame, that's to say the time this frame has spent on load
		if(time <= 0)//when the 10 seconds has past load the main menu scene, this conditional may be used at the final product as helath = 0 or gamepoints = X 
			Application.LoadLevel(0);//this is the line that loads the main menu scene
	}
}
