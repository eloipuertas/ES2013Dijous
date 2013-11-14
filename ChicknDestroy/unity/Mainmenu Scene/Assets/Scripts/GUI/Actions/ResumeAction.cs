using UnityEngine;
using System;

public class ResumeAction : IActionButton
{
	public ResumeAction ()
	{
	}
	
	public void action() {
		Time.timeScale = 1;
	}
}

