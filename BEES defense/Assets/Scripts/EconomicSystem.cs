using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomicSystem : Singleton<EconomicSystem> {

	public enum Consumables{
		Bee,
		PoisonIvy,
		Sunflower,
		Venus
	}

	[SerializeField] private int[] prices;
	[SerializeField] private int startMoney;
	[SerializeField] private int currentHoney;

	// Use this for initialization
	void Start () {
		currentHoney = startMoney;
		GameUI.Instance.SetHoneyCount (currentHoney);
	}
	
	// Update is called once per frame
	void Update () {
		
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
			//Instanciate element
		} else {
			Debug.Log ("No Enought Money");
		}
	}
}
