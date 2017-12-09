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
		boxCharacters = "";
		boxCharacterUpNeighbors = new string[boxCharacters.Length];
		boxCharacterDownNeighbors = new string[boxCharacters.Length];
		boxCharacterLeftNeighbors = new string[boxCharacters.Length];
		boxCharacterRightNeighbors = new string[boxCharacters.Length];

		boxCharacterLeftNeighbors [0] = "";

	}
}
