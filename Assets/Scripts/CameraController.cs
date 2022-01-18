using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float rotateSpeed;
	public Transform pivot;

	void Start () {
		offset = target.position - transform.position;

		pivot.transform.position = target.transform.position;
		//pivot.transform.parent = target.transform;
		pivot.transform.parent = null;

		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void LateUpdate () {
		float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		float vertical = Input.GetAxis ("Mouse Y") * rotateSpeed;

		pivot.Rotate (0, horizontal, 0);
		pivot.Rotate (vertical, 0, 0);

		if(pivot.rotation.eulerAngles.x > 45f && pivot.rotation.eulerAngles.x < 180f){
			pivot.rotation = Quaternion.Euler (45f, 0, 0);
		}

		if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 315f){
			pivot.rotation = Quaternion.Euler (315f, 0, 0);
		}

		float desireYAngle = pivot.eulerAngles.y;
		float desireXAngle = pivot.eulerAngles.x;

		Quaternion rotation = Quaternion.Euler (desireXAngle, desireYAngle, 0);

		transform.position = target.position - (rotation * offset);

		if(transform.position.y < target.position.y){
			transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
		}

		transform.LookAt (target);
	}
}
