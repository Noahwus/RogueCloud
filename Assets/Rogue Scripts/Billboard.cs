using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

	GameObject target;
	public CapsuleCollider cap;
	Vector3 temp;
	float step;


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

		//transform.LookAt (targetPosition);

		Vector3 capPos = new Vector3 (cap.transform.localPosition.x, cap.transform.localPosition.y, cap.transform.localPosition.z);
		step = 2 * Time.deltaTime;

		//temp = transform.localPosition;
		//temp.x = Mathf.Clamp (temp.x, -cap.radius, cap.radius);
		//Vector3.MoveTowards(capPos, targetPosition, step);
		//temp.z = Mathf.Clamp (temp.z, -cap.radius, cap.radius);
		Vector3.MoveTowards(capPos, targetPosition, step);
		//transform.localPosition = temp;

		/*/This is hella cool, Use this sometime in the future
		temp = transform.localPosition;
		temp.x = cap.radius * Mathf.Cos (Vector3.Angle(cap.transform.localPosition, targetPosition));
		temp.z = cap.radius * Mathf.Sin (Vector3.Angle(cap.transform.localPosition, targetPosition));
		transform.localPosition = temp;
		//*/
	}
}
