﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseColorOnHookScript : MonoBehaviour
{

    [Header("Target Object")]
    public SpriteRenderer[] TargetSpriteRenderer;
    public List<GameObject> ObjectsToTurnOff;
	public List<GameObject> ObjectsToChangeLayer;
	public string NewLayer;

    public bool ShouldSetThisToUnseen = true;
    public CameraShake cameraShake;



    [Header("Layer Info")]
    public LayerMask HookLayer;
    public int UnseenLayer;

    [Header("Animation Parameters")]
    public float AnimationDurationInSeconds;

    private Color _color;
    private float _colorAnimationTime;
    private BoxCollider2D _collider;
    private bool _hasStartedColorAnimation;
    private bool _hasEndedColorAnimation;

    // Use this for initialization
    void Start()
    {

        this._collider = this.GetComponent<BoxCollider2D>();
        this._hasStartedColorAnimation = false;
        this._hasEndedColorAnimation = false;

        //Seta automaticamente cameraShake
        if (cameraShake == null)
        {
            cameraShake = GameObject.Find("Camera").GetComponent<CameraShake>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (this._hasStartedColorAnimation == false && this._hasEndedColorAnimation == false)
        {
            this.beforeAnimation();
        }
        else if (this._hasStartedColorAnimation == true && this._hasEndedColorAnimation == false)
        {
            this.duringAnimation();
        }
        else if (this._hasStartedColorAnimation == true && this._hasEndedColorAnimation == true)
        {
            this.afterAnimation();
        }


    }



    private void duringAnimation()
    {
        this._colorAnimationTime += Time.deltaTime / this.AnimationDurationInSeconds;
        if (this._colorAnimationTime >= 1.0f)
        {
            this.onAnimationFinish();
        }
        foreach (var item in this.TargetSpriteRenderer)
        {
            item.color = new Color(1.0f, 1.0f, 1.0f) * this._colorAnimationTime + 
                this._color * (1.0f - this._colorAnimationTime);
        }
        
    }

    private void afterAnimation()
    {
    }

    private void beforeAnimation()
    {
        if (this._collider.IsTouchingLayers(this.HookLayer))
        {
            this.onHookHit();

        }
    }

    private void onHookHit()
    {

        this._hasStartedColorAnimation = true;
        this._colorAnimationTime = 0.0f;
        foreach (var item in this.TargetSpriteRenderer)
        {
            this._color = item.color;
        }
        StartCoroutine(cameraShake.Shake(.15f, .4f));




        //Som
        if (transform.parent.tag == "Crystal")
        {
            LevelManager.Instance.CrystalAbsorbedSound.PlayDelayed(LevelManager.Instance.CrystalAbsorbedSoundDelay);
        }
    }

    private void onAnimationFinish()
    {

        this._colorAnimationTime = 1.0f;
        this._hasStartedColorAnimation = true;
        this._hasEndedColorAnimation = true;
        if (this.ShouldSetThisToUnseen)
        {
            this.transform.gameObject.layer = UnseenLayer;
        }
        else
        {
            this.transform.gameObject.layer = 0;
        }
		foreach (GameObject gameObj in ObjectsToChangeLayer) {
			gameObj.layer = LayerMask.NameToLayer(NewLayer);
		}
        foreach (GameObject gameObj in this.ObjectsToTurnOff)
        {
            gameObj.SetActive(false);
        }


    }


}
