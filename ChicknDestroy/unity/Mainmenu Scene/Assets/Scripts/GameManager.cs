using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	//public GameObject player;
	private GameCamera cam;
	
	void Start () {
		cam = GetComponent<GameCamera>();
		
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		cam.SetTarget(go.transform);
	}
}
