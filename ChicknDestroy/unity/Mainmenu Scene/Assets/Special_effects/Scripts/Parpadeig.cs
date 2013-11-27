using UnityEngine;
using System.Collections;

public class Parpadeig : MonoBehaviour {
	
	private GameObject cosPlayer;
	private GameObject armaPlayer;

	private int fetDany, tempsParpadejant;
	private double tempsActual, lastTime, tempsEntrat, minTime;

	void Start () {
		fetDany = 0;
		minTime = 0.15;
		tempsParpadejant = 2;
	}
	
	void Update () {
		if (fetDany == 1) {
			if ((Time.time - lastTime) > minTime) {
				if (cosPlayer.renderer.enabled) {
					cosPlayer.renderer.enabled = false;
					armaPlayer.renderer.enabled = false;
				}
				else {
					cosPlayer.renderer.enabled = true;
					armaPlayer.renderer.enabled = true;
				}
				lastTime = Time.time;
			}
			
			if (Time.time - tempsEntrat > tempsParpadejant){
				fetDany = 0;
				cosPlayer.renderer.enabled = true;
				armaPlayer.renderer.enabled = true;
			}
		}
	}
	
	void OnCollisionEnter(Collision collision){
		if( collision.gameObject.tag =="foc" || 
			collision.gameObject.tag =="punxes" || 
			collision.gameObject.tag =="guillotina" ||
			collision.gameObject.tag =="balaEscopeta" ||
			collision.gameObject.tag =="balaPistola"){
			if (fetDany == 0) {
				actualitza();
			}
		}
	}
	
	public void setCos(GameObject c) {
		this.cosPlayer = c;
	}
	
	public void setArma(GameObject a) {
		this.armaPlayer = a;
	}
	
	public void mostrarDany() {
		actualitza();
	}
	
	private void actualitza() {
		fetDany = 1;
		cosPlayer.renderer.enabled = false;
		armaPlayer.renderer.enabled = false;
		lastTime = Time.time;
		tempsEntrat = Time.time;
	}
	
}
