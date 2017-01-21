using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensesHandler : Singleton<DefensesHandler> {

	[SerializeField] private GameObject[] defenses_;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateDefense(int type)
	{
		Debug.Log ("Create new defense type " + type);
		GameObject newDefense = Instantiate (defenses_ [type] as GameObject);
		newDefense.GetComponent<Defense> ().Init ((Defense.typeOfDefense)type);
		newDefense.transform.parent = this.transform;
		GameController.Instance.SetDefenseToHand (newDefense);
	}
}
