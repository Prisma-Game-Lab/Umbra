using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpScript : MonoBehaviour
{
    public Animator PlayerJumpAnim;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyBindings.Instance.PlayerJump))
            //if (_playerController.CurrentAvailableJumps > 0)
                if (PlayerJumpAnim != null)
                    PlayerJumpAnim.SetTrigger("jumpPlayer");
    }
}
