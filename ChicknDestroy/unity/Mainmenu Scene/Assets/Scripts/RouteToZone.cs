using UnityEngine;
using System.Collections;

public class RouteToZone : MonoBehaviour {
	
	private string [] Zona0,Zona1,Zona2,Zona3,Zona4,Zona5,Zona6,Zona7,Zona8,Zona9,Zona10,Zona11,Zona12,Zona13,Zona14,Zona15,Zona16,Zona17;
	
	// Use this for initialization
	void Start () {
		Zona0 = new string[1];
		Zona0[0] = "Zona0";
		
		Zona1 = new string[3];
		Zona1[0] = "Zona3";
		Zona1[1] = "Zona15";
		Zona1[2] = "Zona4";
		
		Zona8 = new string[1];
		Zona8[0] = "Zona4";
		
		Zona9 = new string[1];
		Zona9[0] = "Zona10";
		
		Zona10 = new string[3];
		Zona10[0] = "Zona9";
		Zona10[1] = "Zona11";
		Zona10[2] = "Zona12";
		
		Zona11 = new string[3];
		Zona11[0] = "Zona4";
		Zona11[1] = "Zona10";
		Zona11[2] = "Zona3";
		
		Zona12 = new string[1];
		Zona12[0] = "Zona13";
		
		Zona13 = new string[4];
		Zona13[0] = "Zona9";
		Zona13[1] = "Zona10";
		Zona13[2] = "Zona12";
		Zona13[3] = "Zona11";
		
		Zona15 = new string[3];
		Zona15[0] = "Zona1";
		Zona15[1] = "Zona17";
		Zona15[2] = "Zona2";
		
		Zona16 = new string[1];
		Zona16[0] = "Zona2";

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
