using UnityEngine;
using System;

public class Sprite : MonoBehaviour{
	protected Texture2D texture;
	protected Rect xy_size;
	
	public Sprite (Rect xy_size, String image){
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
	}
	
	private void setXY(Vector2 xy) {
		this.xy_size.x = xy.x;
		this.xy_size.y = xy.y;
	}
	
	private void setImage(String img) {
		//this.texture.Resize(xy_size.width,xy_size.height);
		this.texture = (Texture2D)Resources.Load(img);
	}
	
	public void render() {
		GUI.Label(this.xy_size,this.texture);
	}
	
}

