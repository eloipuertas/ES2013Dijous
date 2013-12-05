using UnityEngine;
using System.Collections;

public class FlagManagement : MonoBehaviour {
	
	private AudioSource audioFlagObtained;
	private AudioSource audioFlagPlaced;

	void Start () {
		audioFlagObtained = gameObject.AddComponent<AudioSource>();
		audioFlagPlaced = gameObject.AddComponent<AudioSource>();
		audioFlagObtained.clip = Resources.Load("sounds/flag") as AudioClip;
		audioFlagPlaced.clip = Resources.Load("sounds/flagPlaced") as AudioClip;
	}
	
	public void flagBase(int team, bool b) {
		
		GameObject mastil = null, tela = null;
			
		switch(team) {
			case 1: mastil = GameObject.Find("flagPhilo"+"/mastil");
					tela = GameObject.Find("flagPhilo"+"/tela");
					break;
			case 2: mastil = GameObject.Find("flagRobot"+"/mastil");
					tela = GameObject.Find("flagRobot"+"/tela");
					break;
				
			default: break;
		}
			
		mastil.renderer.enabled = b;
		tela.renderer.enabled = b;
		
	}
		
	public void setflagPlaced(int team) {
		audioFlagPlaced.Play();
		flagBase(team, true);
		flagDistroyed(team);
	}
	
	public void setflagObtained(int team) {
		audioFlagObtained.Play();
		flagBase(team, false);
	}
	
	private void flagDistroyed(int team) {
		DynamicObjects d = (GameObject.Find("GameStartUp").GetComponent("DynamicObjects")) as DynamicObjects;
		d.notifyFlagDistroyed(team);
	}
	
}
