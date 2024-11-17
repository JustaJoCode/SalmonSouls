using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public UIEnemyHealthBar enemyHealthBar;

    public GameObject deathFX;
    public GameObject instantiatedEffectsModel;

    CameraHandler cameraHandler;
    InputHandler inputHandler;
    MiniBossDrops miniBossDrops;

    EnemyManager enemyManager;
    Animator animator;
    EnemyBossManager enemyBossManager;
    CharacterManager characterManager;

    public bool isBoss;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemyManager = GetComponent<EnemyManager>();
        characterManager = GetComponent<CharacterManager>();
        enemyBossManager = GetComponent<EnemyBossManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = FindObjectOfType<InputHandler>();
        miniBossDrops = GetComponent<MiniBossDrops>();
        maxHealth = SetMaxhealthFromHealthLevel();
        currentHealth = maxHealth;

    }

    void Start()
    {
        if (!isBoss)
        {
            enemyHealthBar.SetMaxHealth(maxHealth);
        }


    }

    private int SetMaxhealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnims(int damage)
    {
        currentHealth = currentHealth - damage;

        enemyHealthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

        if (!isBoss)
        {
            enemyHealthBar.SetHealth(currentHealth);
        }
        else if (isBoss && enemyBossManager != null)
        {
            enemyBossManager.UpdateBossHealth(currentHealth);

        }

        currentHealth = currentHealth - damage;

        animator.Play("Damage_01");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Death_01");
            isDead = true;
            cameraHandler.ClearLockOnTargets();
            miniBossDrops.DropCoin();
            inputHandler.lockOnFlag = false;
            enemyManager.currentState = null;
            Destroy(gameObject, 2f);
            GameObject deatheffects = Instantiate(deathFX, enemyHealthBar.transform);
            Destroy(instantiatedEffectsModel.gameObject);
        }
    }

}