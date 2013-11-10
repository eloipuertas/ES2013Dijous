using UnityEngine;
using System.Collections;

public class EndGameConditions : MonoBehaviour {
	
	private bool start = false;
	private string Banner;
	private GUIStyle style;
	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("Team") == 1)//we select which banner to show depending on the team chosen
			Banner = "goalsPhilo";
		else
			Banner = "goalsRobo";
		
		style = new GUIStyle();
		style.normal.textColor = Color.red;
		style.fontSize = 30;
		Time.timeScale = 0F;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){//when player press escape we signal to stop showing the banner and set the game timeScale at 1F
			start = true;
			Time.timeScale = 1F;
		}
	}
	
	void OnGUI(){
		if(!start){//we show the banner til the player press escape
			GUI.DrawTexture(new Rect(Screen.width/4,Screen.height/8,500,500),Resources.Load (Banner) as  Texture);
			GUI.Label(new Rect(Screen.width/2.7F,Screen.height-40,500,500),"Press 'ESC' to Start",style);
		}
	}
	
}
