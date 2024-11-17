using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : MonoBehaviour
{
    PlayerStats playerStats;
    WeaponSlotManager weaponSlotManager;

    public GameObject currentParticleFX; // The particle effects... duh
    public GameObject instantiatedEffectsModel;

    public int amountToHeal;


    private void Awake()
    {
        playerStats = GetComponentInParent<PlayerStats>();
        weaponSlotManager = GetComponent<WeaponSlotManager>();
    }


    public void HealPlayerFromEffect()
    {
        playerStats.HealPlayer(amountToHeal);

        GameObject healeffect = Instantiate(currentParticleFX, playerStats.transform);

        Destroy(instantiatedEffectsModel.gameObject);

        weaponSlotManager.LoadBothWeaponsOnSlots(); 
    }
}
