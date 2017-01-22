using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

  public Text waveReached;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void buttonClick()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("Splashscreen");
  }
}
