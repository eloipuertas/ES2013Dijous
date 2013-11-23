using UnityEngine;
using System.Collections;

public class GestioGranada : MonoBehaviour {
	
	public GameObject explosioGranada;
	
	void OnCollisionEnter(Collision collision){
		
		GameObject expl = GameObject.Instantiate(explosioGranada, transform.position, Quaternion.identity) as GameObject;
		GameObject.Destroy(gameObject);
		
	}
}
