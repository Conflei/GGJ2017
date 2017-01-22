using System.Collections;
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
			
			if (consu > 0) {
				currentHoney -= prices [consu];
				DefensesHandler.Instance.CreateDefense (consu - 1);
			}

			//New Bee
			if (consu == 0 && BeeHandler.Instance.beeCount_>1) {
				currentHoney -= prices [consu];
				BeeHandler.Instance.MakeBee ();
			}

			GameUI.Instance.SetHoneyCount (currentHoney);
			//Instanciate element
		} else {
			Debug.Log ("No Enought Money");
		}
	}
}
