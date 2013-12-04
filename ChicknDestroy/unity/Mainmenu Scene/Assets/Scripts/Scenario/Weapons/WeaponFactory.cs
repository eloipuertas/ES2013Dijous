using System;


public class WeaponFactory
{
	private static int VALUE_PER_UNITY = 1;
	private static WeaponFactory factory;
	public enum WeaponType {KATANA,GUN,SHOTGUN,ASSAULTRIFLE,GRANADE,MINE,UBCARPET,SNIPERRIFLE};
	
	private WeaponFactory (){}
	
	public static WeaponFactory instance() {
		if (factory == null) factory = new WeaponFactory();
		return factory;
	}
	/*
	 * Only use when the game will end.
	 */
	public static void delete_instance() {
		factory = null;
	}
	
	
	public Weapon create(WeaponType type) {
		switch(type) {
		case WeaponType.KATANA:
			return createKatana ();
		case WeaponType.GUN:
			return createGun ();
		case WeaponType.SHOTGUN:
			return createShotGun();
		case WeaponType.ASSAULTRIFLE:
			return createAssaultRifle();
		case WeaponType.GRANADE:
			return createGranade();
		case WeaponType.MINE:
			return createMine ();
		case WeaponType.UBCARPET:
			return createUBCarpet();
		case WeaponType.SNIPERRIFLE:
			return createSniperRifle();
		}
		return null;
	}
	/* =========================================================================================
	 * NORMAL WEAPONS
	 * =========================================================================================
	 */ 
	private MeleeWeapon createKatana() {
		return new MeleeWeapon("Katana",25,500,1*VALUE_PER_UNITY);
	}
	
	private DistanceWeapon createGun() {
		return new DistanceWeapon("Gun",15,750,10*VALUE_PER_UNITY,20);
	}
	
	private DistanceWeapon createShotGun() {
		return new DistanceWeapon("ShotGun",40,2000,4*VALUE_PER_UNITY,10);
	}
	
	private DistanceWeapon createAssaultRifle() {
		return new DistanceWeapon("AssaultRifle",10,200,10*VALUE_PER_UNITY,30);
	}
	/* =========================================================================================
	 * THROWABLE-TYPE WEAPONS
	 * =========================================================================================
	 */ 
	private ThrowableWeapon createGranade() {
		return new DistanceWeapon("Granade",60,2000,8*VALUE_PER_UNITY,3);
	}
	private ThrowableWeapon createMine() {
		return new DistanceWeapon("Mine",60,2000,0,3);
	}
	/* =========================================================================================
	 * SPECIAL-TYPE WEAPONS
	 * =========================================================================================
	 */ 
	private SpecialMeleeWeapon createUBCarpet() {
		return new SpecialMeleeWeapon("UBCarpet",35,100,1*VALUE_PER_UNITY,10*1000);
	}
	
	private SpecialDistanceWeapon createSniperRifle() {
		return new SpecialDistanceWeapon("SniperRifle",100,5*5000,30*VALUE_PER_UNITY,30*1000,5);
	}
}

