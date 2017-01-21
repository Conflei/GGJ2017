using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  //When an enemy reaches the flower box, it latches on to a flower and destroys it

  public Flower FindFlower()
  {
    return GetComponentInChildren<Flower>();
  }

}
