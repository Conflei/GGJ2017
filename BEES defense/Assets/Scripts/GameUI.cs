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

	[SerializeField] private Image crosshair_;

	[Header ("Shake FX")]
	[SerializeField] private Camera camera;
	float shake = 0;
	float shakeAmount = 0.2f;
	float decreaseFactor = 1.0f;
	private Vector3 startingCameraPos_;

	[Header("Day & Night Things")]
	[SerializeField] public Image courtine;


	void Awake()
	{
		startingCameraPos_ = camera.transform.position;
		dynamicMenu_.transform.localScale = Vector3.zero;
		honeyCountText_.text = "Bee x " + honeyCount;
		wavesCountText_.text = "Wave: " + wavesCount;
		openedMenu_ = false;
	}

	// Use this for initialization
	void Start () {
		crosshair_.canvasRenderer.SetAlpha (0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameController.Instance.shootingMode_) {
			crosshair_.transform.position = Input.mousePosition;
		}
			
		if (shake > 0) {
			Vector3 newCamPos = new Vector3 (Random.insideUnitSphere.x * shakeAmount, Random.insideUnitSphere.y * shakeAmount, -10f);
			camera.transform.localPosition = newCamPos;
			shake -= Time.deltaTime * decreaseFactor;

		} else {
			    camera.transform.position = startingCameraPos_;
				shake = 0.0f;
		}

	}

	public IEnumerator BeginTime(int min, int sec)
	{
		int minutes = min;
		int seconds = sec;
	
		while (minutes > 0 || seconds > 0) {
			if (seconds == 0) {
				minutes--;
				seconds = 59;
			}
			timerText_.text = minutes + ":" + seconds;


			seconds -= 1;
			yield return new WaitForSeconds (1f);
		}
		GameController.Instance.TimeOver ();
	}

	public void ScreenClickDown(MapTile tileScript)
	{
		//click outside
		if (openedMenu_) {
			ScreenClickUp ();
			return;
		}

		if (GameController.Instance.occupiedHand) {
			GameController.Instance.SetObjectAt (tileScript);
			return;
		}

		if (GameController.Instance.shootingMode_) {
			GameController.Instance.ShootTo (tileScript);
			return;
		}

		if (!GameController.Instance.onDay)
			return;

		dynamicMenu_.transform.position = Camera.main.WorldToScreenPoint(tileScript.tilePosition_);
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

	public void EnterAimMode()
	{
		iTween.ScaleTo (crosshair_.gameObject, iTween.Hash ("scale", Vector3.one, "time", 1f, "easeType", iTween.EaseType.easeInExpo));
		crosshair_.CrossFadeAlpha (.99f, 1.5f, true);
	}

	public void ExitAimMode()
	{
		iTween.ScaleTo (crosshair_.gameObject, iTween.Hash ("scale", Vector3.one*30f, "time", 2f, "easeType", iTween.EaseType.easeOutExpo));
		crosshair_.CrossFadeAlpha (0f, 1f, false);
	}

	public void Shake()
	{
		shake = 1f;
	}


	//Day & Night things

	public IEnumerator ShowNight()
	{
		courtine.color = Color.black;
		courtine.canvasRenderer.SetAlpha (0.0f);
		courtine.CrossFadeAlpha (1f, 1.5f, false);
		yield return new WaitForSeconds (1.5f);

	}

	public IEnumerator ShowDay()
	{
		courtine.color = Color.white;
		courtine.canvasRenderer.SetAlpha (0.0f);
		courtine.CrossFadeAlpha (1f, 1.5f, false);
		yield return new WaitForSeconds (1.5f);
	}

	public IEnumerator HideCourtine()
	{
		courtine.CrossFadeAlpha (0f, 1f, false);
		yield return new WaitForSeconds (1f);
	}
}
