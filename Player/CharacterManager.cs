using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock On Transform")]
    public Transform lockOnTransform;


    [Header("Combat Colliders")]
    public BoxCollider backStabBoxCollider;
    public CritDamageCollider backStabCollider;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
    public bool canRotate; 

    //Damage will be infliced during animation event
    //Used in backstab or reposte
    public int pendingCriticalDamage;
}

