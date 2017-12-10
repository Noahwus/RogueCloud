using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerationMap : MonoBehaviour {

	public GameObject Wall;
	public GameObject FPSplayer;
	Vector3 temp;

	public int[,,] map;			//Map array
	private int mapRow = 50;	//Rows
	private int mapCol = 25;	//Collums
	private int mapHeight = 6;	//How many stacks of walls are possible
	private int mapCull;		//how many times to repeat the steps of corridor gen

	//private int xx;
	//private int zz;


	void Start(){
		loadResources ();
		initializeMap ();
		populateMap ();
	}

	private void loadResources(){
		//Wall = Resources.Load ("Wall") as GameObject; //This didn't work

		//initialize the 3d array
		map = new int[mapRow, mapHeight, mapCol];
		//Corridor gen will repeat this many times
		mapCull = (mapRow * mapCol) / 2;
	}

	private void initializeMap(){
		// Lay down initial ground plan (all Walls on height 1)
		//Debug.Log(map.Length);
		for(int i = 0; i < mapRow; i++){
			for(int j = 0; j < mapCol; j++){
				map [i,1,j] = 1;
			}
		}

		//Remove paths from initial ground plan (Maze like iteration)
		//Randomize seed();
		int xx = mapRow;
		int zz = mapCol;
		xx = xx / 2;
		zz = zz / 2;

		for (int i = 0; i < mapCull; i++) {

			if (xx > mapRow + 2) {xx = mapRow / 2; }
			if (zz > mapCol + 2) {zz = mapCol / 2; }
			if (xx < 2) {xx = mapRow / 2; }
			if (zz < 2) {zz = mapCol / 2; }

			int m = Random.Range (0, 8);

			if (m == 0) { 
				xx--;
				cullingClamp (xx, zz);
				xx--;
				cullingClamp (xx, zz);
			} else if (m == 1) { 
				xx++;
				cullingClamp (xx, zz);
			} else if (m == 2) { 
				zz++;
				cullingClamp (xx, zz);
			} else if (m == 3) { 
				zz--;
				cullingClamp (xx, zz);
			} else if (m == 4) { 
				xx--;
				cullingClamp (xx, zz);
			} else if (m == 5) { 
				xx++;
				cullingClamp (xx, zz);
				xx++;
				cullingClamp (xx, zz);
			} else if (m == 6) { 
				zz++;
				cullingClamp (xx, zz);
				zz++;
				cullingClamp (xx, zz);
			} else if (m == 7) {
				zz--;
				cullingClamp (xx, zz);
				zz--;
				cullingClamp (xx, zz);
			} else if (m == 8) {
				xx--;
				cullingClamp (xx, zz);
				xx--;
				cullingClamp (xx, zz);
			} else {
				Debug.Log ("not full numb"); 

			}
			Debug.Log ("xx = " + xx + ", zz = " + zz);
		}

		temp = transform.localPosition;
		temp.x = xx;
		temp.y = 15;
		temp.z = zz;
		FPSplayer.transform.localPosition = temp;


		/*/Smooth the Corridors
		for (int k = 1; k < mapHeight - 2; k++) {
			//Debug.Log (k);
			for (int i = 1; i < mapRow - 1; i++) {
				for (int j = 1; j < mapCol-1; j++) {
					if (map [i + 1, k, j] == 0 ||
						map [i - 1, k, j] == 0 ||
						map [i, k, j + 1] == 0 ||
						map [i, k, j - 1] == 0) {

						if (Random.Range (0, 10) >= 6+k) {
							map [i, k, j] = 0;
						}
					}
				}
			}
		}



		/*

		//Stack up inside Walls ( + height )
		for (int k = 1; k < mapHeight-1; k++) {
			for(int i = 0; i < mapRow; i++){
				for(int j = 0; j < mapCol; j++){
					if (map [i, k, j] == 1) {
						if (Random.Range (0, 60) < 60-(k*4)){
							map [i, k + 1, j] = 1;
						}
					}
				}
			}

		}


		//Smooth the map
		for (int k = 1; k < mapHeight - 1; k++) {
			for (int i = 1; i < mapRow-1; i++) {
				for (int j = 1; j < mapCol-1; j++) {
					//Mathf.Clamp (i, 1, mapRow);
					//Mathf.Clamp (j, 1, mapCol);
					if (map [i + 1, k, j] == 1 &&
					   map [i - 1, k, j] == 1 &&
					   map [i, k, j + 1] == 1 &&
					   map [i, k, j - 1] == 1) {
						//if (Random.Range (0, 10) <= 4+k) {
							map [i, k, j] = 1;
						//}
					}
				}
			}
		}
		//*/

	}

	private void populateMap (){
		//Border Walls
		for (int k = 1; k < mapHeight; k++) {
			for (int i = 0; i < mapRow; i++) {
				for (int j = 0; j < mapCol; j++) {
					if (i == 0 || i == mapRow-1) {
						map [i, k, j] = 1;
					}
					if (j == 0 || j == mapCol-1) {
						map [i, k, j] = 1;

					}
				}
			}
		}

		// Populate the world
		for(int i = 0; i < mapRow; i++){
			for (int j = 0; j < mapCol; j++) {
				for (int k = 1; k < mapHeight; k++) {
					//Create instances of 'Wall'
					if (map [i, k, j] == 1) {
						//I,K,J are *2 in Instantiate to account for x and z scale of walls
						Instantiate (Wall, new Vector3 (i * 2, k *2 , j * 2), Quaternion.identity);
						}
					//
					}
				}
			}
		}

	public void cullingClamp(int xx, int zz){
		xx = Mathf.Clamp (xx, 2, mapRow-3);
		zz = Mathf.Clamp (zz, 2, mapCol-3);
		map [xx, 1, zz] = 0;

	}

	public int rowReturn(){
		return mapRow;
	}

	public int colReturn(){
		return mapCol;
	}

}
