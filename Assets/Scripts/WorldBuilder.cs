using UnityEngine;
using System.Collections;

public enum Blocks{
	MoonRocks,
	Spice,
	Unobtainium,
	OxyAle,
	VesperGas,
	TheFifthElement
}

public class WorldBuilder : MonoBehaviour {
	public Transform baseTile;

	public int radius = 5;

	public float tileSize;
	
	private bool[][] worldGrid;

	public float ElementProbability = .4f;
	//public int[] blockTypeProbability = {34, 57, 74, 88, .11f};

	public Color[] blockColor;

	// Use this for initialization
	void Start () {
		tileSize = baseTile.renderer.bounds.size.x;

		//Makin squares
		worldGrid = new bool[(radius *2) + 1][];
		
		for (int x = 0; x < (radius * 2) + 1; x++){
			worldGrid[x] = new bool[(radius * 2) + 1];
			for (int y = 0; y < (radius * 2) + 1; y++){
				worldGrid[x][y] = false;
			}
		}

		BuildWorld();
	}
	
	void BuildWorld(){
		rasterCircle(0,0,radius);

		for (int y = 0; y < radius * 2; y++){
			bool firstBlock = false;
			for (int x = 0; x < radius * 2; x++){
				if (!firstBlock && worldGrid[x][y]){
					firstBlock = true;
				}

				if (x - radius >= 0){
					if(!worldGrid[x][y]){
						SpawnBlock(x-radius, y-radius);
					}
					break;
				}
				else if (firstBlock && !worldGrid[x][y]){
					SpawnBlock(x-radius, y-radius);
					SpawnBlock(radius - x, radius - y);
				}
			}
		}
	}

     void rasterCircle(int x0, int y0, int radius)
     {
		int f = 1 - radius;
		int ddF_x = 1;
		int ddF_y = -2 * radius;
		int x = 0;
		int y = radius;
		
		SpawnBlock(x0, y0 + radius, true);
		SpawnBlock(x0, y0 - radius, true);
		SpawnBlock(x0 + radius, y0, true);
		SpawnBlock(x0 - radius, y0, true);
		
		while(x < y)
		{
			// ddF_x == 2 * x + 1;
			// ddF_y == -2 * y;
			// f == x*x + y*y - radius*radius + 2*x - y + 1;
			if(f >= 0) 
			{
				y--;
				ddF_y += 2;
				f += ddF_y;
			}
			x++;
			ddF_x += 2;
			f += ddF_x;    
			SpawnBlock(x0 + x, y0 + y, true);
			SpawnBlock(x0 - x, y0 + y, true);
			SpawnBlock(x0 + x, y0 - y, true);
			SpawnBlock(x0 - x, y0 - y, true);
			SpawnBlock(x0 + y, y0 + x, true);
			SpawnBlock(x0 - y, y0 + x, true);
			SpawnBlock(x0 + y, y0 - x, true);
			SpawnBlock(x0 - y, y0 - x, true);
		}
	}

	void SpawnBlock(int x, int y, bool activateBox = false){
		Vector2 pos2d = new Vector2(x*tileSize, y*tileSize);

		Transform tile = (Transform)Instantiate(baseTile, new Vector3(pos2d.x, pos2d.y, 0), Quaternion.identity);
		tile.parent = this.transform;

		worldGrid[x + radius][y + radius] = true;

		if (activateBox){
			tile.transform.GetChild(0).gameObject.SetActive(true);
		}

		if (Random.Range(0,100) < ElementProbability){
			int d100 = Random.Range(1,100);
			
			if (d100 < 35){
				tile.GetComponent<Block>().type = Blocks.Spice;
			} else if (d100 <58){
				tile.GetComponent<Block>().type = Blocks.Unobtainium;
			} else if (d100 < 75){
				tile.GetComponent<Block>().type = Blocks.OxyAle;
			} else if (d100 < 89){
				tile.GetComponent<Block>().type = Blocks.VesperGas;
			} else {
				tile.GetComponent<Block>().type = Blocks.TheFifthElement;
			}
		}
	}
}
