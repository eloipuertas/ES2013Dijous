using UnityEngine;
using System;

public class Points : MonoBehaviour
{
	private static int POINTS = 4;
	private Texture2D[] textures;
	private Sprite[] sprites;
	private Sprite bar;
	
	private int n;
	public Points (Vector2 xy, Vector2 size, String img_pattern, Vector2 dev, int max_elements)
	{
		this.sprites = new Sprite[POINTS];
		for (int i = 0;i<POINTS;i++)
			this.sprites[i] = new Sprite(new Rect(xy.x+(dev.x*i),xy.y+(dev.y*i),size.x,size.y),"");
		this.bar = new Sprite(new Rect(xy.x-5,xy.y-5,size.x*4+10,size.y+10),img_pattern+"hudPuntos");
		this.textures = new Texture2D[max_elements];
		for (int i = 0; i<max_elements; i++)
			this.textures[i] = (Texture2D)Resources.Load(img_pattern+i);
		this.n = max_elements;
	}
	private char[] normalize(int points) {
		char[] c = new char[POINTS];
		for(int i = 0;i<4;i++) c[i] = '0';
		int counter = 0;
		int aux = points;
		int aux2 = 0;
		do {
			aux2 = aux%10;
			aux = aux/10;
			c[3-counter] = aux2.ToString().ToCharArray()[0];
			counter++;
		}while(aux!=0);
		return c;
	}
	public void render(int points) {
		char[] normalized = normalize(points);
		this.bar.render ();
		for(int i = 0;i<POINTS;i++) {
			this.sprites[i].setTexture(this.textures[int.Parse(normalized[i].ToString())]);
			this.sprites[i].render();
		}
	}
}

