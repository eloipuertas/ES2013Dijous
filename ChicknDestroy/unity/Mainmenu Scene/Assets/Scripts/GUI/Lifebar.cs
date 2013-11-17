using UnityEngine;
using System;

public class Lifebar : SpriteGroup{
	private int max_life {get; set;}
	private int current_life;
	private int irender;
	public Lifebar (Vector2 xy, Vector2 size, String img_pattern, Vector2 dev, int max_elements,
		int max_life, int clife)
	{
		this.xy_size = new Rect(xy.x,xy.y,size.x,size.y);
		this.sprites = new Sprite[max_elements];
		for (int i = 0; i<max_elements; i++) {
			this.sprites[i] = new Sprite(new Rect(xy.x+(dev.x*i),xy.y+(dev.y*i),size.x,size.y),
				img_pattern+i);
		}
		this.xy = xy;
		this.dev = dev;
		this.n = max_elements;
		this.max_life = max_life;
		this.current_life = clife;
		this.irender = calculateIRender();
	}
	
	private int calculateIRender() {
		return (int)((this.current_life*this.n)/this.max_life);
	}
	
	public void setLife(int life) {
		this.current_life = life;
		this.irender = calculateIRender();
		if (this.irender>=this.n)this.irender = this.n-1;
	}
	public void render() {
		if(this.current_life>100)this.sprites[(this.n-1)-this.irender].render();
		else this.sprites[(this.n-1)-(int)((this.current_life)*(this.n-1)/100)].render ();
		//Debug.Log ((this.n-1)-(int)((this.current_life)*this.n/this.max_life));
	}
}

