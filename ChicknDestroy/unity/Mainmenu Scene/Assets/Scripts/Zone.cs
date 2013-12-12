using UnityEngine;
using System.Collections.Generic;

public class Zone{	

	public int idZone;
	public List<Vector3> zone;
	public List<Vector3> extrems;
	public List<Object> zonesColindants;
	public Vector2 minPoint;
	public Vector2 maxPoint;
	public int averageValue;
	
	public Zone(int idZone){
		this.idZone = idZone;
		zone = new List<Vector3>();
		extrems = new List<Vector3>();
		zonesColindants = new List<Object>();
		averageValue = 0;
		minPoint = new Vector2(99999, 99999);
		maxPoint = new Vector2(-99999, -99999);
	}
	
	public void addCellZone(Vector3 cell) {
		zone.Add(cell);
		
		if(cell.x < minPoint.x && cell.y < minPoint.y) {
			minPoint[0] = cell.x;
			minPoint[1] = cell.y;
		}
		
		if(cell.x > maxPoint.x && cell.y > maxPoint.y) {
			maxPoint[0] = cell.x;
			maxPoint[1] = cell.y;
		}
	}
	
	public bool isInsideZone(Vector3 tpos) {
		return minPoint.x <= tpos.x && tpos.x <= maxPoint.x &&
			minPoint.y <= tpos.y && tpos.y <= maxPoint.y;
	}
	
	public void addCellTerminal(Vector3 cell) {
		extrems.Add(cell);
	}
	
	public void addZone(Object aZone) {
		zonesColindants.Add(aZone);
	}
	
	public void updateAverage(){
		averageValue = 0;
		int nValues = 0;
		foreach(Vector3 vec in zone){
			averageValue += (int)vec.z;
			nValues++;
		}
		averageValue = averageValue/nValues;
	}
	
	public string toString() {
		string res = "";
		res += "Zone: "+idZone + " Average: "+averageValue+" Min: "+minPoint.x+"|"+minPoint.y+" Max: "+maxPoint.x+"|"+maxPoint.y;
		return res;
	}
}
