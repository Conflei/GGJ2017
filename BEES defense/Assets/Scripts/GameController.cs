using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {

	private bool occupiedHand;
	private GameObject onHandGO;

	// Use this for initialization
	void Start () {
		occupiedHand = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (occupiedHand) {
			onHandGO.transform.position = Input.mousePosition;
		}
	}

	public void FreeTileClicked(MapTile myTile)
	{
		//if defense on hand
		//place and occupy the tile
	}

	public void SetDefenseToHand(GameObject defense)
	{
		occupiedHand = true;
		onHandGO = defense;
	}
}
