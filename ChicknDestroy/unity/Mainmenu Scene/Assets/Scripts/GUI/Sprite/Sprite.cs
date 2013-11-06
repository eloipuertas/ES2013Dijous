using UnityEngine;
using System;

public class Sprite : MonoBehaviour{
	protected Texture2D texture;
	protected Rect xy_size;
	
	public Sprite() {}
	public Sprite (Rect xy_size, String image){
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
	}
	
	public void setXY(Vector2 xy) {
		this.xy_size.x = xy.x;
		this.xy_size.y = xy.y;
	}
	public Vector2 getXY() {
		return new Vector2(xy_size.x,xy_size.y);
	}
	public Vector2 getSize() {
		return new Vector2(xy_size.width,xy_size.height);
	}
	
	public void setImage(String img) {
		//this.texture.Resize(xy_size.width,xy_size.height);
		this.texture = (Texture2D)Resources.Load(img);
	}
	
	public void setTexture(Texture2D texture) {
		this.texture = texture;
	}
	
	public void render() {
		GUI.Label(this.xy_size,this.texture);
	}
	
}

