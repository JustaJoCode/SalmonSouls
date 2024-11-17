using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables")]

public class FlaskItem : ConsumableItem
{
    [Header("Pellet Type")]
    public bool SPellets;

    [Header("Recovery Amount")]
    public int healthRecoveryAmount;

    [Header("RecoveryFX")]
    public GameObject recoveryFX;

    public override void ConsumableItemAttempt(PlayerAnimatorManager playerAnimatorManager,WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.ConsumableItemAttempt(playerAnimatorManager, weaponSlotManager, playerEffectsManager);

        GameObject pillbottle = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);

        playerEffectsManager.currentParticleFX = recoveryFX;

        playerEffectsManager.amountToHeal = healthRecoveryAmount;

        playerEffectsManager.instantiatedEffectsModel = pillbottle;

        weaponSlotManager.rightHandSlot.UnloadWeapon();
       
    }
}
