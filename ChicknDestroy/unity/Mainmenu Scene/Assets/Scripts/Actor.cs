using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour {
	
	//Atributos de personaje
	protected int health;
	protected int shield;
	protected GameObject primaryWeapon;
	protected GameObject secondaryWeapon;
	
	protected HUD hud = (HUD) (GameObject.Find("HUD").GetComponent("HUD"));
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
	
	protected bool flag;
	
	protected int team;
	
	// Added from PlayerController, must refactorice to AgentNpc
	protected int currentDirection;
	protected const int DIR_IZQUIERDA = 1;
	protected const int DIR_DERECHA = 2;
	
	// Sounds
	protected AudioSource sonidoDisparoPistola, sonidoDisparoEscopeta;
	protected AudioSource sonidoSalto, sonidoPowerUp, sonidoEscudo, audioKatana, audioGrenade, audioMachineGun;
	
	protected FlagManagement flagManagement;
	public Parpadeig p; // Public visibility, in PlayerController was public.
	
	// GameObjects
	protected GameObject bala, granada, sortidaBalaDreta, sortidaBalaEsquerra, detected, sang;
	
	/*
	 * 
	 */ 
	protected void initSounds() { // -- Refactor to Actor.cs
		sonidoSalto = gameObject.AddComponent<AudioSource>();
		sonidoSalto.clip = Resources.Load("sounds/saltar") as AudioClip;
		
		sonidoPowerUp = gameObject.AddComponent<AudioSource>();
		sonidoPowerUp.clip = Resources.Load("sounds/power_up_curt") as AudioClip;
		
		sonidoEscudo = gameObject.AddComponent<AudioSource>();
		sonidoEscudo.clip = Resources.Load("sounds/so_escut") as AudioClip;
		
		sonidoDisparoPistola = gameObject.AddComponent<AudioSource>();
		sonidoDisparoPistola.clip = Resources.Load("sounds/tir_pistola_015") as AudioClip;
		
		sonidoDisparoEscopeta = gameObject.AddComponent<AudioSource>();
		sonidoDisparoEscopeta.clip = Resources.Load("sounds/tir_escopeta_0849") as AudioClip;
		
		audioMachineGun = gameObject.AddComponent<AudioSource>();
		audioMachineGun.clip = Resources.Load("sounds/machine_gun") as AudioClip;
		
		audioMachineGun = gameObject.AddComponent<AudioSource>();
		audioMachineGun.clip = Resources.Load("sounds/machine_gun") as AudioClip;
		
		audioKatana = gameObject.AddComponent<AudioSource>();
		audioKatana.clip = Resources.Load("sounds/katana") as AudioClip;

		audioGrenade = gameObject.AddComponent<AudioSource>();
		audioGrenade.clip = Resources.Load("sounds/granada_voice") as AudioClip;
	}
	
	protected void setHealth(int n) {
		health = n;
		//fireHealthNotification();
		if (this.GetType () == typeof(PlayerController)) this.hud.notifyHealthChange(this.health);
		if (health <= 0) {
			//fireDeathNotification();
			if(this.GetType() == typeof(PlayerController))this.gameManager.notifyPlayerDeath();
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
	/* ==========================================================================
	 * @LynosSorien -- Attack methods.
	 * ==========================================================================
	 */
	// Used for DistanceWeapon Only.
	protected bool doPrimaryAttack() {
		if (((DistanceWeapon)primary).attack ()) {
			GameObject nouTir = null;
			// The velocity vector (currently (+-1000,0,0) can be changed with parametter "velocity" of DistanceWeapon.
			int velocity = ((DistanceWeapon)this.primary).getVelocity();
			switch(currentDirection){
				case DIR_IZQUIERDA:
					nouTir = (GameObject) Instantiate(bala, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
					velocity*=-1;
					// nouTir.rigidbody.AddForce(new Vector3(-1000, 0, 0), ForceMode.VelocityChange); Changed to use the velocity parametter
					break;
				case DIR_DERECHA:
					nouTir = (GameObject) Instantiate (bala, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
					// nouTir.rigidbody.AddForce(new Vector3(1000, 0, 0), ForceMode.VelocityChange); // Same as before.
					break;
				default:
					break;
			}
			nouTir.rigidbody.AddForce(new Vector3(velocity, 0, 0), ForceMode.VelocityChange);
			
			GestioTir b = nouTir.GetComponent("GestioTir") as GestioTir;
			b.setEquip(team);
			b.setArma(weapon);
			b.setDamage (primary.getDamage()); // Damage from weapon (primary) to bullet.
			
			if (weapon == WEAPON_ESCOPETA)
				sonidoDisparoEscopeta.Play();
			else
				sonidoDisparoPistola.Play();
			return true;
		}
		return false;
	}
	protected bool doSecondaryAttack() {
		//ataqueSecundario = true;
		if (((ThrowableWeapon)this.secondary).attack()) {
			GameObject novaGranada = null;
			int velocity = ((ThrowableWeapon)this.secondary).getVelocity();
			if (currentDirection == DIR_DERECHA) {
				novaGranada = (GameObject) Instantiate (granada, sortidaBalaDreta.transform.position, sortidaBalaDreta.transform.rotation);
				//novaGranada.rigidbody.AddForce(new Vector3(500, 0, 0), ForceMode.VelocityChange);
			} else {
				novaGranada = (GameObject) Instantiate (granada, sortidaBalaEsquerra.transform.position, sortidaBalaEsquerra.transform.rotation);
				//novaGranada.rigidbody.AddForce(new Vector3(-500, 0, 0), ForceMode.VelocityChange);
				velocity*=-1;
			}
			novaGranada.rigidbody.AddForce(new Vector3(velocity, 0, 0), ForceMode.VelocityChange);
			
			GestioTir b = novaGranada.GetComponent("GestioTir") as GestioTir;
			//b.setEquip(1);
			// @LynosSorien -- Added the normal comportament to granade.
			b.setEquip(this.team);
			b.setDamage (this.secondary.getDamage());
			return true;
		}
		return false;
	}
	/* ==========================================================================
	 * @LynosSorien -- END Attack methods.
	 * ==========================================================================
	 */
	/*
	 * 
	 */ 
	void OnCollisionEnter(Collision collision) {
		// ADD the necessary code here.
	}
	
	public int getShield(){ return shield; }
	public void addShield(int s){ setShield(Mathf.Min(100,shield+s)); }
	
	protected virtual void updateModelWeapon(){}
	public void setWeapon(int weapon){
		// @LynosSorien -- Add the creation of the primary Weapon.
		this.weapon = weapon;
		int ammo;
		switch(weapon) {
			case WEAPON_KATANA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.KATANA);
			break;
			case WEAPON_ESCOPETA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.SHOTGUN);
			ammo = ((DistanceWeapon)this.primary).getCAmmo();
			if(this.GetType () == typeof(PlayerController)) this.hud.notifyAmmo(1,ammo);
			break;
			case WEAPON_PISTOLA:
			this.primary = WeaponFactory.instance().create(WeaponFactory.WeaponType.GUN);
			ammo = ((DistanceWeapon)this.primary).getCAmmo();
			if(this.GetType () == typeof(PlayerController)) this.hud.notifyAmmo(1,ammo);
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
	public void notifyHudPoints(int team,int p) {
		this.hud.notifyPoints(team,p);
		if (team == PlayerPrefs.GetInt ("Team"))notifyScoreChange(this.hud.getPoints());
		else notifyScoreChange (this.hud.getNPCPoints());
	}
	/*
	 * Notify the score change. This will view if the actor have won the game.
	 */ 
	public void notifyScoreChange(int points) {
		gameManager.notifyScoreChange(this.team,points);
	}
}
