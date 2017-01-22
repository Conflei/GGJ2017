using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Defense: MonoBehaviour {

	public enum typeOfDefense
	{
		Turret,
		Ivy,
		Venus
	}

	public Slider hpSlider;

	public SpriteRenderer mobSprite;
	public typeOfDefense myType_;
	public int maxHP_;
	public int hp_;

	// Use this for initialization

	public Defense ()
	{}

	public void Init (typeOfDefense type) {
		this.myType_ = type;
		hpSlider.gameObject.SetActive (false);
		this.hp_ = maxHP_;
	}

	public void TakeHit(int damage)
	{
		hp_ -= damage;
		hpSlider.value = maxHP_ / hp_;

		//UI DAMAGE REFRESH

		if (hp_ <= 0)
			Die ();
	}

	public void Update()
	{
		if (hpSlider.value < 1f) {
			hpSlider.gameObject.SetActive (true);
		}
	}

	public void Die()
	{
		StartCoroutine (FadeTo (0f, 1f));
	}

	IEnumerator FadeTo(float aValue, float aTime)
	{
		float alpha = mobSprite.color.a;
		for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
		{
			Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
			mobSprite.color = newColor;
			yield return null;
		}
	}

}
