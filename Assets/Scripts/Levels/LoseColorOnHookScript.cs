using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColorOnHookScript : MonoBehaviour {

	[Header("Target Object")]
	public SpriteRenderer TargetSpriteRenderer;

	[Header("Hook Info")]
  public LayerMask HookLayer;

	[Header("Animation Parameters")]
	public float AnimationDurationInSeconds;

	private Color _color;
	private float _colorAnimationTime;
	private BoxCollider2D _collider;
	private bool _hasStartedColorAnimation;
	private bool _hasEndedColorAnimation;

	// Use this for initialization
	void Start () {
		this._collider = this.GetComponent<BoxCollider2D>();
		this._hasStartedColorAnimation = false;
		this._hasEndedColorAnimation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (this._hasStartedColorAnimation == false && this._hasEndedColorAnimation == false) {
			this.beforeAnimation();
		} else if(this._hasStartedColorAnimation == true && this._hasEndedColorAnimation == false) {
			this.duringAnimation();
		} else if(this._hasStartedColorAnimation == true && this._hasEndedColorAnimation == true) {
			this.afterAnimation();
		}
	}

	private void duringAnimation() {
		this._colorAnimationTime += Time.deltaTime / this.AnimationDurationInSeconds;
		if(this._colorAnimationTime >= 1.0f) {
			this._colorAnimationTime = 1.0f;
			this._hasStartedColorAnimation = true;
			this._hasEndedColorAnimation = true;
		}
		this.TargetSpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f) * this._colorAnimationTime +
																			this._color * (1.0f - this._colorAnimationTime);
	}

	private void afterAnimation() {
	}

	private void beforeAnimation() {
		if(this._collider.IsTouchingLayers(this.HookLayer)) {
			this._hasStartedColorAnimation = true;
			this._colorAnimationTime = 0.0f;
			this._color = this.TargetSpriteRenderer.color;
		}
	}
}
