using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventColliderBeginBossFight : MonoBehaviour
{
    WorldEventManager WorldEventManager;

    private void Awake()
    {
        WorldEventManager = FindObjectOfType<WorldEventManager>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WorldEventManager.ActivateBossFight();
        }
    }
}
