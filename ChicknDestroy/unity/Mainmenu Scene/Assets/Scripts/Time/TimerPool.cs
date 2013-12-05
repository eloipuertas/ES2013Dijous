using System;
using System.Timers;


public class TimerPool
{
	private Timer[] timers;
	public TimerPool (int amount)
	{
		this.timers = new Timer[amount];
		for(int i = 0;i<amount;i++) this.timers[i] = new Timer();
	}
	public void start(long millis, Func<object,System.Timers.ElapsedEventArgs,Void> method) {
		bool flag=false;
		while (!flag) {
			for (int i = 0;i<this.timers.Length;i++) {
				if(this.timers[i].Enabled == false) {
					this.timers[i] = new Timer();
					this.timers[i].Elapsed += new ElapsedEventHandler(method);
					this.timers[i].Interval = millis;
					this.timers[i].Start();
					this.timers[i].AutoReset = false;
					flag = true;
					break;
				}
			}
		}
	}
	
	public void start(long millis, Func<object,System.Timers.ElapsedEventArgs,Void> method, bool autoreset) {
		bool flag=false;
		while (!flag) {
			for (int i = 0;i<this.timers.Length;i++) {
				if(this.timers[i].Enabled == false) {
					this.timers[i] = new Timer();
					this.timers[i].Elapsed += new ElapsedEventHandler(method);
					this.timers[i].Interval = millis;
					this.timers[i].Start();
					this.timers[i].AutoReset = autoreset;
					flag = true;
					break;
				}
			}
		}
	}
}

