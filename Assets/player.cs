using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
public class player : MonoBehaviour {


	PlayerController controller;
	public float moveSpeed = 5;
	public float jumpForce = 5;

	public LayerMask groundLayers;
	public CapsuleCollider col;


	void Start(){
		controller = GetComponent<PlayerController> ();
		col = GetComponent<CapsuleCollider> ();
		
	}

	void Update(){
		Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;

		controller.Move (moveVelocity);

		if(IsGrounded() && Input.GetKeyDown(KeyCode.Space)){
			controller.Jump (jumpForce);
	}


}
	private bool IsGrounded(){
		return Physics.CheckCapsule(col.bounds.center,
			new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z),
			col.radius*.9f, groundLayers);
	}

}