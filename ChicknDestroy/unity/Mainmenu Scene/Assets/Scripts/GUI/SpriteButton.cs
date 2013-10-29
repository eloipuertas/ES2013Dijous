using UnityEngine;
using System;


public class SpriteButton : Sprite
{
	private bool toggled;
	public SpriteButton (Rect xy_size, String image)
	{
		this.toggled = false;
	}
	
	public void render() {
		
	}
	
	public bool isToggled() {
		return toggled;
	}
	
	public void setToggled(bool t) {
		this.toggled = t;
	}
}

