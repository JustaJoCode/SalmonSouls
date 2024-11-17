using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    UIManager uIManager;
    PlayerUIPopUpManager playerUIPopUpManager;
    public HealthBar healthbar;
    StaminaBar staminabar;
    PlayerAnimatorManager animatorHandler;
    PlayerManager playerManager;
    CheckPoint checkPoint;

    private void Awake()
    {
        healthbar = FindObjectOfType<HealthBar>();
        staminabar = FindObjectOfType<StaminaBar>();
        playerUIPopUpManager = FindObjectOfType<PlayerUIPopUpManager>();
        animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        checkPoint = FindObjectOfType<CheckPoint>();
    }

    void Start()
    {
        maxHealth = SetMaxhealthFromHealthLevel();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        
        
    }

    private int SetMaxhealthFromHealthLevel()
    {
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    public void TakeDamageNoAnims(int damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

        }
    }


    public void HealPlayer(int healAmount)
    {

        currentHealth = currentHealth + healAmount;

        if (currentHealth> maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthbar.SetCurrentHealth(currentHealth);


    }

    public void TakeDamage(int damage)
    {
        if (playerManager.isInvulnerable)
            return;
       
        if (isDead)
            return;
       
        currentHealth = currentHealth - damage;
        healthbar.SetCurrentHealth(currentHealth);

        animatorHandler.PlayTargetAnimation("Damage_01", true);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animatorHandler.PlayTargetAnimation("Death_01", true);
            playerUIPopUpManager.SendYouDiedPopUp();
            isDead = true;
        }
    }

}
