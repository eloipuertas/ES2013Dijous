using System;

/**
 * This is our basic game object (ChickenDestroyObject).
 */ 
public class CDObject : Object
{
	protected string name;
	public CDObject(){this.name="";}
	public CDObject (string name)
	{
		this.init (name);
	}
	
	public string getName() {
		return name;
	}
	
	protected void init(string name) {
		this.name = name;
	}
}

