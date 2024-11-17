using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : AnimatorManager
{
    
    PlayerManager playerManager;
    PlayerStats playerStats;
    public InputHandler inputHandler;
    public PlayerLocomotion playerLocomotion;
    int vertical;
    int horizontal;
   

    public void Initialize()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponentInParent<InputHandler>();
        playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        playerStats = GetComponentInParent<PlayerStats>();
        playerManager = GetComponentInParent<PlayerManager>();
        vertical = Animator.StringToHash("vertical");
        horizontal = Animator.StringToHash("horizontal");
    }
    public void UpdateAnimatorvalues(float verticalMovement, float horizontalMovement, bool isSprinting)
    {
        float v = 0;

        if (verticalMovement > 0 && verticalMovement < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalMovement >0.55f)
        {
            v = 1;
        }
        else if (verticalMovement <0 && verticalMovement > -.55f)
        {
            v = -.05f;
        }
        else if (verticalMovement < -0.55f)
        {
            v = -1f;
        }
        else
        {
            v = 0;
        }

        float h = 0;

        if (horizontalMovement > 0 && horizontalMovement < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalMovement > 0.55f)
        {
            h = 1;
        }
        else if (horizontalMovement < 0 && horizontalMovement > -.55f)
        {
            h = -.05f;
        }
        else if (horizontalMovement < -0.55f)
        {
            h = -1f;
        }
        else
        {
            h = 0;
        }

        if (isSprinting)
        {
            v = 2;
            h = horizontalMovement;
        }
        anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
        anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
    }

    private void OnAnimatorMove()
    {
        if (playerManager.isInteracting == false)
            return;

        float delta = Time.deltaTime;
        playerLocomotion.rigidbody.drag = 0;

        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;

        Vector3 velocity = deltaPosition / delta;
        playerLocomotion.rigidbody.velocity = velocity;
    }

    public void CanRotate()
    {
        anim.SetBool("canRotate", true);
    }

    public void StopRotation()
    {
        anim.SetBool("canRotate", false);
    }

    public void EnableCombo()
    {
        anim.SetBool("canDoCombo", true);
    }

    public void DisableCombo()
    {
        anim.SetBool("canDoCombo", false);
    }

    public void EnableIsInvulnerable()
    {
        anim.SetBool("isInvulnerable", true);
    }

    public void DisableIsInvulnerable()
    {
        anim.SetBool("isInvulnerable", false );
    }

    public override void TakeCriticalDamageAnimationEvent()
    {
        playerStats.TakeDamageNoAnims(playerManager.pendingCriticalDamage);
        playerManager.pendingCriticalDamage = 0;
    }
    public void DisableCollisons()
    {
        playerLocomotion.characterCollider.enabled = false;
        playerLocomotion.characterCollisonBlockerCollider.enabled = false;
    }

    public void EnableCollisons()
    {
        playerLocomotion.characterCollider.enabled = true;
        playerLocomotion.characterCollisonBlockerCollider.enabled = true;
    }
}
