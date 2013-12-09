using System;
using System.Timers;

public class SpecialWeapon : Weapon
{
	protected Func<Void> listener;
	protected Timer weapon_life;
	protected bool end;
	public SpecialWeapon(){}
	public SpecialWeapon (string name, int damage,long delay,int range,long life)
	{
		init (name,damage,delay,range,life);
	}
	protected void init(string name, int damage,long delay,int range,long life) {
		this.name = name;
		this.damage = damage;
		this.delay = delay;
		this.range = range;
		initTimer ();
		initWeaponLife(life);
		this.end = false;
	}
	protected void initWeaponLife(long life) {
		this.weapon_life = new Timer();
		this.weapon_life.Interval = life;
		this.weapon_life.Elapsed+= new ElapsedEventHandler(lifePast);
	}
	
	public bool attack() {
		if (!this.ready || this.end)return false;
		this.delay_control.Start ();
		return true;
	}
	
	
	public void weaponGetted(Func<Void> listener) {
		this.listener = listener;
		this.weapon_life.Start ();
	}
	
	protected void lifePast(object sender, ElapsedEventArgs e) {
		listener.DynamicInvoke();
		this.end = true;
	}
}

