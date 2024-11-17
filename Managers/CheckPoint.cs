using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerAnimatorManager animatorHandler;
    PlayerUIPopUpManager playerUIPopUpManager;
    PlayerStats playerStats;

    [SerializeField] GameObject player;
    [SerializeField] List<GameObject> checkpoint;
    [SerializeField] Vector3 vectorPoint;

    private void Awake()
    {
        
        animatorHandler = FindObjectOfType<PlayerAnimatorManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        playerUIPopUpManager = FindObjectOfType<PlayerUIPopUpManager>();
    }


    public void Respawn()
    {
        if (playerStats.currentHealth <= 0)
        {
            player.transform.position = vectorPoint;
            playerStats.currentHealth = playerStats.maxHealth;
            animatorHandler.PlayTargetAnimation("EnemyWake", true);
            playerUIPopUpManager.youDiedPopUpGameObject.SetActive(false);
            playerStats.isDead = false;
            playerStats.healthbar.SetMaxHealth(playerStats.maxHealth);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        vectorPoint = player.transform.position;
      
    }

}
