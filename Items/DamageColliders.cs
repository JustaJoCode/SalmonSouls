using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColliders : MonoBehaviour
{
    Collider damagecollider;

    public int currentWeaponDamage = 12;

    private void Awake()
    {
        damagecollider = GetComponent<Collider>();
        damagecollider.gameObject.SetActive(true);
        damagecollider.isTrigger = true;
        damagecollider.enabled = false;
    }

    public void EnableDamageCollider()
    {
        damagecollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damagecollider.enabled = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            PlayerStats playerStats = collision.GetComponent<PlayerStats>();
            
             
            if(playerStats !=null)
            {
                playerStats.TakeDamage(currentWeaponDamage);
            }

        }
        if (collision.tag == "Enemy")
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(currentWeaponDamage);
            }
        }
    }

}
