using UnityEngine;
using System.Collections;

public class PlayerInterface : MonoBehaviour {
	//private Player player;
	private int h;
	private int p;
	public PlayerInterface() {
		// Debug only
		this.h = 0;
		this.p = 0;
	}
	
	public string getPrimaryWeapon() {
		return "UB_Carpet";
	}
	
	public string getSecondaryWeapon() {
		return "Granade";
	}
	
	public void setHealthPoints(int health) {
		this.h = health;
	}
	
	public int getHealthPoints() {
		return h;
	}
	
	public void setPlayerPoints(int points) {
		this.p = points;
	}
	
	public int getPlayerPoints() {
		return p;
	}
	
	/*public PlayerInterface (Player player) {
		this.player = player;
	}
	
	
	
	public void setPlayer(Player player) {
		this.player = player;
	}*/
}

