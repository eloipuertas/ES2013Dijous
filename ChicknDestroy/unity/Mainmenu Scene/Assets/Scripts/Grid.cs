using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	
	private Vector3[,] grid; 

	// Use this for initialization
	void Start () {
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
		t.text = "cell-x"+i+"y"+j;
		cell.transform.position = new Vector3(x0+100f,y0+100f,80f);
		celltext.transform.position = new Vector3(x0+80f,y0+110f,80f);
		Instantiate (cell);
		Instantiate(celltext);
	}
}
