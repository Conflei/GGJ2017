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
	public int hp_;

	// Use this for initialization

	public Defense ()
	{}

	public void Init (typeOfDefense type) {
		this.myType_ = type;

		if (this.myType_ == typeOfDefense.Ivy) 
			this.hp_ = 1;
		if (this.myType_ == typeOfDefense.Venus) 
			this.hp_ = 50;
		if (this.myType_ == typeOfDefense.Turret) 
			this.hp_ = 20;
		
	}

	public void TakeHit(int damage)
	{
		hp_ -= damage;

		//UI DAMAGE REFRESH

		if (hp_ <= 0)
			Die ();
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
