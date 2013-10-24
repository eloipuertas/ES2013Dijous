using UnityEngine;
using System;

public class GUIObject : MonoBehaviour
{
	private Sprite sprite;
	
	public GUIObject(int x,int y, int width, int height, String image){
		this.sprite = new Sprite(new Rect(x,y,width,height),image);
	}
	
	public void Start() {
		
	}
	
	public void render() {
		this.sprite.render();
	}
}

