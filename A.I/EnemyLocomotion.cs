using UnityEngine;

public class EnemyLocomotion : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimationManager enemyAnimationManager;
    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisonBlockerCollider;

    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimationManager>();
    }
    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisonBlockerCollider, true);
    }


}
