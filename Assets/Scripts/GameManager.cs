using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public int currentGold;
	public Text scoreGoldText;

	void Start () {
		
	}
	
	void Update () {
		
	}

	public void AddGold(int goldToAdd){
		currentGold += goldToAdd;

		scoreGoldText.text = "Gold: " + currentGold;
	}
}
