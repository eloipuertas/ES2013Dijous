using UnityEngine;
using System.Collections;

public class FlagManagement : MonoBehaviour {
	
	private AudioSource audioFlagObtained;
	private AudioSource audioFlagPlaced;
	GameObject mastil,tela;

	void Start () {
		audioFlagObtained = gameObject.AddComponent<AudioSource>();
		audioFlagPlaced = gameObject.AddComponent<AudioSource>();
		audioFlagObtained.clip = Resources.Load("sounds/flag") as AudioClip;
		audioFlagPlaced.clip = Resources.Load("sounds/flagPlaced") as AudioClip;
	}
	
	public void flagBase(bool b) {
		mastil.renderer.enabled = b;
		tela.renderer.enabled = b;	
	}
		
	public void setflagPlaced(int team) {
		audioFlagPlaced.Play();
		flagBase(true);
		flagDistroyed(team);
	}
	
	public void setflagObtained() {
		audioFlagObtained.Play();
		flagBase(false);
	}
	
	private void flagDistroyed(int team) {
		DynamicObjects d = (GameObject.Find("GameStartUp").GetComponent("DynamicObjects")) as DynamicObjects;
		d.notifyFlagDistroyed(team);
	}
	
	public void loadFlag(int team) {
		switch(team) {
			case 1: mastil = GameObject.Find("flagPhilo"+"/mastil");
					tela = GameObject.Find("flagPhilo"+"/tela");
					break;
			case 2: mastil = GameObject.Find("flagRobot"+"/mastil");
					tela = GameObject.Find("flagRobot"+"/tela");
					break;
				
			default: break;
		}
		flagBase(false);
	}
	
}
