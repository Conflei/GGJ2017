using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : Singleton<GameUI> {

	[Header ("Dynamic Menu")]
	[SerializeField] private GameObject dynamicMenu_;
	[SerializeField] private Text honeyCountText_;
	private int honeyCount;
	[SerializeField] private Text timerText_;

	[SerializeField] private Text wavesCountText_;
	private int wavesCount;

	[SerializeField] private Text beeCountText_;
	private int beeCount;
	[SerializeField] private float scaleTime_;

	private bool openedMenu_;


	void Awake()
	{
		dynamicMenu_.transform.localScale = Vector3.zero;
		honeyCountText_.text = "Bee x " + honeyCount;
		wavesCountText_.text = "Wave: " + wavesCount;
		openedMenu_ = false;
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (BeginTime ());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator BeginTime()
	{
		int minutes = 1;
		int seconds = 30;
	
		while (minutes > 0 && seconds > 0) {
			seconds -= 1;
			if (seconds == 0) {
				minutes--;
				seconds = 59;
			}
			timerText_.text = minutes + ":" + seconds;
			yield return new WaitForSeconds (1f);
		}
	}

	public void ScreenClickDown(Vector2 screenPos)
	{
		//click outside
		if (openedMenu_) {
			ScreenClickUp ();
			return;
		}
		dynamicMenu_.transform.position = screenPos;
		iTween.ScaleTo (dynamicMenu_, iTween.Hash ("scale", Vector3.one, "time", scaleTime_, "easeType", iTween.EaseType.easeOutExpo));
		openedMenu_ = true;
	}

	public void ScreenClickUp()
	{
		//dynamicMenu_.transform.position = screenPos;
		iTween.ScaleTo (dynamicMenu_, iTween.Hash ("scale", Vector3.zero, "time", scaleTime_/2f, "easeType", iTween.EaseType.easeInExpo));
		openedMenu_ = false;
	}

	public void SetHoneyCount(int count)
	{
		honeyCountText_.text = count + "";
	}

	public void IncreaseWave()
	{
		wavesCount++;
		wavesCountText_.text = "Wave: " + wavesCount;
	}
}
