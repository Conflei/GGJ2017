using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBoxManager : MonoBehaviour {

  public bool gameOver = false;
  public float timeCheck = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    timeCheck -= Time.deltaTime;
    if(timeCheck <= 0)
    {
      //Check to see if any flowers remain
      Flower f = GetComponentInChildren<Flower>();
      if (!f)
      {
        Debug.Log("GAME OVER!!!");
        gameOver = true;
      }
      timeCheck = 10f;
    }
  }

  public void FlowerDestroyed()
  {
    timeCheck = 2;
  }
}
