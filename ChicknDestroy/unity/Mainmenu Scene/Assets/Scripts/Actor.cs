using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	
	//Atributos de personaje
	protected int health;
	protected int shield;
	protected GameObject primaryWeapon;
	protected GameObject secondaryWeapon;
	
	protected HUD hud =  (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
	protected GameManager gameManager = null;

	public int weapon;
	
	public const int WEAPON_KATANA = 1;
	public const int WEAPON_ESCOPETA = 2;
	public const int WEAPON_PISTOLA = 3;
	public const int PHILO_TEAM = 1;
	public const int ROBOT_TEAM = 2;
	
	protected bool dead = false; // From AgentNpc.
	
	protected TimerPool timers;
	protected Weapon primary;
	protected ThrowableWeapon secondary;
	
	private bool flag;
	
	protected int team;
	
	// Added from PlayerController, must refactorice to AgentNpc
	protected int currentDirection;
	protected const int DIR_IZQUIERDA = 1;
	protected const int DIR_DERECHA = 2;
	
	// Sounds
	protected AudioSource sonidoDisparoPistola, sonidoDisparoEscopeta;
	public Parpadeig p; // Public visibility, in PlayerController was public.
	
	// GameObjects
	protected GameObject bala, granada, sortidaBalaDreta, sortidaBalaEsquerra;

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
	
	protected virtual void fireHealthNotification(){}
	protected virtual void fireDeathNotification(){}
	protected virtual void fireShieldNotification(){}
	
	protected void fireFlagnotification(bool taken, bool robot){ this.hud.notifyFlag(taken,robot); }
	protected void firePointsNotification(int points) {
		// Notify the current points to the HUD, it can be used by NPC team.
		this.hud.notifyPoints(this.team, points);
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
	
	void OnCollisionEnter(Collision collision) {
		// ADD the necessary code here.
	}
	
	public int getShield(){ return shield; }
	public void addShield(int s){ setShield(Mathf.Min(100,shield+s)); }
	
	protected virtual void updateModelWeapon(){}
	public void setWeapon(int weapon){
		// @LynosSorien -- Add the creation of the primary Weapon.
		this.weapon = weapon;
		switch(weapon) {
			case WEAPON_KATANA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.KATANA);
			break;
			case WEAPON_ESCOPETA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.SHOTGUN);
			break;
			case WEAPON_PISTOLA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.GUN);
			break;
		}
		updateModelWeapon();
	}
	public int getWeapon(){ return weapon; }

	public void setTeam(int team){ this.team=team; }
	public int getTeam(){ return team; }
	
	public Vector3 getCoordinates(){ return transform.position; }
	
	public bool isEnemy(Actor a){
		if (a == null) return false;
		return getTeam() != a.getTeam();
	}
}
