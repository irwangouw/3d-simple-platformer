using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;

	//public Rigidbody body;
	public CharacterController controller;

	private Vector3 moveDirection;
	public float gravityScale;

	public Animator anim;
	public Transform pivot;
	public float rotateSpeed;

	public GameObject playerModel;

	public float knockBackForce;
	public float knockBackTime;
	public float knockBackCounter;

	void Start () {
		//body = GetComponent<Rigidbody> ();

		controller = GetComponent<CharacterController> ();
	}
	
	void Update () {
		/*
		body.velocity = new Vector3 (-Input.GetAxis("Horizontal") * moveSpeed, 
			body.velocity.y, -Input.GetAxis("Vertical") * moveSpeed);

		if (Input.GetKeyDown (KeyCode.Space)) {
			body.velocity = new Vector3 (body.velocity.x, jumpForce, body.velocity.z);
		}
		*/

		//moveDirection = new Vector3 (Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

		if (knockBackCounter <= 0) {
			float yStore = moveDirection.y;
			moveDirection = (transform.forward * Input.GetAxis ("Vertical")) + (transform.right * Input.GetAxis ("Horizontal"));
			moveDirection = moveDirection.normalized * moveSpeed;
			moveDirection.y = yStore;

			if (controller.isGrounded) {
				moveDirection.y = 0f;
				if (Input.GetButtonDown ("Jump")) {
					moveDirection.y = jumpForce;
				}
			}
		} else {
			knockBackCounter -= Time.deltaTime;
		}


		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

		controller.Move (moveDirection * Time.deltaTime);

		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			transform.rotation = Quaternion.Euler (0f, pivot.rotation.eulerAngles.y, 0f);
			Quaternion newRotation = Quaternion.LookRotation (new Vector3 (moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp (playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
		}

		anim.SetBool ("IsGrounded", controller.isGrounded);
		anim.SetFloat ("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
	}

	public void KnockBack(Vector3 direction){
		knockBackCounter = knockBackTime;

		moveDirection = direction * knockBackForce;
	}
}
