using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashscreenCheckpoint : MonoBehaviour {

	public SplashScreen splashScreenScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D (Collider2D Colli)
	{
		if (Colli.gameObject.tag != "Bees")
			return;

		StartCoroutine (FadeOut (Colli.GetComponent<SpriteRenderer> ()));
	}

	public IEnumerator FadeOut(SpriteRenderer Fade){
		float time = 1f;
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
		yield return new WaitForSeconds (1f);
		Fade.gameObject.tag = "Untagged";
		splashScreenScript.CheckBees ();
		yield return null;
	}
}
