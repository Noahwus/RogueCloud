/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerationMap(2) : MonoBehaviour {

	public GameObject Wall;
	public GameObject notWall;
	public GameObject FPSplayer;
	Vector3 temp;

	public int[,,] map;			//Map array
	private int mapRow = 35;	//Rows
	private int mapCol = 35;	//Collums
	private int mapHeight = 6;	//How many stacks of walls are possible
	private int mapCull;		//how many times to repeat the steps of corridor gen (look in loadResoure();

	enum instances{ 
		VOID, 
		WALL, 
		TRAP};

	void Start(){
		loadResources ();
		initializeMap ();
		mapPopulate ();
		playerPlace ();

		//place the player
		for (int i = 2; i < mapRow - 2; i++) {
			for (int j = 2; j < mapCol - 2; j++) {
				if (map [i, 1, j] == instances.VOID ) {
					//set Player Location
					temp = transform.localPosition;
					temp.x = i*2 ;
					temp.y = 2;
					temp.z = j*2; 
					FPSplayer.transform.localPosition = temp;

					break;break;
				} else {

				}
			}
		}///

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
		for(int i = 0; i < mapRow; i++){
			for(int j = 0; j < mapCol; j++){
				map [i,1,j] = instances.WALL;
			}
		}

		//Remove paths from initial ground plan (Maze like iteration)
		//Randomize seed(); ?
		int xx = mapRow;
		int zz = mapCol;
		xx = xx / 2;
		zz = zz / 2;

		//Sets center to 0, player can start from here for now, may change
		map [xx, 1, zz] = instances.VOID;

		int avg = 0;
		int q = 0;

		for (int i = 0; i < mapCull; i++) {


			//
			if (xx > mapRow + 2) {xx = xx-5; }
			if (zz > mapCol + 2) {zz = zz-5; }
			if (xx < 2) {xx = xx+5; }
			if (zz < 2) {zz = zz+5; }
			///
			int m = Random.Range (0, 8);




			if (m == 0) { 
				xx--;
				cullingClamp (xx, zz);
				xx--;
				cullingClamp (xx, zz);
			} else if (m == 2) { 
				xx++;
				cullingClamp (xx, zz);
			} else if (m == 1) { 
				zz++;
				cullingClamp (xx, zz);
			} else if (m == 3) { 
				zz--;
				cullingClamp (xx, zz);
			} else if (m == 4) { 
				xx--;
				cullingClamp (xx, zz);
			} else if (m == 6) { 
				xx++;
				cullingClamp (xx, zz);
				xx++;
				cullingClamp (xx, zz);
			} else if (m == 5) { 
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
				Debug.Log ("not full numb or Something else went wrong too");
				Debug.Log ("xx = " + xx + ", zz = " + zz);
			}



			//Debug.Log ("xx = " + xx + ", zz = " + zz);
		}



		//Smooth the Corridors
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



		//

		//Stack up inside Walls ( + height )
		for (int k = 1; k < mapHeight-1; k++) {
			for(int i = 0; i < mapRow; i++){
				for(int j = 0; j < mapCol; j++){
					if (map [i, k, j] == 1) {
						if (Random.Range (0, 60) < 60-(k*4)){
							map [i, k + 1, j] = instances.WALL;
						}
						if (k == 2) {
							if (Random.Range (0, 60) < 60-(k*4)){
								map [i, k + 1, j] = instances.WALL;
							}
						}
					}
				}
			}

		}

		////Smooth the map
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
		///




	}


	private void mapPopulate (){
		trapPopulate ();

		//Border Walls
		for (int k = 1; k < mapHeight; k++) {
			for (int i = 0; i < mapRow; i++) {
				for (int j = 0; j < mapCol; j++) {
					if (i == 0 || i == mapRow-1) {
						map [i, k, j] = instances.WALL;
					}
					if (j == 0 || j == mapCol-1) {
						map [i, k, j] = instances.WALL;

					}
				}
			}
		}

		// Populate the world (i,k,j are *2 to account for instance scales)
		for(int i = 0; i < mapRow; i++){
			for (int j = 0; j < mapCol; j++) {
				for (int k = 1; k < mapHeight; k++) {
					//Create instances of 'Wall'
					if (map [i, k, j] == instances.WALL) {
						Instantiate (Wall, new Vector3 (i * 2, k *2 , j * 2), Quaternion.identity);
					}

					//Create instances of ~ trap
					else if (map [i, k, j] == instances.TRAP) {
						Instantiate (notWall, new Vector3 (i * 2, k *2 , j * 2), Quaternion.identity);
					}
				}
			}
		}

		//ceiling (has to be run after populate because it places walls above the limit of the array
		for (int i = 0; i < mapRow; i++) {
			for (int j = 0; j < mapCol; j++) {
				Instantiate (Wall, new Vector3 (i * 2, 12 , j * 2), Quaternion.identity);
			}
		}///
	}


	public void cullingClamp(int xx, int zz){
		xx = Mathf.Clamp (xx, 2, mapRow-3);
		zz = Mathf.Clamp (zz, 2, mapCol-3);
		map [xx, 1, zz] = instances.VOID;

	}


	public void trapPopulate(){
		//Traps for inescapable holes
		for(int k = 4; k>=2; k--){
			for (int i = 1; i < mapRow - 1; i++) {
				for (int j = 1; j < mapCol - 1; j++) {
					//if Space next to ceiling is open, and the two spaces below it as well
					if (map [i, k, j] == instances.VOID &&
						map [i - 1, k, j] == instances.WALL &&
						map [i + 1, k, j] == instances.WALL &&
						map [i, k, j - 1] == instances.WALL &&
						map [i, k, j + 1] == instances.WALL) {
						if (map [i, k-1, j] == instances.VOID) {
							if (map [i, k-2, j] == instances.VOID) {
								map [i, k-2, j] = instances.TRAP;
							}
						}
					}
				}


			}
		}
	}


	private void playerPlace(){
		//place the player
		for (int i = 2; i < mapRow - 2; i++) {
			for (int j = 2; j < mapCol - 2; j++) {
				if (map [i, 1, j] == instances.VOID) {
					//set Player Location
					temp = transform.localPosition;
					temp.x = i*2 ;
					temp.y = 2;
					temp.z = j*2; 
					FPSplayer.transform.localPosition = temp;

					break;break;
				} else {

				}
			}
		}

	}



	public int rowReturn(){
		return mapRow;
	}

	public int colReturn(){
		return mapCol;
	}

}
*/