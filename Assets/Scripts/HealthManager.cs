using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

	public int maxHealth;
	public int currentHealth;

	public PlayerMovement thePlayer;

	public float invincibilityLength;
	private float invincibilityCounter;

	public Renderer playerSkin1;
	public Renderer playerSkin2;

	private float flashCounter;
	public float flashLength = 0.1f;

	private bool isSpawning;
	private Vector3 respawnPoint;
	public float respawnLength;

	public Text healthText;

	public GameObject koEffect;

	public Image blackScreen;
	private bool isFadeToBlack;
	private bool isFadeFromBlack;
	public float fadeSpeed;
	public float waitForFade;

	void Start () {
		currentHealth = maxHealth;
		healthText.text = "Health: " + currentHealth;

		thePlayer = FindObjectOfType<PlayerMovement> ();

		respawnPoint = thePlayer.transform.position;
	}
	
	void Update () {
		if (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;

			flashCounter -= Time.deltaTime;

			if(flashCounter <= 0){
				playerSkin1.enabled = !playerSkin1.enabled;
				playerSkin2.enabled = !playerSkin2.enabled;
				flashCounter = flashLength;
			}
			if (invincibilityCounter <= 0) {
				playerSkin1.enabled = true;
				playerSkin2.enabled = true;
			}
		}

		if(isFadeToBlack){
			blackScreen.color = new Color (blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards (blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
			if(blackScreen.color.a == 1f){
				isFadeToBlack = false;
			}
		}

		if(isFadeFromBlack){
			blackScreen.color = new Color (blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards (blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
			if(blackScreen.color.a == 0f){
				isFadeFromBlack = false;
			}
		}

	}

	public void HurtPlayer(int damage, Vector3 direction){

		if(invincibilityCounter <= 0){
			currentHealth -= damage;
			healthText.text = "Health: " + currentHealth;

			if (currentHealth <= 0) {
				Respawn ();
			} else {
				thePlayer.KnockBack (direction);

				invincibilityCounter = invincibilityLength;

				playerSkin1.enabled = false;
				playerSkin2.enabled = false;

				flashCounter = flashLength;
			}
		}
			
	}

	public void HealPlayer(int heal){
		currentHealth += heal;
		healthText.text = "Health: " + currentHealth;

		if(currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
	}

	public void Respawn(){
		if(!isSpawning){
			StartCoroutine ("RespawnCo");
		}
	}

	public IEnumerator RespawnCo(){
		isSpawning = true;
		thePlayer.gameObject.SetActive (false);
		Instantiate (koEffect, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (respawnLength);

		isFadeToBlack = true;

		yield return new WaitForSeconds (waitForFade);

		isFadeToBlack = false;
		isFadeFromBlack = true;

		isSpawning = false;
		thePlayer.gameObject.SetActive (true);

		thePlayer.transform.position = respawnPoint;

		currentHealth = maxHealth;
		healthText.text = "Health: " + currentHealth;

		invincibilityCounter = invincibilityLength;

		playerSkin1.enabled = false;
		playerSkin2.enabled = false;

		flashCounter = flashLength;

	}
}
