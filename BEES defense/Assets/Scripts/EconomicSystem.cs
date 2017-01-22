﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicSystem : Singleton<EconomicSystem> {

	[SerializeField] private int[] prices;
	[SerializeField] private int startMoney;
	[SerializeField] private int currentHoney;
	[SerializeField] public GameObject beePrefab_;

	// Use this for initialization
	void Start () {
		currentHoney = startMoney;
		GameUI.Instance.SetHoneyCount (currentHoney);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void AddHoney(int Amount)
  {
    currentHoney += Amount;
    GameUI.Instance.SetHoneyCount(currentHoney);
  }

	public void BuyItem(int consu)
	{
		Debug.Log ("Buy Item");
		if (currentHoney - prices [consu] >= 0) {
			currentHoney -= prices [consu];
			GameUI.Instance.SetHoneyCount (currentHoney);
			Debug.Log ("Element Bought: " + consu + " Money Left: " + currentHoney);
			if (consu > 0) {
				DefensesHandler.Instance.CreateDefense (consu - 1);
			}

			//New Bee
			if (consu == 0) {
				GameObject oldBee = GameObject.FindGameObjectWithTag ("Bee");
				Instantiate (beePrefab_, oldBee.transform.parent, true);
			}
			//Instanciate element
		} else {
			Debug.Log ("No Enought Money");
		}
	}
}
