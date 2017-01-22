using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeHandler : Singleton<BeeHandler> {

	[SerializeField] GameObject beePrefab_;
	[SerializeField] public int beeCount_ = 15;

	// Use this for initialization
	void Start () {
		GameUI.Instance.SetBeeCount (beeCount_);

		for (int i = 0; i < beeCount_; i++) {
			GameObject newBee = Instantiate (beePrefab_, this.transform.position, Quaternion.identity, this.transform) as GameObject;
		}
	}

	/// <summary>
	/// Makes a new bee
	/// </summary>
	public void MakeBee()
	{
		GameObject newBee = Instantiate (beePrefab_, this.transform.transform.position, Quaternion.identity, this.transform) as GameObject;
		beeCount_++;
		GameUI.Instance.SetBeeCount (beeCount_);
	}

	public void DiscountBee()
	{
		Destroy (this.transform.GetChild (0).gameObject);
		beeCount_--;
		GameUI.Instance.SetBeeCount (beeCount_);
	}
}
