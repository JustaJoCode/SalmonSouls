using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WeaponInventorySlot : MonoBehaviour
{
    UIManager uIManager;
    PlayerInventory playerInventory;
    WeaponSlotManager weaponSlotManager;

    public Image icon;
    WeaponItem item;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        weaponSlotManager = FindObjectOfType<WeaponSlotManager>();
        uIManager = FindObjectOfType<UIManager>();

    }

    public void AddItem(WeaponItem newItem)
    {
        item = newItem;
        icon.sprite = item.itemIcon;
        icon.enabled = true;

        gameObject.SetActive(true);
    }

    public void ClearInventorySlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void EquipThisItem()
    {
        if (uIManager.rightHandSLot01Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlot[0]);
            playerInventory.weaponInRightHandSlot[0] = item;
            playerInventory.weaponsInventory.Remove(item);
      
        }
        else if (uIManager.rightHandSLot02Selected)
        {
            playerInventory.weaponsInventory.Add(playerInventory.weaponInRightHandSlot[1]);
            playerInventory.weaponInRightHandSlot[1] = item;
            playerInventory.weaponsInventory.Remove(item);
        }
        else
        {
            return;
        }

        playerInventory.rightWeapon = playerInventory.weaponInRightHandSlot[playerInventory.currentRightWeaponIndex];
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.rightWeapon, false);
        weaponSlotManager.LoadWeaponOnSlot(playerInventory.leftWeapon, false);

        uIManager.equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        uIManager.ResetAllSelectedSlots();
    }
}
