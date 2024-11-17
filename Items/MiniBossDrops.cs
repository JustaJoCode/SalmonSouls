using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossDrops : CharacterStats
{
    public GameObject keyModel;
    public Transform bosstransform;


    EnemyStats enemyStats;

    private void Awake()
    {
        enemyStats = GetComponent<EnemyStats>();
    }

   
    void Start()
    {
       
    }

    public void DropCoin()
    {
        Vector3 position = transform.position;
        GameObject key = Instantiate(keyModel, position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);

        
    }
}
