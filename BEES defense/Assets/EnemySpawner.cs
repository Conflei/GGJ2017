using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  public bool TestNextWave;

  public bool TestLeftPath;
  public bool TestMidPath;
  public bool TestRightPath;

  public int waveNumber = 0;

  public int RunnerCount = 10;

  public System.Collections.Generic.List<Vector3> leftPath;
  public System.Collections.Generic.List<Vector3> midPath;
  public System.Collections.Generic.List<Vector3> rightPath;

  public EnemyMovement runnerPrefab;
  public EnemyMovement shooterPrefab;

  // Use this for initialization
	void Start () {
    SpawnNextWave();
	}

  // Update is called once per frame
  void Update() {
    if (TestLeftPath)
    {
      TestLeftPath = false;
      StartCoroutine(SpawnOverTime(leftPath, 10, 8, runnerPrefab, 1f));
    }

    if (TestMidPath)
    {
      TestMidPath = false;
      StartCoroutine(SpawnOverTime(midPath, 10, 3, runnerPrefab, 0.5f));
    }
    if (TestRightPath)
    {
      TestRightPath = false;
      //SpawnPath(rightPath, 10, runnerPrefab);
      StartCoroutine(SpawnOverTime(rightPath, 10, 1,  runnerPrefab, 0.5f));
    }
    if(TestNextWave)
    {
      TestNextWave = false;
      SpawnNextWave();
    }
  }

  public void SpawnPath(System.Collections.Generic.List<Vector3> path, int enemyCount, EnemyMovement enemyPrefab)
  {
    for (int i = 0; i < enemyCount; i++)
    {
      EnemyMovement em = Instantiate<EnemyMovement>(enemyPrefab);
      em.transform.position = transform.position;
      em.majorWaypoints.Clear();
      em.majorWaypoints.AddRange(path);
    }
  }

  private IEnumerator SpawnOverTime(System.Collections.Generic.List<Vector3> path, int enemyCount, int enemiesPerWave, EnemyMovement enemyPrefab, float waitTime)
  {
    if (enemiesPerWave <= 0) enemiesPerWave = 1;
    for (int i=0; i< enemyCount; i += enemiesPerWave)
    {
      if (enemyCount - i < enemiesPerWave) enemiesPerWave = enemyCount - i;
      for (int j = 0; j < enemiesPerWave; j++)
      {
        EnemyMovement em = Instantiate<EnemyMovement>(enemyPrefab);
        em.transform.position = transform.position;
        em.majorWaypoints.Clear();
        em.majorWaypoints.AddRange(path);
      }
      yield return new WaitForSeconds(waitTime);
    }
  }

  private IEnumerator SpawnAfterDelay(System.Collections.Generic.List<Vector3> path, int enemyCount, int enemiesPerWave, EnemyMovement enemyPrefab, float waitTime, float waveDelay)
  {
    yield return new WaitForSeconds(waveDelay);

    StartCoroutine(SpawnOverTime(path, enemyCount, enemiesPerWave, enemyPrefab, waitTime));
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
  


  public void SpawnNextWave()
  {
    waveNumber += 1;
    switch(waveNumber)
    {
      case 1:
        StartCoroutine(SpawnAfterDelay(rightPath, 1, 1, shooterPrefab, 1f, 5f));
        StartCoroutine(SpawnAfterDelay(rightPath, 10, 1, runnerPrefab, 0.5f, 5f));
        StartCoroutine(SpawnAfterDelay(leftPath, 10, 2, runnerPrefab, 2f, 10f));
        StartCoroutine(SpawnAfterDelay(midPath, 10, 3, runnerPrefab, 1f, 15f));
        break;
      case 2:
        StartCoroutine(SpawnAfterDelay(leftPath, 20, 1, runnerPrefab, 0.5f, 5f));
        StartCoroutine(SpawnAfterDelay(leftPath, 10, 2, runnerPrefab, 2f, 10f));
        StartCoroutine(SpawnAfterDelay(rightPath, 5, 1, shooterPrefab, 10f, 1f));
        StartCoroutine(SpawnAfterDelay(midPath, 10, 3, runnerPrefab, 1f, 15f));
        break;
      case 3:
        StartCoroutine(SpawnAfterDelay(leftPath, 20, 1, runnerPrefab, 0.5f, 5f));
        StartCoroutine(SpawnAfterDelay(leftPath, 10, 2, runnerPrefab, 2f, 10f));
        StartCoroutine(SpawnAfterDelay(rightPath, 5, 1, shooterPrefab, 2f, 10f));
        StartCoroutine(SpawnAfterDelay(midPath, 10, 3, runnerPrefab, 1f, 15f));
        break;
      case 4:
      case 5:
      case 6:
      case 7:
      case 8:
      case 9:
      case 10:
        break;
      default:
        //Some kind of scaling, maybe randomized distribution

        break;
    }
  }

}
