using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class player : MonoBehaviour {


	PlayerController controller;
	public float moveSpeed = 5;
	public float jumpSpeed = 5;


	void Start(){
		controller = GetComponent<PlayerController> ();
		
	}

	void Update(){
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;

		controller.Move (moveVelocity);

		if(
	}


}
