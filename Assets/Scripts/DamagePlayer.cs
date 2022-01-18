using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour {

	public int damageToPlayer;

	private void OnTriggerEnter(Collider target){
		if(target.tag == "Player"){
			Vector3 hitDirection = target.transform.position - transform.position;
			hitDirection = hitDirection.normalized;

			FindObjectOfType<HealthManager> ().HurtPlayer (damageToPlayer, hitDirection);
		}
	}
}
