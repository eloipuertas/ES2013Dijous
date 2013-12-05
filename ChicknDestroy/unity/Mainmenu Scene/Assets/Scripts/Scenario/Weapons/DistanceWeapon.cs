using System;
using System.Timers;

public class DistanceWeapon : Weapon
{
	protected int ammo;
	protected int current_ammo;
	public DistanceWeapon (string name, int damage,long delay,int range, int ammo)
	{
		init (name,damage,delay,range,ammo);
	}
	
	public DistanceWeapon(){}
	
	protected void init(string name, int damage, long delay, int range, int ammo) {
		this.name = name;
		this.damage = damage;
		this.delay = delay;
		this.range = range;
		this.ammo = ammo;
		this.current_ammo = ammo;
		initTimer ();
	}
	
	public bool attack() {
		if (!this.ready || this.current_ammo<=0) return false;
		this.current_ammo--;
		this.delay_control.Start ();
		return true;
	}
	
	public void reload(int ammo) {
		this.current_ammo += ammo;
		if (this.current_ammo >this.ammo) this.current_ammo = this.ammo;
	}
	
	public int getCAmmo() {
		return current_ammo;
	}
	public int getAmmo() {
		return ammo;
	}
}

