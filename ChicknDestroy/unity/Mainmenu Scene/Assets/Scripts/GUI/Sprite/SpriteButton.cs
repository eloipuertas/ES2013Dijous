using UnityEngine;
using System;


public class SpriteButton : Sprite
{
	private bool toggled;
	private IActionButton action;
	private Texture2D toggled_texture;
	public SpriteButton (Rect xy_size, String image, String image2)
	{
		this.toggled = false;
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
		this.toggled_texture = (Texture2D)Resources.Load (image);
		this.action = null;
	}
	
	public SpriteButton (Rect xy_size, String image,String image2,IActionButton action)
	{
		this.toggled = false;
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
		this.toggled_texture = (Texture2D)Resources.Load (image);
		this.action = action;
	}
	
	public void render() {
		GUI.Label(this.xy_size,this.texture);
		if(action == null){
			if(GUI.Button(this.xy_size,"")) {
				this.toggled = true;
				this.setTexture(this.toggled_texture);
			}
		}else {
			if(GUI.Button(this.xy_size,"")) {
				this.toggled = true;
				this.setTexture(this.toggled_texture);
				this.action.action();
			}
		}
	}
	
	public bool isToggled() {
		return toggled;
	}
	
	public void setToggled(bool t) {
		this.toggled = t;
		if(this.toggled == false) this.setTexture (this.texture);
		else this.setTexture (this.toggled_texture);
	}
}

