using System;
using System.Timers;

public class ThrowableWeapon : DistanceWeapon
{
	public ThrowableWeapon (string name, int damage,long delay,int range, int ammo, int velocity)
	{
		init (name,damage,delay,range,ammo, velocity);
	}
}

