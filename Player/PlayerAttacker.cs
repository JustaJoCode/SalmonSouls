using UnityEngine;

public class PlayerAttacker : MonoBehaviour
{
    PlayerAnimatorManager animatorHandler;
    InputHandler inputHandler;
    WeaponSlotManager weaponSlotManager;
    PlayerManager playerManager;
    PlayerInventory playerInventory;
    PlayerStats playerStats;

    public string lastAttack;

    public LayerMask backStabLayer = 1<< 12;

    private void Awake()
    {
        animatorHandler = GetComponentInChildren<PlayerAnimatorManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        inputHandler = GetComponent<InputHandler>();
        playerManager = GetComponent<PlayerManager>();
        playerInventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();
       
    }

    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (playerStats.currentStamina <= 0)
            return;

        if (inputHandler.comboFlag)
        {
            animatorHandler.anim.SetBool("canDoCombo", false);

            if (lastAttack == weapon.OH_Light_Attack_1)
            {
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
            }
        }
       
    }

    public void HandleLightAttack(WeaponItem weapon)
    {
        if (playerStats.currentStamina <= 0)
        return;

        weaponSlotManager.attackingWeapon = weapon;
        animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
        lastAttack = weapon.OH_Light_Attack_1;
        Debug.Log("Attack preformed");
        
    }


    public void AttemptBackStabOrReposte()
    {
        if (playerStats.currentStamina <= 0)
            return;
         
        RaycastHit hit;

        if (Physics.Raycast(inputHandler.criticalAttackRayCastStarterPoint.position, transform.TransformDirection(Vector3.forward), out hit, .5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageColliders rightWeapon = weaponSlotManager.rightHandDamageCollider;


            if (enemyCharacterManager !=null)
            {
                //Check for Team ID;
                playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;


                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.currentWeaponDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                animatorHandler.PlayTargetAnimation("Back_Stab", true);
                enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Back_Stabbed", true);
            }
        }
    }
}
