using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed_ = 0.1f;
	public int damage_ = 10;
	public bool onTouchKill;

	private Vector3 target_;
	private Vector3 lastPosition;

	// Use this for initialization
	public void Init (Vector3 target) {
		this.target_ = target;
	}

	// Update is called once per frame
	void Update () {
		float step = speed_ * Time.deltaTime;
		this.transform.position = Vector2.MoveTowards (transform.position, target_, step);
		if (lastPosition == this.transform.position)
			StartCoroutine(DestroyMe ());

		lastPosition = this.transform.position;
	}

	public IEnumerator DestroyMe()
	{
		//yield return StartCoroutine(FadeOut (this.transform.GetChild (0).GetComponent<SpriteRenderer> ()));
		yield return StartCoroutine(FadeTo(0f, 1f));
		Destroy (this.gameObject, 1f);
	}

	public IEnumerator FadeIn(SpriteRenderer Fade){
		Fade.gameObject.SetActive(true);
		float time = 1f;
		yield return new WaitForEndOfFrame ();
		Fade.color = new Color (Fade.color.r, Fade.color.g, Fade.color.b, 0f);
		Color startColor = Fade.color;	
		Color endColor = new Color (startColor.r, startColor.g, startColor.b, 1f);
		yield return null;

		for (float t = 0f; t <= time; t += Time.deltaTime) {
			Color temp = Color.Lerp (startColor, endColor, t/time);	
			Fade.color = temp;
			yield return new WaitForEndOfFrame();
		}
		Fade.color = endColor;
		yield return null;
	}

	public IEnumerator FadeOut(SpriteRenderer Fade){
		Debug.Log ("FadeOut at "+Fade.gameObject.name);
		float time = 1.2f;
		yield return new WaitForEndOfFrame();
		Fade.color = new Color(Fade.color.r, Fade.color.g, Fade.color.b, 1f);
		Color startColor = Fade.color;	
		Color endColor = new Color (startColor.r, startColor.g, startColor.b, 0f);
		yield return null;

		for (float t = 0f; t <= time; t += Time.deltaTime) {
			Color temp = Color.Lerp (startColor, endColor, t/time);
			Fade.color = temp;	
			yield return new WaitForEndOfFrame();
		}	
		Fade.color = endColor;
		yield return null;
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = transform.GetChild(0).GetComponent<SpriteRenderer>().color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColor;
			yield return null;
		}
	}

	public void OnTriggerEnter2D(Collider2D colli)
	{
		if (colli.tag != "Alien")
			return;

		//colli.gameObject.GetComponent<EnemyMovement> ().TakeHit (damage_);

		if (onTouchKill)
			Destroy (this.gameObject);
	}
}
