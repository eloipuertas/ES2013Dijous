using UnityEngine;
using System.Collections;

public class GestioBala : MonoBehaviour {

	private int equip;  //equip 1 -> player, equip 2 -> NPC
	
	public AudioSource audioDany;
	public GameObject sangPistola;
	public GameObject sangEscopeta;
	
	void Start() {
		GameObject.Destroy(gameObject, (float)0.6);
	}
	
	void OnCollisionEnter(Collision collision){
		
		if (collision.gameObject.tag == "NPC" && equip == 1){ //tir del player i impacte amb el NPC
			
			GameObject expl = null;
			audioDany.Play();
			
			ContactPoint contact = collision.contacts[0]; //punt de contacte de la bala amb el NPC
			
			if (gameObject.tag == "balaPistola") {
				expl = Instantiate(sangPistola, contact.point, Quaternion.identity) as GameObject;
				//fer dany al NPC (menys que l'escopeta)
			}
			else if (gameObject.tag == "balaEscopeta") {
				expl = Instantiate(sangEscopeta, contact.point, Quaternion.identity) as GameObject;
				//fer dany al NPC
			}
			
	    	Destroy(expl, (float)0.2);
		}

		GameObject.Destroy(gameObject);
		
	}
	
	public void setEquip(int e) {
		this.equip = e;
	}
	
	public int getEquip(int e) {
		return this.equip;
	}
	
}
