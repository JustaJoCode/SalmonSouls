using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimationManager enemyAnimationManager)
    {
        enemyAnimationManager.anim.SetFloat("vertical", 0);
        enemyAnimationManager.anim.SetFloat("horizontal", 0);

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

        if (enemyManager.isInteracting)
           return this;

        if (viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayTargetAnimationWithRootRotation("TurnBehind", true);
            return combatStanceState;

        }
        else if(viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayTargetAnimationWithRootRotation("TurnBehind", true);
            return combatStanceState;

        }
        else if (viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayTargetAnimationWithRootRotation("TurnRight", true);
            return combatStanceState;

        }
        else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting)
        {
            enemyAnimationManager.PlayTargetAnimationWithRootRotation("TurnLeft", true);
            return combatStanceState;

        }

        return combatStanceState;
    }
}
 