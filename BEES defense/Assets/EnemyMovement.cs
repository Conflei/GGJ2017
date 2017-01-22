﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

  public float speed = 3;

  public System.Collections.Generic.List<Vector3> majorWaypoints;
  public Vector3 currentSourcePoint;
  public Vector3 currentDestinationPoint;
  public Vector3 currentWanderPoint;

  public float wanderStepDistance = 0.5f;
  public float wanderMagnitude = 0.16f;

  public int Health = 10;
  //Will need animator for... animating stuff.

  private Animator am;

  // Use this for initialization
  protected virtual void Start () {
    currentSourcePoint = transform.position;
    if(majorWaypoints.Count > 0)
      currentDestinationPoint = majorWaypoints[0];
    CalcWanderPoint();
    //Debug.Log("EnemyMovement.Start");
    am = GetComponent<Animator>();

  }
	
	// Update is called once per frame
	protected virtual void Update () {
    
    Vector3 wanderDiff = currentWanderPoint - transform.position;
    if(wanderDiff.magnitude < wanderMagnitude)
    {
      CalcWanderPoint();
    }
    
    transform.position = Vector3.MoveTowards(transform.position, currentWanderPoint, speed * Time.deltaTime);
  }

  void CalcWanderPoint()
  {
    //We want a point closer to the current destination
    //And up to wanderMagnitude away from the line
    Vector3 pathLine = currentDestinationPoint - currentSourcePoint;
    if(pathLine.magnitude < wanderStepDistance)
    {
      currentWanderPoint = currentDestinationPoint + ((Vector3)Random.insideUnitCircle * wanderMagnitude);
      if(majorWaypoints.Count > 0)
      {
        currentDestinationPoint = majorWaypoints[0];
        majorWaypoints.RemoveAt(0);
      }
    }
    else
    {
      //pathLine.Normalize();
      pathLine = Vector3.ClampMagnitude(pathLine, wanderStepDistance);
      //currentWanderPoint = transform.position + pathLine + ((Vector3)Random.insideUnitCircle * wanderMagnitude);
      currentWanderPoint = currentSourcePoint + pathLine + ((Vector3)Random.insideUnitCircle * wanderMagnitude);
      //Vector3 

    }
    currentSourcePoint = currentSourcePoint + pathLine;
    
  }

  protected virtual void OnDrawGizmosSelected()
  {

    Gizmos.DrawLine(currentSourcePoint, currentDestinationPoint);
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(transform.position, currentWanderPoint);
    Gizmos.color = Color.cyan;
    if (majorWaypoints.Count > 0)
      Gizmos.DrawLine(currentDestinationPoint, majorWaypoints[0]);
    for (int i = 0; i < majorWaypoints.Count - 1; i++)
    {
      Gizmos.DrawLine(majorWaypoints[i], majorWaypoints[i + 1]);
    }

  }

  public virtual void TakeHit(int dmgAmount)
  {
    //Spawn some kind of hit effect?  Glowy blue blood spatters maybe?
    Health -= dmgAmount;
    if (Health <= 0)
      Die();
  }

  public virtual void Die()
  {
    //TODO: Set off dying animation

    
    am.SetTrigger("Die");
    Destroy(gameObject, 1);
    this.enabled = false;

  }

  public virtual void KillFlower(Flower targetFlower)
  {
    //TODO: Set off animation for destroying the flower
    am.SetTrigger("Die");
    Destroy(gameObject, 1);
    Destroy(targetFlower.gameObject);
    this.enabled = false;
  }

  
  public virtual void DieImmediate()
  {
    Destroy(gameObject);
  }

}