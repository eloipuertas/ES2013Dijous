using UnityEngine;
using System;

public class GUIObject : MonoBehaviour
{
	private Sprite sprite;
	private SpriteGroup sgroup;
	
	private bool mono;
	
	public GUIObject(int x,int y, int width, int height, String image){
		this.sprite = new Sprite(new Rect(x,y,width,height),image);
		this.mono = true;
	}
	/**
	 *  
	 */
	public GUIObject(int iX, int iY, int in_width, int in_height, 
		String img_pattern, int x_dev, int y_dev, int max_elements) {
		this.sgroup = new SpriteGroup(new Vector2(iX,iY), new Vector2(in_width,in_height),
			img_pattern,new Vector2(x_dev,y_dev),max_elements);
		this.mono = false;
	}
	
	public void Start() {
		
	}
	
	public void render() {
		if (this.mono) this.sprite.render();
		else this.sgroup.render();
	}
}

