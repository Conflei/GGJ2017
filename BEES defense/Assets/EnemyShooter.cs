using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : EnemyMovement {

  [Header("Shooty stuff")]
  public float reloadTime = 1f;
  public float reloadTimeRemaining;

  public float scanTime = 0.25f;
  public float scanTimeRemaining;

  public float scanRange;

  public EnemyProjectile enemyProjectilePrefab;
  public Vector3 ProjectileOffset;


  public bool bTestDie;
  public bool bTestShoot;

  // Use this for initialization
  protected override void Start () {
    base.Start();
    reloadTimeRemaining = reloadTime;
    scanTimeRemaining = Random.Range(0f,scanTime);
	}
	
	// Update is called once per frame
	protected override void Update () {
    base.Update();

    if(bTestDie)
    {
      Die();
    }

    if(bTestShoot)
    {
      am.SetTrigger("Shoot");
      bTestShoot = false;
    }


    reloadTimeRemaining -= Time.deltaTime;
    if(reloadTimeRemaining <= 0)
    {
      scanTimeRemaining -= Time.deltaTime;
      if(scanTimeRemaining <= 0)
      {
        Collider2D target = Physics2D.OverlapCircle(transform.position, scanRange, LayerMask.GetMask("Towers"));
        if(target)
        {
          //TODO: Instantiate projectile and point it at the target
          EnemyProjectile ep = Instantiate<EnemyProjectile>(enemyProjectilePrefab);
          ep.transform.position = transform.position + ProjectileOffset;
          ep.moveVector = (target.transform.position - ep.transform.position);

          am.SetTrigger("Shoot");
          

          reloadTimeRemaining = reloadTime;
        }
        scanTimeRemaining = scanTime;
      }
    }

	}

  protected override void OnDrawGizmosSelected()
  {
    base.OnDrawGizmosSelected();
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, scanRange);

    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position + ProjectileOffset, 0.1f);

  }
}
