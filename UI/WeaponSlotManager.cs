using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerInventory playerInventory;

    public WeaponItem attackingWeapon;
    public WeaponHoldersSlot leftHandSlot;
    public WeaponHoldersSlot rightHandSlot;

    public DamageColliders leftHandDamageCollider;
    public DamageColliders rightHandDamageCollider;


    Animator animator;
    QuickSlots quickSlots;
    InputHandler inputHandler;
    PlayerStats playerStats;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        quickSlots = FindObjectOfType<QuickSlots>();
        playerStats = GetComponentInParent<PlayerStats>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        inputHandler = GetComponentInParent<InputHandler>(); 

        WeaponHoldersSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHoldersSlot>();

        foreach(WeaponHoldersSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if(weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
        }
    }

    public void LoadBothWeaponsOnSlots()
    {
        LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        LoadWeaponOnSlot(playerInventory.leftWeapon, true);
    }

    public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (isLeft)
        {
            leftHandSlot.LoadWeaponModel(weaponItem);
            LoadLeftWeaponDamageCollider();
            quickSlots.UpdateWeaponQuickSlotsUI(true, weaponItem);

            if(weaponItem != null)
            {
                animator.CrossFade(weaponItem.Left_Arm_Idle_01, 0.2f);

            }
            else
            {
                animator.CrossFade("Left Arm Empty", 0.2f);
            }
        }

        else
        {
            rightHandSlot.LoadWeaponModel(weaponItem);
            LoadRightWeaponDamageCollider();
            quickSlots.UpdateWeaponQuickSlotsUI(false, weaponItem);

            if (weaponItem != null)
            {
                animator.CrossFade(weaponItem.Right_Arm_Idle_01, 0.2f);

            }
            else
            {
                animator.CrossFade("Right Arm Empty", 0.2f);
            }
        }
    }

    #region Handle Weapon's DamageCollider



    private void LoadLeftWeaponDamageCollider()
    {
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageColliders>();
        leftHandDamageCollider.currentWeaponDamage = playerInventory.leftWeapon.baseDamage;
    }

    private void LoadRightWeaponDamageCollider()
    {
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageColliders>();
        rightHandDamageCollider.currentWeaponDamage = playerInventory.rightWeapon.baseDamage;
    }

    public void OpenDamageCollider()
    {

        if (playerManager.isUsingRightHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        if (playerManager.isUsingLeftHand)
        {
            rightHandDamageCollider.EnableDamageCollider();
        }
        
       
    }

    
    public void CloseDamageCollider()
    {
        rightHandDamageCollider.DisableDamageCollider();
      
    }


    #endregion

}
