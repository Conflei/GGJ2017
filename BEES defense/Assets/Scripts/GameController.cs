using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController> {

	public bool occupiedHand{ get; set;}
	private GameObject onHandGO;

	public bool shootingMode_;

	[SerializeField] GameObject[] machineGuns_;
	[SerializeField] GameObject bullet_;
	private GameObject usingMachineGun_;

	[Header("Day & Night Stuff")]
	public bool onDay;
	public int minPerDay = 0;
	public int secPerDay = 10;

	public int minPerNight = 0;
	public int secPerNight = 20;

	// Use this for initialization
	public IEnumerator Start () {
		onDay = true;
		occupiedHand = false;
		GameUI.Instance.courtine.color = Color.white;
		yield return StartCoroutine (GameUI.Instance.HideCourtine ());
		StartCoroutine (GameUI.Instance.BeginTime (minPerDay, secPerDay));
		yield return null;
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

	public void EnableShootingMode(int machine)
	{
		if (occupiedHand || shootingMode_)
			return;

		usingMachineGun_ = machineGuns_ [machine];

		shootingMode_ = true;

		GameUI.Instance.EnterAimMode ();
	}

	public void ExitShootingMode()
	{
		shootingMode_ = false;

		GameUI.Instance.ExitAimMode ();
	}

	public void ShootTo(MapTile tileScript)
	{
		
		GameUI.Instance.Shake ();
		ExitShootingMode ();
		//SHOOOOT
		GameObject newBullet = Instantiate (bullet_, Camera.main.ScreenToWorldPoint(usingMachineGun_.transform.position), this.transform.rotation) as GameObject;
		newBullet.GetComponent<Bullet> ().Init (tileScript.tilePosition_);

	}

	public void TimeOver()
	{
		if (onDay)
			StartCoroutine(BeginNight ());
		else
			StartCoroutine(BeginDay ());
	}

	public IEnumerator BeginNight()
	{
		onDay = false;
		yield return StartCoroutine (GameUI.Instance.ShowNight ());
		StartCoroutine (GameUI.Instance.BeginTime (minPerNight, secPerNight));
		Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves> ().enabled = false;
		yield return StartCoroutine (GameUI.Instance.HideCourtine ());

	}

	public IEnumerator BeginDay()
	{
		onDay = true;
		yield return StartCoroutine (GameUI.Instance.ShowDay ());
		StartCoroutine (GameUI.Instance.BeginTime (minPerDay, secPerDay));
		Camera.main.GetComponent<UnityStandardAssets.ImageEffects.ColorCorrectionCurves> ().enabled = true;
		yield return StartCoroutine (GameUI.Instance.HideCourtine ());

		for (int i = 0; i < Random.Range (3, 6); i++) {
			EnemySpawner.Instance.SpawnNextWave ();
			yield return new WaitForSeconds (3f);
		}

	}
}
