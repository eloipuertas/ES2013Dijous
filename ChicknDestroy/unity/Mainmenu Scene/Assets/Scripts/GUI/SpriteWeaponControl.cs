using UnityEngine;
using System;


public class SpriteWeaponControl : Sprite
{
	private Sprite primary_weapon;
	private Sprite bg_primary;
	private Sprite secondary_weapon;
	private Sprite bg_secondary;
	
	public SpriteWeaponControl (Rect xy_size)
	{
		this.xy_size = xy_size;
		this.primary_weapon = new Sprite(xy_size,"primarios/katana");
		this.bg_primary = new Sprite(xy_size,"primarios/hudArmas");
		this.secondary_weapon = new Sprite(new Rect(xy_size.x+xy_size.width,xy_size.y,40,40),"secundarios/granada");
		this.bg_secondary = new Sprite(new Rect(xy_size.x+xy_size.width,xy_size.y,40,40),"secundarios/hudExplosivos");
	}
	
	public void render() {
		this.bg_primary.render ();
		this.bg_secondary.render ();
		this.primary_weapon.render ();
		this.secondary_weapon.render ();
	}
	
	public void notifyPrimaryWeapon(String weapon){
		this.primary_weapon.setImage("primarios/"+weapon);
	}
	
	public void notifySecondaryWeapon(string weapon) {
		this.secondary_weapon.setImage ("secundarios/"+weapon);
	}
}

