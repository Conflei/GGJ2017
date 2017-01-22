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

  public bool bUseFlockingSineWave = false;
  public float SineInput;
  public float SineFrequencyMult = 1;
  public float SineValue;
  public float SineMult = 1;


  public float jitterAmount = 0.01f;

  public bool bFacingLeft = false;

  [Header("Debug stuff")]
  public Vector3 boidsDebug;
  public Vector3 jitterDebug;


  private SpriteRenderer sr;
  private Animator am;

  // Use this for initialization
  void Start () {
    moveTarget = MouseStuff.instance.transform;
    sr = GetComponent<SpriteRenderer>();
    am = GetComponent<Animator>();
    //TODO: This isn't working the way I want - Want to offset the animations so they're not all synchronized
    am.SetTime(Random.value);
  }



  // Update is called once per frame
  void Update () {
    //Move towards the mouse
    Vector2 newPosition = new Vector2();

    //Cheese nonphysics!
    if(bFollowMouse)
      if((moveTarget.position - transform.position).magnitude > boidsDesiredDistance)
        newPosition = Vector3.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
    else
    {
      newPosition = transform.position;
    }

    //Jitter a little
    Vector2 jitter = (Random.insideUnitCircle * jitterAmount * Time.deltaTime);
     

    //Boids! Well, not really, but some kind of flocking behavior
    Vector2 boids = new Vector2();

    //Sine wave added to the boids calculations
    float tempDesiredDistance = 1;

    if(bUseFlockingSineWave)
    {
      SineInput += Time.deltaTime * SineFrequencyMult;
      SineValue = Mathf.Sin(SineInput);
      tempDesiredDistance = boidsDesiredDistance + SineValue * SineMult;
    }

    if(Random.value > 0.5f)
    {
      float runawayMult = Random.value;
      Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position, boidsDetection, LayerMask.GetMask("Bees"));
      foreach (Collider2D c in colls)
      {
        if (c.gameObject != gameObject)
        {
          Vector2 diff = c.transform.position - transform.position;
          float mag = diff.magnitude;
          if (mag > tempDesiredDistance)
          {
            diff.Normalize();
            diff *= mag - tempDesiredDistance;
            //It's outside desired range, so we move towards it
            boids += diff;

          }
          else
          {
            
            Vector2 runaway;
            runaway = -diff.normalized;
            runaway *= boidsSeparateForce * (tempDesiredDistance - mag);
            //I want a few to hang out more in the center, so lets put a random onto here...
            //runaway *= runawayMult;
            if(runawayMult > 0.5f)
              boids += runaway;

          }
        }
      }
      
      boids.Normalize();
      boids *= boidsMagnitude * Time.deltaTime;
      boidsDelayRemaining = boidsDelay + Random.Range(-0.1f, 0.1f);
    }

    newPosition += jitter;
    newPosition += boids;

    boidsDebug = boids;
    jitterDebug = jitter;

    newPosition.x = Mathf.Clamp(newPosition.x, -5.76f, 5.76f);
    newPosition.y = Mathf.Clamp(newPosition.y, -4.48f, 4.48f);

    if ((newPosition - (Vector2)transform.position).x > 0)
    {
      if (!bFacingLeft)
        sr.flipX = false;
      else
        bFacingLeft = false;
    }
    else
    {
      if (bFacingLeft)
        sr.flipX = true;
      else
        bFacingLeft = true;
    }

    transform.position = newPosition;
    

  }
}
