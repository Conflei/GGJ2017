using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {

	public bool occupiedHand{ get; set;}
	private GameObject onHandGO;

	// Use this for initialization
	void Start () {
		occupiedHand = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (occupiedHand) {
			onHandGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			onHandGO.transform.position = new Vector3 (onHandGO.transform.position.x, onHandGO.transform.position.y, 0f);
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

	public void SetObjectAt(MapTile tileScript)
	{
		if (tileScript.Occupied)
			return;

		if (tileScript.Blocked)
			return;

		if (tileScript.Path && onHandGO.GetComponent<Defense> ().myType_ != Defense.typeOfDefense.Ivy)
			return;
		
		occupiedHand = false;
		onHandGO.transform.position = tileScript.tilePosition_;
		tileScript.Occupied = true;

	}
}
