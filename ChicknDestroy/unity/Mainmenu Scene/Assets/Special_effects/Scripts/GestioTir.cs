using UnityEngine;
using System.Collections;

public class GestioTir : MonoBehaviour {

	private int equip;  //equip 1 -> player, equip 2 -> NPC
	private int arma;
	
	public AudioSource audioDany;
	public GameObject sangPistola;
	public GameObject sangEscopeta;
	public GameObject explosioGranada;
	
	void Start() {
		GameObject.Destroy(gameObject, (float)0.8);		
	}
	
	void OnCollisionEnter(Collision collision){
		
		GameObject expl = null;
		
		if (gameObject.tag == "granada") {
				expl = GameObject.Instantiate(explosioGranada, transform.position, Quaternion.identity) as GameObject;
		} else {
			Actor actor = collision.gameObject.GetComponent(typeof(Actor)) as Actor;
			if (isEnemy (actor)){
				
				
				audioDany.Play();
				
				ContactPoint contact = collision.contacts[0]; //punt de contacte de la bala amb el NPC
				
				
				switch(arma){
					case Actor.WEAPON_PISTOLA:
						expl = Instantiate(sangPistola, contact.point, Quaternion.identity) as GameObject;
						actor.dealDamage(15);
						break;
					case Actor.WEAPON_ESCOPETA:
						expl = Instantiate(sangEscopeta, contact.point, Quaternion.identity) as GameObject;
						actor.dealDamage(40);
						break;
					default: break;
				}

				
				Destroy(expl, (float)0.2);
				
			}
			
			
		}
		

		

		GameObject.Destroy(gameObject);
		
	}

	public void setEquip(int e) {
		this.equip = e;
	}
	
	public int getEquip() {
		return this.equip;
	}
	
	public void setArma(int a) {
		this.arma = a;
	}
	
	public int getArma() {
		return this.arma;
	}
	
	private bool isEnemy(Actor a){
		if (a == null) return false;
		return equip != a.getTeam();
	}
	
}
