using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseStuff : MonoBehaviour {

  public static MouseStuff instance;

  public Vector3 position;

	// Use this for initialization
	void Start () {
    Debug.Log("MouseStuff.Start");		
	}

  void OnEnable()
  {

    if(instance && instance != this)
    {
      Debug.Log("Whops, duplicate target position");
      this.enabled = false;
    }
    else
    {
      instance = this;
    }
  }
	
	// Update is called once per frame
	void Update () {
    Vector3 mousePosition = Input.mousePosition;
    //Debug.Log(mousePosition);
    Vector3 newPos = Camera.main.ScreenToWorldPoint(mousePosition);
    newPos.z = 0;
    position = newPos;
    transform.position = position;




    if(Input.GetMouseButtonDown(0))
    {
      Collider2D coll = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Tiles"));
      //There should only be one...
      if(coll)
      {
        coll.GetComponent<SpriteRenderer>().color = Color.blue;
      }
    }
	}
}
