using UnityEngine;
using System;
using System.Timers;

public class TimeSprite : Sprite
{
	private Timer timer;
	private bool working;
	public TimeSprite (Rect xy_size, String image)
	{
		this.working = false;
		this.setVisible(false);
		this.texture = new Texture2D((int)xy_size.width,(int)xy_size.height);
		this.xy_size = xy_size;
		this.texture = (Texture2D)Resources.Load(image);
	}
	
	public void start(bool activated) {
		if (!this.working)this.setVisible (activated);
	}
	
	public void start(long millis) {
		this.working = true;
		this.timer = new Timer();
		this.timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
		this.timer.Interval = millis;
		this.setVisible (true);
		this.timer.Start();
	}
	
	public void onTimedEvent(object source, ElapsedEventArgs e) {
		this.setVisible (false);
		this.working = false;
	}
}

