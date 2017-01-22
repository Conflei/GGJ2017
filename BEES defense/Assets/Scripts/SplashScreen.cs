using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

	[SerializeField] private Image FadeScreen;

	// Use this for initialization
	void Start () {
		FadeScreen.canvasRenderer.SetAlpha (0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Space)) {
			
		}
	}

	public void CheckBees()
	{
		StartCoroutine (CheckBeesWorker ());
	}

	public IEnumerator CheckBeesWorker()
	{
		yield return new WaitForSeconds (1f);
		GameObject bee = GameObject.FindGameObjectWithTag ("Bees");
		Debug.Log ("Checking Bees");
		if (bee == null) {
			Debug.Log ("No Bees");
			FadeScreen.CrossFadeAlpha (1f, 1f, true);

			//begin scene
			UnityEngine.SceneManagement.SceneManager.LoadScene(1);
		} else {
			Debug.Log ("Still Bees");
		}



		yield return null;
	}
}
