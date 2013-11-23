using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	
	//Atributos de personaje
	protected int health;
	protected int shield;
	protected GameObject primaryWeapon;
	protected GameObject secondaryWeapon;
	
	protected HUD hud =  null;
	protected GameManager gameManager = null;

	protected int weapon;
	
	protected const int WEAPON_KATANA = 1;
	protected const int WEAPON_ESCOPETA = 2;
	protected const int PHILO_TEAM = 1;
	protected const int ROBOT_TEAM = 2;
	
	protected int team;

	protected void setHealth(int n) {
		health = n;
		fireHealthNotification();
		if (health <= 0) {
			fireDeathNotification();
		}
	}
	
	protected void setShield(int n){
		shield = n;
		fireShieldNotification();
	}
		
	protected void fireHealthNotification() {
		if(hud)
			this.hud.notifyHealthChange(this.health);
	}
	protected void fireDeathNotification() {
		if(hud)
			this.gameManager.notifyPlayerDeath();	
	}
	
	protected void fireShieldNotification(){
		//if(hud)
		//	this.hud.notifyShieldChange(this.shield);
	}
	
	public string getPrimaryWeapon(){ return primaryWeapon.ToString(); }
	public string getSecondaryWeapon(){ return secondaryWeapon.ToString(); }
	
	public int getHealth(){ return health; }
	public void heal(int h){ setHealth(Mathf.Min(100,health+h)); }
	public void dealDamage(int damage) { 
		if(shield>0){
			int tmpShield = shield-damage;
			setShield(Mathf.Max(0,tmpShield));
			if(tmpShield<0){
				setHealth(health+tmpShield);
			}
		}else{
			setHealth(health - damage);
		}
	}
	
	public int getShield(){ return shield; }
	public void addShield(int s){ setShield(Mathf.Min(100,shield+s)); }
	
	public void setWeapon(int weapon){ this.weapon = weapon; }
	public int getWeapon(){ return weapon; }

	public void setTeam(int team){ this.team=team; }
	public int getTeam(){ return team; }
	
	public Vector3 getCoordinates(){ return transform.position; }
	
}
