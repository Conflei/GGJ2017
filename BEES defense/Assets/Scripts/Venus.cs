using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : Defense {

	public float delayTimeToShoot_ = 0.5f;
	public int damagePerHit_ = 5;

	[SerializeField] private AutoSight sight_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sight_.targetLocked_ && delayTimeToShoot_ <= 0f) {
			Hit ();
		}
		if (delayTimeToShoot_ > 0)
			delayTimeToShoot_ -= Time.deltaTime;
	}

	public void Hit()
	{
		sight_.onSight.GetComponent<EnemyMovement> ().TakeHit (damagePerHit_);	
	}
}
