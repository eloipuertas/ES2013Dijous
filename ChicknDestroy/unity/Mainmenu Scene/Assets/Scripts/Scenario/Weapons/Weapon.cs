using System;
using System.Timers;


public class Weapon : CDObject
{
	protected int damage;
	protected long delay;
	protected int range;
	protected Timer delay_control;
	protected bool ready;
	/**
	 * Delay is how many time we must to wait until we can do the next attack (the waiting between attack and attack).
	 */ 
	public Weapon (string name, int damage,long delay, int range)
	{
		init (name,damage,delay,range);
	}
	public Weapon(){}
	protected void init(string name, int damage,long delay,int range) {
		this.name = name;
		this.damage = damage;
		this.delay = delay;
		this.range = range;
		initTimer ();
	}
	
	protected void initTimer() {
		this.delay_control = new Timer();
		this.delay_control.Interval = delay;
		this.delay_control.Elapsed+= new ElapsedEventHandler(delayLapse);
		this.ready = true;
	}
	
	public bool attack() {
		if (!this.ready)return false;
		this.delay_control.Start ();
		return true;
	}
	
	public int getDamage() {
		return damage;
	}
	public long getDelay() {
		return delay;
	}
	
	protected void delayLapse(object sender, ElapsedEventArgs e) {
		this.ready = true;
	}
}

