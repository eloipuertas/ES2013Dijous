using UnityEngine;
using System.Collections;

public class GestioExplosioGranada : MonoBehaviour {
	
	public AudioSource audioExplosio;

	void Start () {
		GameObject.Destroy(gameObject, 1);
		audioExplosio.Play();
	}
	
}
