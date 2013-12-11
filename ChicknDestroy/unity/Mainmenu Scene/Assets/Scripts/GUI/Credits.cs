using System;
using UnityEngine;
using System.Timers;
using System.Collections;


public class Credits : MonoBehaviour
{
	private const long INTERVAL = 100;
	private const int x = 1920/3;
	private const int y = 12000/3;
	private const int DY = 5;
	private Timer timer;
	private int count;
	//private int N = (int)((y+Screen.height)/DY);
	private int N = 400;
	private int nrosters = 10;
	private int change_interval;
	private bool end = false;
	
	private ChangeLevelAction action;
	
	private Sprite credits;
	private Sprite rosters;
	
	private UnityEngine.Object[] objects;
	
	private bool change;
	
	private const int MULTIPLE = 20;
	public Credits (){
	}
	
	void Start() {
		this.count = 0;
		this.credits = new Sprite(new Rect((Screen.width/2)-(x/2),Screen.height,x,y),"creditos");
		this.rosters = new Sprite(new Rect(this.credits.getXY ().x+300,Screen.height/3,200,200),"");
		
		this.timer = new Timer();
		this.timer.Elapsed+=new ElapsedEventHandler(onLapseEvent);
		this.timer.Interval = INTERVAL;
		this.timer.AutoReset = true;
		this.timer.Start();
		
		change_interval = (int)(N/nrosters);
		
		this.action = new ChangeLevelAction();
		
		this.objects = Resources.LoadAll("Credits/");
		
		this.change = false;
		
		Debug.Log (N);
	}
	
	private string getNextRoster() {
		//return "";
		return this.objects[UnityEngine.Random.Range(0,this.objects.Length)].name;
	}
	
	void OnGUI() {
		this.credits.render ();
		if (this.count%MULTIPLE == 0 && !this.change) {
			string m = getNextRoster ();
			Debug.Log (m);
			this.rosters.setImage ("Credits/"+m);
			this.change = true;
		}
		this.rosters.render();
		if(this.end) this.action.action ();
	}
	
	private void onLapseEvent(object source, ElapsedEventArgs e) {
		this.count++;
		this.change = false;
		if (this.count>=N) {
			// END
			this.timer.Stop ();
			this.timer.AutoReset = false;
			this.end = true;
		}
		this.credits.setXY (new Vector2(this.credits.getXY ().x,
			this.credits.getXY().y-DY));
	}
}

