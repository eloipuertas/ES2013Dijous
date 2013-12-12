using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	private Vector3[,] grid; 

	// Use this for initialization
	void Start () {
		//GameObject.Find("Patios").SetActive(false);//uncoment this line to view the grid better
		float x0,y0;//cell bottom x and bootom y value
		x0 = -1221f;
		grid = new Vector3[18000/200,1400/200];//width:18000|height:1400|grid size:200x200
		for(int i = 0; i<grid.GetLength(0);i++){
			y0 = -110f;
			for(int j = 0; j < grid.GetLength(1);j++){
				grid[i,j] = new Vector3(x0+100f,y0+100f,0f);
				//gridShow(i,j,x0,y0);//uncoment this line to view the grid at the scene
				
				y0 += 200f;
			}
			x0 += 200f;
		}
	
	}
	
	public Vector3[,] getGrid(){
		return grid;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void gridShow(int i,int j,float x0,float y0){
		GameObject cell = Resources.Load("cell") as GameObject;
		cell.name = "cell-x"+i+"y"+j;
		cell.transform.position = new Vector3(x0+100f,y0+100f,80f);
		GameObject celltext = Resources.Load("celltext") as GameObject;
		cell.name = "cell-x"+i+"y"+j;
		TextMesh t = celltext.GetComponent("TextMesh") as TextMesh;
		t.text = "cell-x"+i+"y"+j+"("+getZone(i,j)+")";
		cell.transform.position = new Vector3(x0+100f,y0+100f,80f);
		celltext.transform.position = new Vector3(x0+60f,y0+110f,80f);
		
		
		Light l = cell.GetComponent("Light") as Light;
		l.intensity = 1.38f;
		l.range = 200;
		setColorZone (i,j,l);
		
		Instantiate (cell);
		Instantiate(celltext);

	}
	
	
	private void setColorZone(int x,int y, Light l){//Tranlate X and Y grid position to zone
		switch(getZone(x,y)){
			case "Zona1":
				l.color = new Color(1,0,0);	break;
			case "Zona2":
				l.color = new Color(1f,0.5f,0);	break;
			case "Zona3":
				l.color = new Color(1,1,0);	break;
			case "Zona4":
				l.color = new Color(0.5f,1,0.5f); break;
			case "Zona5":
				l.color = new Color(0,1,0); break;
			case "Zona6":
				l.color = new Color(0.2f,0.5f,0.7f); break;
			case "Zona7":
				l.color = new Color(0.5f,0.5f,0.5f); break;
			case "Zona8":
				l.color = new Color(0,0,1); break;
			case "Zona9":
				l.color = new Color(0,0.5f,1); break;
			case "Zona10":
				l.color = new Color(0,1,1); break;
			case "Zona11":
				l.color = new Color(0.5f,0,1); break;
			case "Zona12":
				l.color = new Color(1,0,1); break;
			case "Zona13":
				l.color = new Color(1,0,0.5f); break;
			case "Zona14":
				l.color = new Color(1,0.5f,0.5f); break;
			case "Zona15":
				l.color = new Color(1,0.5f,1); break;
			case "Zona16":
				l.color = new Color(0,0,0); break;
			case "Zona17":
				l.color = new Color(0.1f,0.2f,0.8f); break;
			default://default is Zona0
				l.color = new Color(1,1,1); break;

		}
	}
	
	
	private string getZone(int x,int y){//Tranlate X and Y grid position to zone
		if((x>=0 && x<=6) && (y>=0 && y<=3))
			return "Zona1";
		if((x>=0 && x<=7) && (y>=4 && y<=6))
			return "Zona2";
		if((x>=7 && x<=80) && (y>=0 && y<=0))
			return "Zona3";
		if((x>=8 && x<=80) && (y>=1 && y<=1))
			return "Zona4";
		if((x>=16 && x<=31) && (y>=2 && y<=2))
			return "Zona5";
		if((x>=17 && x<=36) && (y>=3 && y<=4))
			return "Zona6";
		if((x>=41 && x<=47) && (y>=2 && y<=4))
			return "Zona7";
		if((x>=50 && x<=72) && (y>=2 && y<=2))
			return "Zona8";
		if((x>=74 && x<=77) && (y>=2 && y<=4))
			return "Zona9";
		if((x>=78 && x<=80) && (y>=2 && y<=3))
			return "Zona10";
		if((x>=81 && x<=88) && (y>=0 && y<=3))
			return "Zona11";
		if((x>=82 && x<=88) && (y>=4 && y<=6))
			return "Zona12";
		if((x>=77 && x<=81) && (y>=5 && y<=6))
			return "Zona13";
		if((x>=53 && x<=71) && (y>=3 && y<=5))
			return "Zona14";
		if((x>=7 && x<=9) && (y>=2 && y<=3))
			return "Zona15";
		if((x>=8 && x<=11) && (y>=5 && y<=6))
			return "Zona16";
		if((x>=10 && x<=15) && (y>=2 && y<=3))
			return "Zona17";
		
		return "Zona0";
	}
}
