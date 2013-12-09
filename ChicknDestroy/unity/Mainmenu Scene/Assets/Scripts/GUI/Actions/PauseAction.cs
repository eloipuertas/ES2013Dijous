using UnityEngine;
using System;

public class PauseAction : IActionButton
{
	public PauseAction ()
	{
		
	}
	
	public void action() {
		Time.timeScale = 0;
	}
}

