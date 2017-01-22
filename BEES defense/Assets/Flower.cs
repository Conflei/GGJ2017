using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour {

  public int moneyAmount = 0;
  public int generateAmount = 5;

  public float generateTime = 30;
  private float generateTimeRemaining = 30;

  private Animator am;

	// Use this for initialization
	void Start () {
    generateTimeRemaining = generateTime;
    am = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
    //If currently day
    generateTimeRemaining -= Time.deltaTime;
    if(generateTimeRemaining <= 0)
    {
      moneyAmount = generateAmount;
      am.SetTrigger("Ready");
      
      GetComponent<SpriteRenderer>().color = Color.yellow;
      generateTimeRemaining = generateTime;
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

      EconomicSystem.Instance.AddHoney(moneyAmount);
      moneyAmount = 0;
      am.SetTrigger("Idle");
      

    }

  }

  public void Die()
  {
    Destroy(gameObject, 1);
    am.SetTrigger("Die");
    GetComponent<AudioSource>().Play();
    this.enabled = false;
  }

}
