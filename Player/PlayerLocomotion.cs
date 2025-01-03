using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    CameraHandler cameraHandler;
    Transform cameraObject;
    PlayerManager playerManager;
    PlayerStats playerStats;
    InputHandler inputHandler;
    public Vector3 moveDirection;

    [HideInInspector]
    public Transform myTransform;
    public PlayerAnimatorManager animatorHandler;

    public new Rigidbody rigidbody;
    public GameObject normalCamera;

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisonBlockerCollider;

    [Header("Movement Stats")]
    [SerializeField]
    float movementSpeed = 5;
    [SerializeField]
    float sprintSpeed = 7;
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float fallSpeed = 50;

    [Header("Ground & Air Detection")]
    [SerializeField]
    float groundDetectionRayStartPoint = 0.5f;
    [SerializeField]
    float minimumDistanceNeededToBeginFall = 1f;
    [SerializeField]
    private float groundDirectionRayDistance = -0.2f;
    LayerMask ignoreForGroundCheck;
    public float inAirTimer;


    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();
        rigidbody = GetComponent<Rigidbody>();
        inputHandler = GetComponent<InputHandler>();
        animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>(); 
    }

    void Start()
    {
        cameraObject = Camera.main.transform;
        myTransform = transform;
        animatorHandler.Initialize();

        playerManager.isGrounded = true;
        ignoreForGroundCheck = ~((1 << 8) | (1 << 11));
        Physics.IgnoreCollision( characterCollider, characterCollisonBlockerCollider, true);
    }
   
    Vector3 normalVector;
    Vector3 targetPosition;

    public void HandleRotation(float delta)
    {
        if (animatorHandler.canRotate)
        {
            if (inputHandler.lockOnFlag)
            {
                if (inputHandler.sprintFlag || inputHandler.rollFlag)
                {
                    Vector3 targetDirection = Vector3.zero;
                    targetDirection = cameraHandler.cameraTransform.forward * inputHandler.vertical;
                    targetDirection = cameraHandler.cameraTransform.forward * inputHandler.horizontal;
                    targetDirection.Normalize();
                    targetDirection.y = 0;

                    if (targetDirection == Vector3.zero)
                    {
                        targetDirection = transform.forward;
                    }

                    Quaternion tr = Quaternion.LookRotation(targetDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                    transform.rotation = targetRotation;
                }
                else
                {
                    Vector3 roationDirection = moveDirection;
                    roationDirection = cameraHandler.currentLockOnTarget.position - transform.position;
                    roationDirection.y = 0;
                    roationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(roationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
                    transform.rotation = targetRotation;
                }


            }
            else
            {
                Vector3 targetDir = Vector3.zero;
                float moveOverride = inputHandler.moveAmount;

                targetDir = cameraObject.forward * inputHandler.vertical;
                targetDir += cameraObject.right * inputHandler.horizontal;

                targetDir.Normalize();
                targetDir.y = 0;

                if (targetDir == Vector3.zero)
                {
                    targetDir = myTransform.forward;
                }

                float rs = rotationSpeed;

                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

                myTransform.rotation = targetRotation;
            }

        }
    }  

    public void HandleMovement(float delta)
    {
        if (inputHandler.rollFlag)
        {
            return;
        }

        if (playerManager.isInteracting)
            return;

        moveDirection = cameraObject.forward * inputHandler.vertical;
        moveDirection += cameraObject.right * inputHandler.horizontal;
        moveDirection.Normalize();

        float speed = movementSpeed;

        if (inputHandler.sprintFlag && inputHandler.moveAmount > 0.5f)
        {
            speed = sprintSpeed;
            playerManager.isSprinting = true;
            moveDirection *= speed;
            
        }
        else
        {
            if (inputHandler.moveAmount < 0.5f)
            {
                playerManager.isSprinting = false;
            }

            moveDirection *= speed;
        }

        Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
        rigidbody.velocity = projectedVelocity;

        if (inputHandler.lockOnFlag && inputHandler.sprintFlag == false)
        {
            animatorHandler.UpdateAnimatorvalues(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);
        }
        else
        {
            animatorHandler.UpdateAnimatorvalues(inputHandler.moveAmount, 0, playerManager.isSprinting);
        }

     

    }

    public void HandleRollingandSprinting(float delta)
    {
        if (animatorHandler.anim.GetBool("isInteracting"))
            return;


        if (playerStats.currentStamina <= 0)
            return;


        if (inputHandler.rollFlag)
        {
            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;

            if (inputHandler.moveAmount > 0)
            {
                animatorHandler.PlayTargetAnimation("Rolling", true);
                moveDirection.y = 0;

                Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                myTransform.rotation = rollRotation;
            }
            
        }
        
    }

    public void HandleFalling(float delta, Vector3 moveDirection) 
    {

        playerManager.isGrounded = false;
        RaycastHit hit;
        Vector3 origin = myTransform.position;
        origin.y += groundDetectionRayStartPoint;

        if(Physics.Raycast(origin, myTransform.forward, out hit, 0.4f))
        {
            moveDirection = Vector3.zero;
        }

        if(playerManager.isInAir)
        {
            rigidbody.AddForce(-Vector3.up * fallSpeed);
            rigidbody.AddForce(moveDirection * fallSpeed / 10f);
        }

        Vector3 dir = moveDirection;
        dir.Normalize();
        origin += dir * groundDirectionRayDistance;

        targetPosition = myTransform.position;

        Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.5f, false);

        if(Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck))
        {
            normalVector = hit.normal;
            Vector3 tp = hit.point;
            playerManager.isGrounded = true;
            targetPosition.y = tp.y;

            if(playerManager.isInAir)
            {
                if(inAirTimer > 0.5f)
                {
                    Debug.Log("You were in the air for " + inAirTimer);
                    animatorHandler.PlayTargetAnimation("Landing", true);
                    inAirTimer = 0;
                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Empty", true);
                    inAirTimer = 0;
                }
                playerManager.isInAir = false; 
            }
            
        }
        else
        {
            if(playerManager.isGrounded)
            {
                playerManager.isGrounded = false;
            }

            if(playerManager.isInAir == false)
            {
                if(playerManager.isInteracting == false)
                {
                    animatorHandler.PlayTargetAnimation("Falling", true);

                }

                Vector3 vel = rigidbody.velocity;
                vel.Normalize();
                rigidbody.velocity = vel * (movementSpeed / 2);
                playerManager.isInAir = true;
            }

            if (playerManager.isInteracting || inputHandler.moveAmount>0)
            {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime/ 0.1f);

            }
            else
            {
                myTransform.position = targetPosition;
            }

            if (playerManager.isGrounded)
            {
                if(playerManager.isInteracting || inputHandler.moveAmount > 0)
                {
                    myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime);
                }
                else
                {
                    myTransform.position = targetPosition;
                }
            }

        }
    }
}
