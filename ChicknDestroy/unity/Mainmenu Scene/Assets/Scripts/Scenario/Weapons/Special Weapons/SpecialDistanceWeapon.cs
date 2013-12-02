using System;
using System.Timers;


public class SpecialDistanceWeapon : SpecialWeapon
{
	protected int ammo;
	protected int current_ammo;
	public SpecialDistanceWeapon (string name, int damage,long delay,int range,long life, int ammo)
	{
		init(name,damage,delay,range,life,ammo);
	}
	
	protected void init(string name, int damage,long delay,int range,long life,int ammo) {
		this.name = name;
		this.damage = damage;
		this.delay = delay;
		this.range = range;
		this.ammo = ammo;
		this.current_ammo = current_ammo;
		initTimer ();
		initWeaponLife(life);
		this.end = false;
	}
	
	public bool attack() {
		if (!this.ready || this.current_ammo<=0 || this.end) return false;
		this.current_ammo--;
		this.delay_control.Start ();
		return true;
	}
	
	public void reload(int ammo) {
		this.current_ammo += ammo;
		if (this.current_ammo >this.ammo) this.current_ammo = this.ammo;
	}
}

