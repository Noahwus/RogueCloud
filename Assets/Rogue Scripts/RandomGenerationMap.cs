using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGenerationMap : MonoBehaviour {

	public GameObject Wall;
	public GameObject Trap;
	public GameObject FPSplayer;
	Vector3 temp;

	public int[,,] map;			//Map array
	private int mapRow = 35;	//Rows
	private int mapCol = 35;	//Collums
	private int mapHeight = 7;	//How many stacks of walls are possible
	private int mapCull;		//how many times to repeat the steps of corridor gen (look in loadResoure();

	enum instance{VOID, WALL, TRAP, CHEST};




	void Start(){
		loadResources ();
		initializeMap ();
		mapPopulate ();
		playerPlace ();

		//place the player
		for (int i = 2; i < mapRow - 2; i++) {
			for (int j = 2; j < mapCol - 2; j++) {
				if (map [i, 1, j] == (int)instance.VOID) {
					//set Player Location
					temp = transform.localPosition;
					temp.x = i*2 ;
					temp.y = 2;
					temp.z = j*2; 
					FPSplayer.transform.localPosition = temp;

					break;
				} else {

				}
			}
		}//*/

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
				map [i,1,j] = (int)instance.WALL;
			}
		}

		//Remove paths from initial ground plan (Maze like iteration)
		//Randomize seed(); ?
		int xx = mapRow;
		int zz = mapCol;
		xx = xx / 2;
		zz = zz / 2;

		//Sets center to 0, player can start from here for now, may change
		map [xx, 1, zz] = (int)instance.VOID;
		for (int i = 0; i < mapCull; i++) {
			//
			if (xx > mapRow + 2) {xx = xx-5; }
			if (zz > mapCol + 2) {zz = zz-5; }
			if (xx < 2) {xx = xx+5; }
			if (zz < 2) {zz = zz+5; }
			//*/
			int m = Random.Range (0, 8);




			if (m == 0) { 
				xx--;
				cullingClamp (xx, zz);
				xx--;
				cullingClamp (xx, zz);
			} else if (m == 1) { 
				zz++;
				cullingClamp (xx, zz);
			}else if (m == 2) { 
				xx++;
				cullingClamp (xx, zz);
			}  else if (m == 3) { 
				zz--;
				cullingClamp (xx, zz);
			} else if (m == 4) { 
				xx--;
				cullingClamp (xx, zz);
			}else if (m == 5) { 
				zz++;
				cullingClamp (xx, zz);
				zz++;
				cullingClamp (xx, zz);
			} else if (m == 6) { 
				xx++;
				cullingClamp (xx, zz);
				xx++;
				cullingClamp (xx, zz);
			}  else if (m == 7) {
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



		/*/Smooth the Corridors
		for (int k = 1; k < mapHeight - 2; k++) {
			//Debug.Log (k);
			for (int i = 1; i < mapRow - 1; i++) {
				for (int j = 1; j < mapCol-1; j++) {
					if (map [i + 1, k, j] == (int)instance.VOID ||
						map [i - 1, k, j] == (int)instance.VOID ||
						map [i, k, j + 1] == (int)instance.VOID ||
						map [i, k, j - 1] == (int)instance.VOID) {

						if (Random.Range (0, 10) >= 6+k) {
							map [i, k, j] = (int)instance.VOID;
						}
					}
				}
			}
		}



		//*/

		//Stack up inside Walls ( + height )
		for (int k = 1; k < mapHeight-1; k++) {
			for(int i = 0; i < mapRow; i++){
				for(int j = 0; j < mapCol; j++){
					if (map [i, k, j] == (int)instance.WALL) {
						if (Random.Range (0, 60) < 60-(k*4)){
							map [i, k + 1, j] = (int)instance.WALL;
						}
						if (k == 2) {
							if (Random.Range (0, 60) < 30-(k*4)){
								map [i, k + 1, j] = (int)instance.WALL;
							}
						}
					}
				}
			}

		}
			
		/*///Smooth the map
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


	private void mapPopulate (){
		trapPopulate ();
		chestPopulate ();

		//Border Walls
		for (int k = 1; k < mapHeight; k++) {
			for (int i = 0; i < mapRow; i++) {
				for (int j = 0; j < mapCol; j++) {
					if (i == 0 || i == mapRow-1) {
						map [i, k, j] = (int)instance.WALL;
						map [i, mapHeight + 1, mapRow] = (int)instance.WALL;
					}
					if (j == 0 || j == mapCol-1) {
						map [i, k, j] = (int)instance.WALL;

					}
				}
			}
		}

		// Populate the world (i,k,j are *2 to account for instance scales)
		for(int i = 0; i < mapRow; i++){
			for (int j = 0; j < mapCol; j++) {
				for (int k = 1; k < mapHeight; k++) {
					//Create instances of 'Wall'
					if (map [i, k, j] == (int)instance.WALL) {
						Instantiate (Wall, new Vector3 (i * 2, k * 2, j * 2), Quaternion.identity);
					}
					//Create instances of ~ trap
					else if (map [i, k, j] == (int)instance.TRAP) {
						Instantiate (Trap, new Vector3 (i * 2, k * 2, j * 2), Quaternion.identity);
					}
					//Create instance of ~ Chest
					else if (map [i, k, j] == (int)instance.CHEST) {

					}
					//Create instance of mosters?
					else if(true){

					}
					//Create instance of Ladder?
					//Create instance of
					}
				}
			}

		//ceiling (has to be run after populate because it places walls above the limit of the array
		for (int i = 0; i < mapRow; i++) {
			for (int j = 0; j < mapCol; j++) {
				Instantiate (Wall, new Vector3 (i * 2, mapHeight * 2 , j * 2), Quaternion.identity);
			}
		}//*/
	}




	public void cullingClamp(int xx, int zz){
		xx = Mathf.Clamp (xx, 2, mapRow-3);
		zz = Mathf.Clamp (zz, 2, mapCol-3);
		map [xx, 1, zz] = (int)instance.VOID;

	}




	private void playerPlace(){
		//place the player
		for (int i = 2; i < mapRow - 2; i++) {
			for (int j = 2; j < mapCol - 2; j++) {
				if (map [i, 1, j] == (int)instance.VOID) {
					//set Player Location
					temp = transform.localPosition;
					temp.x = i*2 ;
					temp.y = 2;
					temp.z = j*2; 
					FPSplayer.transform.localPosition = temp;

					break;
				} else {

				}
			}
		}

	}



	public void trapPopulate(){
		//Traps for inescapable holes
		for(int k = mapHeight-1; k>=1; k--){
			for (int i = 1; i < mapRow-1; i++) {
					for (int j = 1; j < mapCol-1; j++) {
						//if Space next to ceiling is open, and the two spaces below it as well
					if (map [i, k, j] == (int)instance.VOID) {
						if ( map [i, k - 1, j] == (int)instance.VOID &&
							map [i - 1, k - 1, j] == (int)instance.WALL &&
							map [i + 1, k - 1, j] == (int)instance.WALL &&
							map [i, k - 1, j - 1] == (int)instance.WALL &&
							map [i, k - 1, j + 1] == (int)instance.WALL) {
							if (map [i, k - 2, j] == (int)instance.VOID) {
								if (k != 2 && map[i, k-3, j] == (int)instance.VOID) {
									map [i, k - 3, j] = (int)instance.TRAP;
								} else {
									map [i, k - 2, j] = (int)instance.TRAP;
								}
										//Debug.Log ("Traps aren't gay");
								}
							} 
						}
					}


			}
		}
	}
		



	public void chestPopulate(){
		for (int i = 1; i < mapRow-1; i++) {
			for (int j = 1; j < mapCol-1; j++) {
				groundPocketPopulate (i, 1, j, (int)instance.CHEST, 1);
			}
		}


	}




	public void groundPocketPopulate(int i, int k, int j, int replace, int isolation){
		if (map [i, k, j] == (int)instance.VOID) {
			//East
			if ((map [i, k, j] == (int)instance.VOID && map [i + 1, k, j] == (int)instance.WALL && map [i, k, j - 1] == (int)instance.WALL && map [i, k, j + 1] == (int)instance.WALL)) {
				if (((map [i - 1, k, j] == (int)instance.VOID && map [i - 1, k, j + 1] == (int)instance.WALL && map [i - 1, k, j - 1] == (int)instance.WALL)) || isolation == 1) {
					if ( ((map [i - 2, k, j] == (int)instance.VOID && map [i - 2, k, j + 1] == (int)instance.WALL && map [i - 2, k, j - 1] == (int)instance.WALL) || isolation == 2) || isolation ==1) {
						if (Random.Range (0, 100) < ((mapCull))) {
							map [i, k, j] = replace;
						}
					}
				}
			//West
			} else if ((map [i, k, j] == (int)instance.VOID && map [i - 1, k, j] == (int)instance.WALL && map [i, k, j - 1] == (int)instance.WALL && map [i, k, j + 1] == (int)instance.WALL)) {
				if(((map[i+1, k, j] == (int)instance.VOID) && (map[i+1,k,j+1] == (int)instance.WALL) && (map[i+1,k,j-1] == (int)instance.WALL)) || isolation == 1){
					if(((map[i+2, k, j] == (int)instance.VOID) && (map[i+2,k,j+1] == (int)instance.WALL) && (map[i+2,k,j-1] == (int)instance.WALL) || isolation == 2) || isolation == 1){
						if(Random.Range(0,100)<((mapCull))){
							map[i,k,j] = replace;
						}
					}
				}
			//North
			} else if ((map [i, k, j] == (int)instance.VOID && map [i - 1, k, j] == (int)instance.WALL && map [i + 1, k, j] == (int)instance.WALL && map [i, k, j + 1] == (int)instance.WALL)) {
				if ((map [i, k, j - 1] == (int)instance.VOID && map [i - 1, k, j - 1] == (int)instance.WALL && map [i + 1, k, j - 1] == (int)instance.WALL) || isolation == 1) {
					if(((map[i, k, j - 1] == (int)instance.VOID) && (map[i + 2, k , j + 1] == (int)instance.WALL) && (map[i + 2 , k , j - 1] ==(int)instance.WALL) || isolation == 2) || isolation == 1 ){
						if (Random.Range (0, 100) < ((mapCull))) {
							map [i, k, j] = replace;
						}
					}
				}
			//South
			} else if( (map [i, k, j] == (int)instance.VOID && map [i - 1, k, j] == (int)instance.WALL && map [i + 1, k, j] == (int)instance.WALL && map [i, k, j - 1] == (int)instance.WALL)) {
				if((map[i, k, j+1] == (int)instance.VOID && map[i-1,k,j+1] == (int)instance.WALL && map[i+1,k,j+1] ==(int)instance.WALL) || isolation == 1){
					if (((map [i, k, j + 1] == (int)instance.VOID && map [i - 2, k, j + 1] == (int)instance.WALL && map [i + 2, k, j + 1] == (int)instance.WALL) || isolation == 2) || isolation == 1) {
						if (Random.Range (0, 100) < ((mapCull ))) {
							map [i, k, j] = replace;
						}
					}
				}
			}else{}

				}else{//nothing
		}
	}


	/*
	public int NeighborLeft(int i, int k, int j){
		return map [i-1, k, j];
	}

	public int NeighborRight(int i, int k, int j){
		return map [i+1, k, j];
	}

	public int NeighborUp(int i, int k, int j){
		return map [i, k, j+1];
	}

	public int NeighborDown(int i, int k, int j){
		return map [i, k, j-1];
	}

	public int NeighborAbove(int i, int k, int j){
		return map [i, k+1, j];
	}

	public int NeighborBelow(int i, int k, int j){
		return map [i, k-1, j];
	}

*/





	public int rowReturn(){
		return mapRow;
	}

	public int colReturn(){
		return mapCol;
	}

}
