using UnityEngine;
using System;

public class PauseMenu : MonoBehaviour
{
	private HUD hud;
	private Sprite menu;
	
	private SpriteButton resume_button;
	private SpriteButton save_button;
	private SpriteButton load_button;
	private SpriteButton quit_game_button;
	
	private Vector2 origin;
	private Vector2 end;
	private bool visible;
	public PauseMenu (HUD hud,Vector2 origin, Vector2 end,Vector2 dev,String menu,String team) {
		this.hud = hud;
		this.origin = origin;
		this.end = end;
		this.visible = false;
		this.menu = new Sprite(new Rect(origin.x,origin.y,end.x-origin.x,end.y-origin.y),menu);
		
		int x=Screen.width/2,y = Screen.height/2;
		
		this.resume_button = new SpriteButton(new Rect(x-50,origin.y+2*dev.y,100,40),
			"pausa/resume"+team,
			"pausa/resume"+team,
			new ResumeAction(),
			KeyCode.R);
		this.save_button = new SpriteButton(new Rect(x-50,this.resume_button.getXY ().y+dev.y,100,40),
			"pausa/save"+team,
			"pausa/save"+team);
		this.load_button = new SpriteButton(new Rect(x-50,this.save_button.getXY ().y+dev.y,100,40),
			"pausa/load"+team,
			"pausa/load"+team);
		this.quit_game_button = new SpriteButton(new Rect(x-50,this.load_button.getXY ().y+dev.y,100,40),
			"pausa/quit"+team,
			"pausa/quit"+team,
			new ChangeLevelAction(),
			KeyCode.Q);
	}
	
	public void reinit_buttons() {
		this.resume_button.setToggled(false);
		this.quit_game_button.setToggled (false);
	}
	
	public void render() {
		if(isVisible ()) {
			if(this.resume_button.isToggled ())resumeToggled();
			this.menu.render();
			// button control-render logic.
			this.resume_button.render();
			this.save_button.render ();
			this.load_button.render ();
			this.quit_game_button.render();
		}
	}
	
	private void resumeToggled() {
		reinit_buttons ();
		this.hud.resume ();
	}
	
	private void quitGameToggled() {
		
	}
	
	public void setVisible(bool visible) {
		this.visible = visible;
		this.resume_button.setVisible (visible);
		this.quit_game_button.setVisible (visible);
	}
	public bool isVisible() {
		return visible;
	}
}


