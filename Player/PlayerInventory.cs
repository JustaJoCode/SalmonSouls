using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    WeaponSlotManager weaponSlotManager;
     
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;
    public WeaponItem unarmedWeapon;
    public ConsumableItem currentConsumable;

    public WeaponItem[] weaponInRightHandSlot = new WeaponItem[1];
    public WeaponItem[] weaponInLeftHandSlot = new WeaponItem[1];

    public int currentRightWeaponIndex = 0;
    public int currentLeftWeaponIndex = 0;

    public List<WeaponItem> weaponsInventory;

    private void Awake()
    {
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
        
    }

    private void Start()
    {
        weaponSlotManager.LoadWeaponOnSlot(rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(leftWeapon, false);

    }

    public void ChangeRightWeapon()
    {
        currentRightWeaponIndex = currentRightWeaponIndex + 1;

        if (currentRightWeaponIndex == 0 && weaponInRightHandSlot[0] !=null)
        {
            rightWeapon = weaponInRightHandSlot[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlot[currentRightWeaponIndex], false);
        }
        else if(currentRightWeaponIndex == 0 && weaponInRightHandSlot[0]== null)
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
        }

        if (currentRightWeaponIndex == 1 && weaponInRightHandSlot[1] != null)
        {
            rightWeapon = weaponInRightHandSlot[currentRightWeaponIndex];
            weaponSlotManager.LoadWeaponOnSlot(weaponInRightHandSlot[currentRightWeaponIndex], false);
        }
        else if (currentRightWeaponIndex == 1 && weaponInRightHandSlot[1] == null)
        {
            currentRightWeaponIndex = currentRightWeaponIndex + 1;
        }


        if (currentRightWeaponIndex > weaponInRightHandSlot.Length -1)
        {
            currentRightWeaponIndex = -1;
            rightWeapon = unarmedWeapon;
            weaponSlotManager.LoadWeaponOnSlot(unarmedWeapon, false);

        }
    }

    
}
