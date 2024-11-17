using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorTrigger : MonoBehaviour
{
    Animator animator;
    BossKeyPickUp bosskeyPickUp;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        bosskeyPickUp = FindObjectOfType<BossKeyPickUp>();

        animator.Play("idle");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (bosskeyPickUp.hasBossKey)
        {
            animator.Play("DoorOpen");
        }
    }
}
