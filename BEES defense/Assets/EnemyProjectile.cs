using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

  public float speed = 2.5f;
  public int damage = 1;
  public float remainingLifespan = 1f;
  public Vector3 moveVector;

	// Use this for initialization
	void Start () {
		
	}

  // Update is called once per frame
  void Update() {
    Vector3 newPos = Vector3.MoveTowards(transform.position, transform.position + moveVector, Time.deltaTime * speed);
    transform.position = newPos;
    remainingLifespan -= Time.deltaTime;
    if (remainingLifespan <= 0)
    {
      Destroy(gameObject);
    }
	}

  void OnTriggerEnter2D(Collider2D coll)
  {
    AutoTurret test = coll.GetComponentInParent<AutoTurret>();
    if(test)
    {
      test.TakeHit(damage);

      Destroy(gameObject);
    }
    
  }
}
