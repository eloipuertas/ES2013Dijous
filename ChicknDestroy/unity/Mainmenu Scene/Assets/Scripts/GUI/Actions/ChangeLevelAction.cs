using UnityEngine;
using System;

public class ChangeLevelAction : IActionButton
{
	private int level;
	public ChangeLevelAction ()
	{
		this.level = 0;
	}
	public ChangeLevelAction(int level) {
		this.level = level;
	}
	
	public void action() {
		Application.LoadLevel(this.level);
	}
}

