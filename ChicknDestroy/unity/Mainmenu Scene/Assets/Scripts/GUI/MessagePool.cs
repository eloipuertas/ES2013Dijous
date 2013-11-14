using UnityEngine;
using System;


public class MessagePool
{
	private Message[] messages;
	private int n;
	public MessagePool (int npool)
	{
		this.n = npool;
		this.messages = new Message[npool];
		for(int i = 0;i<npool;i++)
			this.messages[i] = new Message();
	}
	
	public void start(Vector2 pos, String message, double millis) {
		for(int i = 0;i<n;i++) {
			if (!this.messages[i].isWorking()) {
				this.messages[i].start(pos,message,millis);
				break;
			}
		}
	}
	
	public void render() {
		for(int i = 0; i<n;i++)
			this.messages[i].render();
	}
}