using UnityEngine;
using UnityEngine.EventSystems;

public class CustomEventTrigger : EventTrigger
{
	public override void OnPointerDown( PointerEventData data )
	{
		Debug.Log( "OnPointerDown called." +data.position);
		this.gameObject.GetComponent<GameUI> ().ScreenClickDown (data.position);
	}

	public override void OnPointerUp( PointerEventData data )
	{
		//Debug.Log( "OnPointerUp called." );
	}
}
