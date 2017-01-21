using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSight : MonoBehaviour {

	[SerializeField] public GameObject onSight;

	public bool targetLocked_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (targetLocked_) {
			if (onSight == null)
				targetLocked_ = false;
		}
	}


	public void OnTriggerEnter2D(Collider2D objectSpotted)
	{
		if (targetLocked_)
			return;
		if (objectSpotted.tag == "Alien") {
			onSight = objectSpotted.gameObject;
			targetLocked_ = true;
		}
	}

	public void OnTriggerExit2D(Collider2D objectSpotted)
	{
		if (objectSpotted.gameObject == onSight) {
			targetLocked_ = false;
			onSight = null;
		}
	}
}
