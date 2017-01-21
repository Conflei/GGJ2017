using   System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

  [Header("Not part of the path, but cannot build towers here")]
  public bool Blocked = false;
  [Header("Part of the path - cannot build towers, but can build traps etc.")]
  public bool Path = false;
  [Header("True if something has been built on this location")]
  public bool Occupied = false;

	public  Vector3 tilePosition_ { get; set;}

  //Reference to the tower or trap on this tile?

	// Use this for initialization
	void Start () {
		tilePosition_ = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnDrawGizmos()
  {
    if (Blocked)
    {
      Gizmos.color = Color.red;
      Gizmos.DrawCube(transform.position, new Vector3(0.08f, 0.08f, 0.08f));
    }
    else if (Path)
    {
      Gizmos.color = Color.green;
      Gizmos.DrawCube(transform.position, new Vector3(0.08f, 0.08f, 0.08f));
    }
    else if (Occupied)
    {
      Gizmos.color = Color.yellow;
      Gizmos.DrawCube(transform.position, new Vector3(0.08f, 0.08f, 0.08f));
    }
    else
    {
      Gizmos.color = Color.blue;
      Gizmos.DrawCube(transform.position, new Vector3(0.08f, 0.08f, 0.08f));
    }


    }

	public void ChangeState(bool occupied)
	{
		this.Occupied = occupied;
		GameController.Instance.FreeTileClicked (this);
	}

}
