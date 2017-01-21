using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour {

  public bool bDebug = false;
  public bool bFollowMouse = true;

  public Transform moveTarget;

  public float speed = 1;

  public float boidsDetection = 5;
  public float boidsDesiredDistance = 1;
  public float boidsSeparateForce = 2;
  public float boidsMagnitude = 0.5f;
  public float boidsDelay = 0.25f;
  private float boidsDelayRemaining = 0.25f;


  public float jitterAmount = 0.01f;

  [Header("Debug stuff")]
  public Vector3 boidsDebug;
  public Vector3 jitterDebug;



  // Use this for initialization
  void Start () {
    moveTarget = MouseStuff.instance.transform;

  }



  // Update is called once per frame
  void Update () {
    //Move towards the mouse
    Vector2 newPosition = new Vector2();

    //Cheese nonphysics!
    if(bFollowMouse)
      newPosition = Vector3.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
    else
    {
      newPosition = transform.position;
    }

    //Jitter a little
    Vector2 jitter = (Random.insideUnitCircle * jitterAmount * Time.deltaTime);
     
    //Boids stuff?		

    Vector2 boids = new Vector2();

    //boidsDelayRemaining -= Time.deltaTime;


    //if (boidsDelayRemaining <= 0)
    if(Random.value > 0.5f)
    {
      Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, boidsDetection);
      foreach (Collider2D c in colls)
      {
        if (c.gameObject != gameObject)
        {
          Vector2 diff = c.transform.position - transform.position;
          float mag = diff.magnitude;
          if (mag > boidsDesiredDistance)
          {
            //It's outside desired range, so we move towards it
            boids += diff;

          }
          else
          {
            Vector2 runaway;
            runaway = -diff.normalized;
            runaway *= boidsSeparateForce * (1 - mag);
            boids += runaway;
          }
        }

      }

      Debug.Log(boids);

      boids.Normalize();
      boids *= boidsMagnitude * Time.deltaTime;
      boidsDelayRemaining = boidsDelay + Random.Range(-0.1f, 0.1f);
    }


    newPosition += jitter;
    newPosition += boids;

    boidsDebug = boids;
    jitterDebug = jitter;

    

    transform.position = newPosition;

    Debug.Log("Test test test");


  }
}
