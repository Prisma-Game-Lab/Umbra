using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShineOnAim : MonoBehaviour
{
    public Animator ShineAnim;
    public SpriteRenderer ShineSprite;

    private void OnMouseOver()
    {
        ShineSprite.enabled = true;
        ShineAnim.SetBool("EnemyLight", true);
    }

    private void OnMouseExit()
    {
        ShineSprite.enabled = false;
        ShineAnim.SetBool("EnemyLight", false);
    }
}
