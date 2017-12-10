using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {
	public int mapRows = 10;
	public int mapColums = 10;

	private char[,] map;

	private string boxCharacters;
	private string[] boxCharacterUpNeighbors;
	private string[] boxCharacterDownNeighbors;
	private string[] boxCharacterLeftNeighbors;
	private string[] boxCharacterRightNeighbors;

	void Start(){
		InitializeBoxCharacters ();
		//InititializeMap();
		//DisplayMap();
	}

	private void InitializeBoxCharacters(){
		boxCharacters = "01234567";
		boxCharacterUpNeighbors = new string[boxCharacters.Length];
		boxCharacterDownNeighbors = new string[boxCharacters.Length];
		boxCharacterLeftNeighbors = new string[boxCharacters.Length];
		boxCharacterRightNeighbors = new string[boxCharacters.Length];

		boxCharacterLeftNeighbors [0] = "0246";
		boxCharacterLeftNeighbors [1] = "1357";
		boxCharacterLeftNeighbors [2] = "1X";
		boxCharacterLeftNeighbors [3] = "026X";
		boxCharacterLeftNeighbors [4] = "5X";
		boxCharacterLeftNeighbors [5] = "046";
		boxCharacterLeftNeighbors [6] = "0245X";
		boxCharacterLeftNeighbors [7] = "17X";

		boxCharacterRightNeighbors[0] = "0356";
		boxCharacterRightNeighbors[1] = "17X";
		boxCharacterRightNeighbors[2] = "06";
		boxCharacterRightNeighbors[3] = "1X";
		boxCharacterRightNeighbors[4] = "06";
		boxCharacterRightNeighbors[5] = "1X";
		boxCharacterRightNeighbors[6] = "035X";
		boxCharacterRightNeighbors[7] = "1X";

		boxCharacterUpNeighbors[0] = "0X";
		boxCharacterUpNeighbors[1] = "";
		boxCharacterUpNeighbors[2] = "";
		boxCharacterUpNeighbors[3] = "";
		boxCharacterUpNeighbors[4] = "";
		boxCharacterUpNeighbors[5] = "";
		boxCharacterUpNeighbors[6] = "";
		boxCharacterUpNeighbors[7] = "";


	}
}
