using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : CharacterManager
{
    InputHandler inputhandler;
    Animator anim;
    CameraHandler cameraHandler;
    PlayerLocomotion playerLocomotion;
    PlayerStats playerStats;
    PlayerAnimatorManager playerAnimatorManager;

    InteractableUI interactableUI;
    public GameObject interactableUIGameObject;
    public GameObject IteminteractableGameObject;

    public bool isInteracting;

    [Header ("Flags")]
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;
    public bool canDoCombo;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isInvulnerable;

    private void Awake()
    {
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        backStabCollider = GetComponentInChildren<CritDamageCollider>();
        anim = GetComponentInChildren<Animator>();
        cameraHandler = CameraHandler.singleton;
        playerStats = GetComponent<PlayerStats>();
        inputhandler = GetComponent<InputHandler>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
        interactableUI = FindObjectOfType<InteractableUI>();
        
    }
    

    void Update()
    {
        float delta = Time.deltaTime;

        isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        isUsingRightHand = anim.GetBool("isUsingRightHand");
        isUsingLeftHand = anim.GetBool("isUsingLeftHand");
        isInvulnerable = anim.GetBool("isInvulnerable");
        playerAnimatorManager.canRotate = anim.GetBool("canRotate");
        anim.SetBool("isDead", playerStats.isDead);

   
        inputhandler.TickInput(delta);
        playerLocomotion.HandleRollingandSprinting(delta);
       
        

        CheckForInteractableObject();
    }

    private void FixedUpdate()
    {
        float delta = Time.fixedDeltaTime; 
        playerLocomotion.moveDirection.y = 0;
        playerLocomotion.HandleMovement(delta);
        playerLocomotion.HandleFalling(delta, playerLocomotion.moveDirection);
        playerLocomotion.HandleRotation(delta);

    }
    
    private void LateUpdate()
    {
        inputhandler.rollFlag = false;
        inputhandler.rt_Input = false;
        inputhandler.rb_Input = false;
        inputhandler.d_Pad_Up = false;
        inputhandler.d_Pad_Down = false;
        inputhandler.d_Pad_Right = false;
        inputhandler.d_Pad_Left = false;
        inputhandler.a_Input = false;


        inputhandler.inventory_Input = false;

        float delta = Time.fixedDeltaTime;

        if (cameraHandler != null)
        {
           cameraHandler.FollowTarget(delta);
           cameraHandler.HandleCamRotation(delta, inputhandler.mouseY, inputhandler.mouseX);
        }

        if (isInAir)
        {
            playerLocomotion.inAirTimer = playerLocomotion.inAirTimer + Time.deltaTime;
        }

    }

    public void CheckForInteractableObject()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.3f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactables interactableObject = hit.collider.GetComponent<Interactables>();

                if (interactableObject != null)
                {
                    string interactableText = interactableObject.interactabletext;
                    interactableUI.interactableText.text = interactableText;
                    interactableUIGameObject.SetActive(true);


                    if (inputhandler.a_Input)
                    {
                        hit.collider.GetComponent<Interactables>().Interact(this);
                    }


                }

            }

        }
        else
        {
            if(interactableUIGameObject != null)
            {
                interactableUIGameObject.SetActive(false);
            }
            if (IteminteractableGameObject != null && inputhandler.a_Input)
            {
                IteminteractableGameObject.SetActive(false);
            }
        }
    }

    public void PassThroughFOgWallInteraction(Transform fogWallEntrance)
    {
        playerLocomotion.rigidbody.velocity = Vector3.zero;

        Vector3 rotationDirection = fogWallEntrance.transform.forward;
        Quaternion turnRotation = Quaternion.LookRotation(rotationDirection);
        transform.rotation = turnRotation;

        playerAnimatorManager.PlayTargetAnimation("Pass Through Fog", true);
    }

}
