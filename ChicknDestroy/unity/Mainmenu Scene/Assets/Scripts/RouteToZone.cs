using UnityEngine;
using System.Collections;

public class RouteToZone : MonoBehaviour {
	
	private string [] Zona0,Zona1,Zona2,Zona3,Zona4,Zona5,Zona6,Zona7,Zona8,Zona9,Zona10,Zona11,Zona12,Zona13,Zona14,Zona15,Zona16,Zona17;
	
	// Use this for initialization
	void Start () {
		Zona0 = new string[1];
		Zona0[0] = "Zona0";
		
		Zona1 = new string[3];
		Zona0[0] = "Zona3";
		Zona0[1] = "Zona15";
		Zona0[2] = "Zona4";
		
		
	
	}
	
	public string [] thisZoneToThisZones(string zone){
		switch(zone){
			case "Zona1":
				return Zona1; break;
			case "Zona2":
				return Zona2; break;
			case "Zona3":
				return Zona3; break;
			case "Zona4":
				return Zona4; break;
			case "Zona5":
				return Zona5; break;
			case "Zona6":
				return Zona6; break;
			case "Zona7":
				return Zona7; break;
			case "Zona8":
				return Zona8; break;
			case "Zona9":
				return Zona9; break;
			case "Zona10":
				return Zona10; break;
			case "Zona11":
				return Zona11; break;
			case "Zona12":
				return Zona12; break;
			case "Zona13":
				return Zona13; break;
			case "Zona14":
				return Zona14; break;
			case "Zona15":
				return Zona15; break;
			case "Zona16":
				return Zona16; break;
			case "Zona17":
				return Zona17; break;
			default://default is Zona0
				return Zona0; break;

		}
		
	}
}
