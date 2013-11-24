using UnityEngine;
using System.Collections;

public class GestioTir : MonoBehaviour {

	private int equip;  //equip 1 -> player, equip 2 -> NPC
	
	public AudioSource audioDany;
	public GameObject sangPistola;
	public GameObject sangEscopeta;
	public GameObject explosioGranada;
	
	void Start() {
		GameObject.Destroy(gameObject, (float)0.6);
	}
	
	void OnCollisionEnter(Collision collision){
		
		//l'explosio s'ha de veure sempre, toqui amb el NPC o no
		if (gameObject.tag == "granada") {
				//gestionar aqui el dany si toca un de l'equip rival, comprovar que sigui de l'altre equip...
				GameObject expl = GameObject.Instantiate(explosioGranada, transform.position, Quaternion.identity) as GameObject;
		}
		
		else if (collision.gameObject.tag == "NPC" && equip == 1){ //tir del player i impacte amb el NPC
			
			audioDany.Play();
			GameObject expl = null;
			
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
