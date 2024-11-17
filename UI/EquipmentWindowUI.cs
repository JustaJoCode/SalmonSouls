using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentWindowUI : MonoBehaviour
{
    public bool rightHandSlot01Selected;
    public bool rightHandSlot02Selected;

    public HandEquipmentSlotUI[] handEquipmentSlotUI;

    private void Start()
    {
        
    }

    public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory)
    {
        for (int i = 0; i < handEquipmentSlotUI.Length; i++)
        {
            if (handEquipmentSlotUI[i].rightHandSlot01)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlot[0]);

            }
            else if (handEquipmentSlotUI[i].rightHandSlot02)
            {
                handEquipmentSlotUI[i].AddItem(playerInventory.weaponInRightHandSlot[1]);

            }
        }
    }


    public void SelectRightHandSlot01()
    {
        rightHandSlot01Selected = true;
    }

    public void SelectRightHandSlot02()
    {
        rightHandSlot02Selected = true;
    }





}
