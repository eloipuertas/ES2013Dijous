using UnityEngine;
using System;

public class SpriteGroup : Sprite
{
	protected Sprite[] sprites;
	protected int n{get; set;}
	protected Vector2 dev {get; set;}
	protected Vector2 xy {get; set;}
	
	public SpriteGroup(){}
	public SpriteGroup (Vector2 xy, Vector2 size, String img_pattern, Vector2 dev, int max_elements) {
		this.sprites = new Sprite[max_elements];
		for (int i = 0; i<max_elements; i++)
			this.sprites[i] = new Sprite(new Rect(xy.x+(dev.x*i),xy.y+(dev.y*i),size.x,size.y),
				img_pattern+i);
		this.n = max_elements;
		this.dev = dev;
		this.xy = xy;
	}
	
	public void render() {
		for (int i = 0; i<this.n;i++)
			this.sprites[i].render();
	}
}

