using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFadeIn : MonoBehaviour {

	private Text _textComponent;

	private float _currentTimeCounter;
	
	public float DelayBeforeStartingToFadeIn = 1.0f;
	public float FadeInDurationLinear = 1.0f;

	// Use this for initialization
	void Start () {
  	this._textComponent = GetComponent<Text>();
		this.setColorAlpha(0.0f);
		this._currentTimeCounter = 0.0f;
	}
	
	private void setColorAlpha(float value)
	{
		Color color = this._textComponent.color;
		color.a = value;
		this._textComponent.color = color;
	}

	// Update is called once per frame
	void Update () {
		if (this._currentTimeCounter < this.DelayBeforeStartingToFadeIn + this.FadeInDurationLinear)
		{
			this._currentTimeCounter += Time.deltaTime;
			if (this._currentTimeCounter < this.DelayBeforeStartingToFadeIn)
			{
				this.setColorAlpha(0.0f);
			}
			else
			{
				this.setColorAlpha(Mathf.Lerp(0.0f, 1.0f, (this._currentTimeCounter - this.DelayBeforeStartingToFadeIn) / this.FadeInDurationLinear));
			}
		}
		else
		{
			this.setColorAlpha(1.0f);
		}
	}
}
