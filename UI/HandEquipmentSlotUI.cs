using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
    UIManager uIManager;
    public Image icon;
    WeaponItem weapon;

    public bool rightHandSlot01;
    public bool rightHandSlot02;

    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(WeaponItem newweapon)
    {
        weapon = newweapon;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void Clearitem()
    {
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void SelectThisSlot()
    {
        if (rightHandSlot01)
        {
            uIManager.rightHandSLot01Selected = true;
        }
        else if (rightHandSlot02)
        {
            uIManager.rightHandSLot02Selected = true;
        }
       
    }
}
