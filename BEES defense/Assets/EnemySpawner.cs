using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  public bool TestLeftPath;

  public int RunnerCount = 10;

  public System.Collections.Generic.List<Vector3> leftPath;
  public System.Collections.Generic.List<Vector3> midPath;
  public System.Collections.Generic.List<Vector3> rightPath;

  public EnemyMovement runnerPrefab;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    if(TestLeftPath)
    {
      TestLeftPath = false;
      SpawnLeftPath();
    }
  }

  public void SpawnLeftPath()
  {
    for(int i = 0; i < RunnerCount; i++)
    {
      EnemyMovement em = Instantiate<EnemyMovement>(runnerPrefab);
      em.transform.position = transform.position;
      em.majorWaypoints.Clear();
      em.majorWaypoints.AddRange(leftPath);
      //em.majorWaypoints = leftPath;
      Debug.Log("SpawnLeftPath!");
    }
  }

  void OnDrawGizmosSelected()
  {
    //Draw out left path
    Gizmos.color = Color.cyan;
    for (int i = 0; i < leftPath.Count - 1; i++)
    {
      Gizmos.DrawLine(leftPath[i], leftPath[i + 1]);
    }
    Gizmos.color = Color.blue;
    for (int i = 0; i < midPath.Count - 1; i++)
    {
      Gizmos.DrawLine(midPath[i], midPath[i + 1]);
    }
    Gizmos.color = Color.red;
    for (int i = 0; i < rightPath.Count - 1; i++)
    {
      Gizmos.DrawLine(rightPath[i], rightPath[i + 1]);
    }

  }
  

}
