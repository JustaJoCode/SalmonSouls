using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
  
    EnemyLocomotion enemyLocomotion;
    EnemyAnimationManager enemyAnimationManager;
    EnemyStats enemyStats;

    public bool isPreformingAction;
    public bool isInteracting;

    public State currentState;
    public CharacterStats currentTarget;
    public NavMeshAgent navmeshAgent;
    public Rigidbody enemyRigidBody;

    
    public float rotationSpeed = 25;
    public float maximumAggroRadius = 1.5f;

    [Header("Combo Flags")]
    public bool canDoCombo;

    [Header("Ai Settings")]
    public float detectionRadius = 20;
    // The highet, and lower these angles are the greater detection FOV
    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float currentRecoveryTime = 0;

    [Header("AI Combat Settings")]
    public bool allowAIToPreformCombos;
    public float comboLikelyHood; 

    private void Awake()
    {
        enemyLocomotion = GetComponent<EnemyLocomotion>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
        enemyStats = GetComponent<EnemyStats>();
        navmeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
        backStabCollider = GetComponentInChildren<CritDamageCollider>();
        navmeshAgent.enabled = false;
    
    }

    private void Start()
    {
        enemyRigidBody.isKinematic = false;
    }

    private void Update()
    {
        HandleRecoveryTime();
        HandleStateMachine();

        isRotatingWithRootMotion = enemyAnimationManager.anim.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimationManager.anim.GetBool("isInteracting");
        canRotate = enemyAnimationManager.anim.GetBool("canRotate");
        canDoCombo = enemyAnimationManager.anim.GetBool("canDoCombo");
        enemyAnimationManager.anim.SetBool("isDead", enemyStats.isDead);
    }

    private void LateUpdate()
    {
        navmeshAgent.transform.localPosition = Vector3.zero;
        navmeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleStateMachine()
    {

        if (currentState != null)
        {   
            State nextState = currentState.Tick(this, enemyStats, enemyAnimationManager);

            if (nextState != null)
            {
                SwitchToNextState(nextState);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }

    private void HandleRecoveryTime()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;

        }

        if (isPreformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPreformingAction = false;
            }

        }
    }

   
}




