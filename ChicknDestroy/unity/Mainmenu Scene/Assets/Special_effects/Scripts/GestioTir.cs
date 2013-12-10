using UnityEngine;
using System.Collections;

public class GestioTir : MonoBehaviour {
	
	// @LynosSorien -- added damage attribute.
	private int damage;
	
	private int equip;
	private int arma;
	
	public AudioSource audioDany;
	public GameObject sangPistola;
	public GameObject sangEscopeta;
	public GameObject explosioGranada;
	
	void Start() {
		if (arma == 2 || arma == 3) GameObject.Destroy(gameObject, (float)0.6); //bala de pistola i escopeta 
		else GameObject.Destroy(gameObject, 2); //granada
			
	}
	
	void OnCollisionEnter(Collision collision){ // When collide
		
		GameObject expl = null;
		
		if (gameObject.tag == "granada")
				expl = GameObject.Instantiate(explosioGranada, transform.position, Quaternion.identity) as GameObject;
		
		Actor actor = collision.gameObject.GetComponent(typeof(Actor)) as Actor;
		if (isEnemy (actor)){
				
			audioDany.Play();
			
			ContactPoint contact = collision.contacts[0]; //punt de contacte de la bala amb el NPC
				
			switch(arma){
				case Actor.WEAPON_PISTOLA:
					expl = Instantiate(sangPistola, contact.point, Quaternion.identity) as GameObject;
					break;
				case Actor.WEAPON_ESCOPETA:
					expl = Instantiate(sangEscopeta, contact.point, Quaternion.identity) as GameObject;
					break;
				default: break;
			}
				
			actor.dealDamage(this.damage); //la granada tambe treu dany
			
			Destroy(expl, (float)0.2);			
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
	
	public void setDamage(int damage) {
		this.damage = damage;
	}
	
	public int getDamage() {
		return damage;
	}
	
	private bool isEnemy(Actor a){
		if (a == null) return false;
		return equip != a.getTeam();
	}
	
}
