using System.Collections;
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

	// Use this for initialization
	void Start () {
    currentSourcePoint = transform.position;
    currentDestinationPoint = majorWaypoints[0];
    CalcWanderPoint();
    Debug.Log("EnemyMovement.Start");
    
	}
	
	// Update is called once per frame
	void Update () {
    //Vector3 diff = currentDestinationPoint - transform.position;

    //if (diff.magnitude < wanderMagnitude)
    //if (false) 
    //{
    //  currentSourcePoint = currentDestinationPoint;
    //  if(majorWaypoints.Count > 0)
    //  {
    //    currentDestinationPoint = majorWaypoints[0];
    //    majorWaypoints.RemoveAt(0);
    //    //I'd just use a queue, but the unity editor doesn't display those as conveniently
    //    CalcWanderPoint();
    //  }
      
      
    //}
    //else
    {
      Vector3 wanderDiff = currentWanderPoint - transform.position;
      if(wanderDiff.magnitude < wanderMagnitude)
      {
        CalcWanderPoint();
      }
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

  void OnDrawGizmosSelected()
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
}
