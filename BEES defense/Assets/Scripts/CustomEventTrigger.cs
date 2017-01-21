using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventTrigger : EventTrigger
{
	public override void OnPointerDown( PointerEventData data )
	{
		Debug.Log( "OnPointerDown called." +data.position);

		MapTile myTile = null;
		Collider2D coll = Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition), LayerMask.GetMask("Tiles"));
		//There should only be one...
		if (coll) {
			Debug.Log ("There is coll and my name is " + coll.name);
			myTile = coll.GetComponent<MapTile> (); 
			//coll.GetComponent<SpriteRenderer> ().color = Color.blue;
			GameUI.Instance.ScreenClickDown (myTile);
		} else {
			Debug.Log ("Collided with no tile");
		}
			
	}

	public override void OnPointerUp( PointerEventData data )
	{
		//Debug.Log( "OnPointerUp called." );
	}
}
