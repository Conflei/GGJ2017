using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvy : Defense {

	public float delayTimeToHit_ = 0.5f;
	public int damagePerHit_ = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D colli)
	{
		if (colli.gameObject.tag != "Alien")
			return;


	}
}
