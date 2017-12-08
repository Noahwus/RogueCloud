using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Rigidbody playerRigidbody;

	void Start(){
		playerRigidbody = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity){
		velocity = _velocity;
	}

	public void Jump(float jumpForce){
		print ("Worked");
		playerRigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
	}

	public void FixedUpdate(){
		playerRigidbody.MovePosition (playerRigidbody.position + velocity * Time.fixedDeltaTime);

	}

}
