using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    Animator animator;
    KeyPickUp keyPickUp;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        keyPickUp = FindObjectOfType<KeyPickUp>();

        animator.Play("idle");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (keyPickUp.hasBasementKey)
        {
            animator.Play("DoorOpen");
        }
    }
}
