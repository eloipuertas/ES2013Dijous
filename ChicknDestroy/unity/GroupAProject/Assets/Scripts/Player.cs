using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("PosX")){
			Vector3 initPos = new Vector3(PlayerPrefs.GetFloat("PosX"),PlayerPrefs.GetFloat("PosY"),PlayerPrefs.GetFloat("PosZ"));
			transform.position = initPos;
		}
		
	}
	
}
