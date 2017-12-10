using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScale : MonoBehaviour {

	RandomGenerationMap dungeon;
	
	Vector3 temp;

	void Start(){
		dungeon = GetComponent<RandomGenerationMap> ();

		temp = transform.localScale;
		temp.x = dungeon.rowReturn()/4;
		temp.z = dungeon.colReturn()/4;
		transform.localScale = temp;



		temp = transform.localPosition;
		temp.x = dungeon.rowReturn ();
		temp.y = 1;
		temp.z = dungeon.colReturn ();
		transform.localPosition = temp;
	}
}
