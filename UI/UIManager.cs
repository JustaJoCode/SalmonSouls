using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UIManager : MonoBehaviour
{
    PlayerManager playerManager;
    public PlayerInventory playerInventory;
    public EquipmentWindowUI equipmentWindowUI;

    [HideInInspector] public PlayerUIPopUpManager playerUIPopUpManager; 

    [Header("UI Stuff")]
    [SerializeField] private GameObject selectInventory;
    [SerializeField] private GameObject weaponInventory;


    [Header("UI Windows")]
    public GameObject HUD;
    public GameObject EquipmentScreenWindow;
    public GameObject selectWindow;
    public GameObject weaponInventoryWindow;

    [Header("Weapon Inventory")]
    public GameObject weaponInventorySlotPrefab;
    public Transform weaponInventorySlotsParent;
    WeaponInventorySlot[] weaponInventorySlots;

    [Header("Hand Slots")]
    public bool rightHandSLot01Selected;
    public bool rightHandSLot02Selected;

    private void Start()
    {
        weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
        equipmentWindowUI.LoadWeaponsOnEquipmentScreen(playerInventory);
        playerManager = GetComponent<PlayerManager>();
        playerUIPopUpManager = GetComponentInChildren<PlayerUIPopUpManager>();
    }

    public void UpdateUI()
    {
        #region WeaponInventorySlots

        for (int i = 0; i < weaponInventorySlots.Length; i++)
        {
            if (i< playerInventory.weaponsInventory.Count)
            {
                if(weaponInventorySlots.Length <playerInventory.weaponsInventory.Count)
                {
                    Instantiate(weaponInventorySlotPrefab, weaponInventorySlotsParent);
                    weaponInventorySlots = weaponInventorySlotsParent.GetComponentsInChildren<WeaponInventorySlot>();
                }

                weaponInventorySlots[i].AddItem(playerInventory.weaponsInventory[i]);
            }
            else
            {
                weaponInventorySlots[i].ClearInventorySlot();
            }
        }

        #endregion
    }

    public void OpenSelectWindow()
    {
        selectWindow.SetActive(true);

        EventSystem.current.SetSelectedGameObject(selectInventory);

    }

    public void CloseSelectWindow()
    {
        selectWindow.SetActive(false);
    }

    public void CloseAllInventoryWindows()
    {
        ResetAllSelectedSlots();
        weaponInventoryWindow.SetActive(false);
        EquipmentScreenWindow.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ResetAllSelectedSlots()
    {
        rightHandSLot01Selected = false;
        rightHandSLot02Selected = false;
    }

}
