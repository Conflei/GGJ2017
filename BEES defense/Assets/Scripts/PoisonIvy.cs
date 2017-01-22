using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonIvy : Defense {

	public float delayTimeToHit_ = 0.5f;
	public int damagePerHit_ = 5;

  private float delayTimeRemaining;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if (delayTimeRemaining > 0) delayTimeRemaining -= Time.deltaTime;
	}

	public void OnTriggerEnter2D(Collider2D colli)
	{
		if (colli.gameObject.tag != "Alien")
			return;
    if(delayTimeRemaining <= 0)
    {
      delayTimeRemaining = delayTimeToHit_;
      colli.GetComponent<EnemyMovement>().TakeHit(damagePerHit_, 0.5f, 5f);
    }
  }
}
