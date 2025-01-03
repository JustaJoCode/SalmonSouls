using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHoldersSlot : MonoBehaviour
{
    public Transform parentOveride;

    public bool isLeftHandSlot;
    public bool isRightHandSlot;

    public GameObject currentWeaponModel;
    public WeaponItem currentWeapon;

    public void UnloadWeapon()
    {
        if(currentWeaponModel !=null)
        {
            currentWeaponModel.SetActive(false);
        }
    }
    public void UnloadWeaponAndDestroy()
    {
        if(currentWeaponModel !=null)
        {
            Destroy(currentWeaponModel);
        }
    }

    public void LoadWeaponModel(WeaponItem weaponItem)
    {
        UnloadWeaponAndDestroy();


        if (weaponItem == null)
        {
            UnloadWeapon();

            return;
        }

            GameObject model = Instantiate(weaponItem.modelprefab) as GameObject;

            if(model != null)
            {
                if(parentOveride !=null)
                {
                    model.transform.parent = parentOveride;
                }
                else
                {
                    model.transform.parent = transform;
                }

                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
            }

            currentWeaponModel = model;
        
    }
}
