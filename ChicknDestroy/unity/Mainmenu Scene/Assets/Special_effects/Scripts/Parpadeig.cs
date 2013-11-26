using UnityEngine;
using System.Collections;

public class Parpadeig : MonoBehaviour {
	
	private GameObject cosPlayer;
	//public GameObject armaPlayer;

	private int fetDany, tempsParpadejant;
	private double tempsActual, lastTime, tempsEntrat, minTime;

	// Use this for initialization
	void Start () {
		fetDany = 0;
		minTime = 0.15;
		tempsParpadejant = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (fetDany == 1) {
			if ((Time.time - lastTime) > minTime) {
				if (cosPlayer.renderer.enabled) {
					cosPlayer.renderer.enabled = false;
					//armaPlayer.renderer.enabled = false;
				}
				else {
					cosPlayer.renderer.enabled = true;
					//armaPlayer.renderer.enabled = true;
				}
				lastTime = Time.time;
			}
			
			if (Time.time - tempsEntrat > tempsParpadejant){
				fetDany = 0;
				cosPlayer.renderer.enabled = true;
				//armaPlayer.renderer.enabled = true;
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag =="foc" || collision.gameObject.tag =="punxes" || collision.gameObject.tag =="guillotina"){
			if (fetDany == 0) {
				fetDany = 1;
				cosPlayer.renderer.enabled = false;
				//armaPlayer.renderer.enabled = false;
				lastTime = Time.time;
				tempsEntrat = Time.time;
			}
		}
	}
	
	public void setCos(GameObject c) {
		this.cosPlayer = c;
	}
	
}
