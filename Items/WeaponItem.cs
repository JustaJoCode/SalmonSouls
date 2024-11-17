using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]

public class WeaponItem : Item
{
    public GameObject modelprefab;
    public bool isUnarmed;

    [Header("Idle Animations")]
    public string Right_Arm_Idle_01;
    public string Left_Arm_Idle_01;

    [Header("Damage")]
    public int baseDamage = 25;
    public int criticalDamageMultiplier = 4 ;

    [Header("Attack Animations")]

    public string OH_Light_Attack_1;
    public string OH_Light_Attack_2;

    [Header("Stamina Costs")]
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;
}
