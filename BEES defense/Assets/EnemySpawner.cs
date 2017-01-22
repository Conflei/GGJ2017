using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner> {

  public enum EnemyType { Runner, Shooter }
  public enum Path { Left, Middle, Right, Random}

  [System.Serializable]
  public class EnemyGroup
  {
    [Tooltip("Type of enemy in this group")]
    public EnemyType enemy = EnemyType.Runner;
    [Tooltip("Total number of enemies in this group")]
    public int enemyCount = 5;
    [Tooltip("Number of enemies to spawn every (cycle time) seconds")]
    public int clusterSize = 1;
    [Tooltip("Delay between clusters of enemies")]
    public float cycleTime = 1;
    [Tooltip("Delay after wave starts for this group of enemies to begin spawning")]
    public float delay = 5;
    [Tooltip("Which path this group should follow")]
    public Path path = Path.Middle;
  }

  [System.Serializable]
  public class EnemyWave
  {
    public System.Collections.Generic.List<EnemyGroup> groups;
  }

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

  public System.Collections.Generic.List<EnemyWave> waves;

  // Use this for initialization
	void Start () {
     //SpawnNextWave();
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
		if (GameController.Instance.onDay)
				 break;
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
		if (GameController.Instance.onDay)
					yield break;
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
  
  public void SpawnWave(EnemyWave wave, int Multiplier)
  {

    for(int i = 0; i < Multiplier; i++)
    {
      foreach (EnemyGroup group in wave.groups)
      {
        EnemyMovement prefab;
        if (group.enemy == EnemyType.Runner)
        {
          prefab = runnerPrefab;
        }
        else
        {
          prefab = shooterPrefab;
        }

        if (group.path == Path.Random)
        {
          group.path = (Path)Random.Range(0, 3);
        }

        System.Collections.Generic.List<Vector3> path;
        if (group.path == Path.Left)
        {
          path = leftPath;
        }
        else if (group.path == Path.Middle)
        {
          path = midPath;
        }
        else //if (group.path == Path.Right)
        {
          path = rightPath;
        }

        StartCoroutine(SpawnAfterDelay(path, group.enemyCount, group.clusterSize, prefab, group.cycleTime, group.delay));
      }
    }
  }


  public void SpawnNextWave()
  {
    EnemyWave currentWave;
    int Multiplier = 1;
    if (waves.Count > waveNumber)
    {
      currentWave = waves[waveNumber];
    }
    else
    {
      Multiplier = 1 + waveNumber - waves.Count;
      currentWave = waves[waves.Count - 1];
    }

    SpawnWave(currentWave, Multiplier);


    waveNumber += 1;
  }

}
