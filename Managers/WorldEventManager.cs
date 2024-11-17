using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<FogWall> fogwalls;

    UIBossHealthBar bossHealthBar;
    EnemyBossManager boss;

    public bool bossFightisActive;
    public bool bossHasBeenAwakened;
    public bool bossHasBeenDefeated;

    private void Awake()
    {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }

    public void ActivateBossFight()
    {
        bossFightisActive = true;
        bossHasBeenAwakened = true;

        bossHealthBar.SetUIHealthBarToActive();

        foreach (var FogWall in fogwalls)
        {
            FogWall.ActivateFogWall();
        }
    }

    public void BossHasBeenDefeated()
    {
        bossHasBeenDefeated = true;
        bossFightisActive = false;
        bossHealthBar.SetUIHealthBarToInActive();

        foreach (var FogWall in fogwalls)
        {
            FogWall.DeactivateFogWall();
        }
    }
}
