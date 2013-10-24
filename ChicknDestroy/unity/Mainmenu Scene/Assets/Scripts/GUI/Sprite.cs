using UnityEngine;
using System;

public class Sprite : MonoBehaviour{
	private Texture2D texture;
	private Renderer renderer;
	private String image;
	private Rect xy_size;
	
	public Sprite (Rect xy_size, String image){
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		//UnityEngine.Object tdata = Resources.Load("Texture/titulo");
		//texture.LoadImage(tdata);
		this.image = image;
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
	}
	
	public void render() {
		//Debug.Log("Rendering Sprite");
		//this.obj.renderer.material.SetTexture("_MainTex",this.texture);
		//Debug.Log (Resources.Load ("Texture/titulo.png").ToString ());
		//obj.renderer.material.mainTexture = (UnityEngine.Texture)(Resources.Load ("titulo.png"));
		GUI.Label(this.xy_size,this.texture);
	}
	
}

