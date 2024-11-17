using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonfireInteractable : Interactables
{
    //Bonfire Location(for tp)

    [Header("Bonfire Teleport Transform")]
    [SerializeField] Transform BonfireTeleportTransform;

    [Header("Activation Status")]
    public bool hasBeenActivated;

    // Bonfire Unique ID(for saving reasons)
    [Header("Bonfire FX")]
    public ParticleSystem activationFX;
    public ParticleSystem bubbleFX;

    private void Awake()
    {
        
    }

    public override void Interact(PlayerManager playerManager)
    {
        Debug.Log("Bubbler Activated");

    }

}
