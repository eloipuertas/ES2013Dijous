using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour
{
	private Sprite menu;
	private Sprite title;
	
	private SpriteButton quit_menu_button;
	private SpriteButton quit_game_button;
	
	private Vector2 origin;
	private Vector2 end;
	private bool visible;
	public PauseMenu (Vector2 origin, Vector2 end,String menu, String title, String b_pattern) {
		this.origin = origin;
		this.end = end;
		this.visible = false;
		this.menu = new Sprite(new Rect(origin.x,origin.y,end.x-origin.x,end.y-origin.y),menu);
		this.title = new Sprite(new Rect((end.x-origin.x)/2,origin.y+10,(end.x-origin.x)/3,20),title);
	}
	
	public void render() {
		this.menu.render();
		this.title.render();
		
		// button control-render logic.
	}
	
	private void quitMenuToggled() {
		
	}
	
	private void quitGameToggled() {
		
	}
	
	public void setVisible(bool visible) {
		this.visible = visible;
	}
	public bool isVisible() {
		return visible;
	}
}


