using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	GameObject target;
	public CapsuleCollider cap;
	private float rotationX;


	// Use this for initialization
	void Start() {
		cap = GetComponentInParent<CapsuleCollider> ();
		target = GameObject.Find ("FPSPlayer");

	}
	
	// Update is called once per frame
	void Update () {

		Vector3 targetPosition = new Vector3 (target.transform.position.x, 
			transform.position.y,
			target.transform.position.z);

		cap.transform.LookAt (targetPosition);





		/*/This is hella cool, Use this sometime in the future
		temp = transform.position;
		temp.x = cap.radius * Mathf.Cos (Vector3.Angle(cap.transform.localPosition, targetPosition));
		temp.z = cap.radius * Mathf.Sin (Vector3.Angle(cap.transform.localPosition, targetPosition));
		transform.position = temp;
		//*/

		/*/This is hella cool, Use this sometime in the future
		temp = transform.position;
		temp.x = cap.radius * Mathf.Cos (Vector3.Angle(cap.transform.localPosition, targetPosition));
		temp.z = cap.radius * Mathf.Sin (Vector3.Angle(cap.transform.localPosition, targetPosition));
		transform.position = temp;
		//*/
	}
}
