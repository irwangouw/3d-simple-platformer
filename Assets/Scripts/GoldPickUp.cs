using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickUp : MonoBehaviour {

	public int value;
	public GameObject goldPickUpEffect;

	void Start () {
		
	}
	
	void Update () {
		
	}

	private void OnTriggerEnter(Collider target){
		if(target.tag == "Player"){
			FindObjectOfType<GameManager>().AddGold (value);
			Instantiate (goldPickUpEffect, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
