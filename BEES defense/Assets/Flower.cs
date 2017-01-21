using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

  public int moneyAmount = 0;
  public int generateAmount = 5;

  public float generateTime = 30;
  private float generateTimeRemaining = 30;

	// Use this for initialization
	void Start () {
    generateTimeRemaining = generateTime;	
	}
	
	// Update is called once per frame
	void Update () {
    //If currently day
    generateTimeRemaining -= Time.deltaTime;
    if(generateTimeRemaining <= 0)
    {
      moneyAmount = generateAmount;
      //Particle system, or halo, or something along those lines?
      //Probably better with particle system
      GetComponent<SpriteRenderer>().color = Color.yellow;
    }
	}

  void OnTriggerEnter2D(Collider2D other)
  {

    //Could do stay I guess but that's spammy.  Enter should be fine
    BeeMovement b = other.GetComponent<BeeMovement>();
    if(moneyAmount > 0 && b)
    {
      GetComponent<SpriteRenderer>().color = Color.white;
      //Add money to the player's cash total

      moneyAmount = 0;

    }

  }

}
