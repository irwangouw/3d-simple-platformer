using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour {

	public int value;
	public GameObject healEffect;

	void Start () {
		
	}
	
	void Update () {
		
	}

	private void OnTriggerEnter(Collider target){
		if(target.tag == "Player"){
			FindObjectOfType<HealthManager> ().HealPlayer (value);
			Instantiate (healEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
