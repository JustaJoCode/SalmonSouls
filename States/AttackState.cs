using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public CombatStanceState combatStanceState;
    public PersueTargetState persueTargetState;
    public RotateTowardsTargetState rotateTowardsTargetState;
    public EnemyAttackAction currentAttack;


    public bool hasPreformedAttack = false;
    bool willdoComboNextAttack = false;


    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationManager enemyAnimationManager)
    {
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        RotateTowardsTargetWhileAttacking(enemyManager);
         
        if (distanceFromTarget > enemyManager.maximumAggroRadius)
        {
            return persueTargetState;
        }

        if (willdoComboNextAttack && enemyManager.canDoCombo)
        {
            AttackTargetWithCombo(enemyAnimationManager,enemyManager);
        }

        if (!hasPreformedAttack)
        {
            AttackTarget(enemyAnimationManager, enemyManager);
            RollForComboChance(enemyManager); 
        }

        if (willdoComboNextAttack && hasPreformedAttack)
        {
            return this;
        }

        return rotateTowardsTargetState;
    }

    private void AttackTarget(EnemyAnimationManager enemyAnimationManager, EnemyManager enemyManager)
    {
        enemyAnimationManager.PlayTargetAnimationWithRootRotation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPreformedAttack = true;
    }

    private void AttackTargetWithCombo(EnemyAnimationManager enemyAnimationManager, EnemyManager enemyManager)
    {
        willdoComboNextAttack = false;
        enemyAnimationManager.PlayTargetAnimationWithRootRotation(currentAttack.actionAnimation, true);
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }

    private void RotateTowardsTargetWhileAttacking(EnemyManager enemyManager)
    {
        if (enemyManager.canRotate && enemyManager.isInteracting)
        {
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }


    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if (enemyManager.allowAIToPreformCombos && comboChance <= enemyManager.comboLikelyHood)
        {
            if (currentAttack.comboAction != null)
            {
                willdoComboNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else
            {
                willdoComboNextAttack = false;
                currentAttack = null;
            }
            

        }

    }
  

}
