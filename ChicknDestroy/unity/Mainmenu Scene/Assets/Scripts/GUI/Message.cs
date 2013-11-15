using UnityEngine;
using System;
using System.Timers;


public class Message
{
	private Vector2 pos;
	private String message;
	private Boolean working;
	private Timer timer;
	public Message ()
	{
		this.working = false;
		this.timer = new Timer();
		this.timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
	}
	
	public Message (Vector2 pos, String message)
	{
		this.working = false;
		this.pos = pos;
		this.message = message;
		this.timer = new Timer();
		this.timer.Elapsed += new ElapsedEventHandler(onTimedEvent);
	}
	
	public void render() {
		if (isWorking()) GUI.Label(new Rect(pos.x,pos.y,100,10),this.message);
	}
	
	public void start(Vector2 pos, String message, double millis) {
		this.pos = pos;
		this.message = message;
		setWorking (true);
		
		this.timer.Interval = millis;
		this.timer.Start();
	}
	
	public void start(double millis) {
		setWorking (true);
		
		this.timer.Interval = millis;
		this.timer.Start();
	}
	
	public Boolean isWorking() {
		return working;
	}
	
	public void setWorking(bool w) {
		this.working = w;
		if(!isWorking ()) this.timer.Enabled = false;
	}
	
	public void printMessage() {
		Debug.Log(this.message);
	}
	
	private void onTimedEvent(object source, ElapsedEventArgs e) {
		setWorking(false);
	}
}

