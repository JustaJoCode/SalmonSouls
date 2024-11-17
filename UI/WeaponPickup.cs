using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPickup : Interactables
{
    public WeaponItem weapon;
    public GameObject nemo;

    Dialogue dialogue;


    private void Awake()
    {
        dialogue = FindObjectOfType<Dialogue>();
    }

    public override void Interact(PlayerManager playerManager)
    {
        base.Interact(playerManager);
        PickUpItem(playerManager);

    }


    private void PickUpItem(PlayerManager playerManager)
    {
       
        PlayerInventory playerInventory;
        PlayerLocomotion playerLocomotion;
        PlayerAnimatorManager animatorHandler;

        playerInventory = playerManager.GetComponent<PlayerInventory>();
        playerLocomotion = playerManager.GetComponent<PlayerLocomotion>();
        animatorHandler = playerManager.GetComponentInChildren<PlayerAnimatorManager>();


        playerLocomotion.rigidbody.velocity = Vector3.zero;
        animatorHandler.PlayTargetAnimation("Pick Up Item", true);
        dialogue.hasSword = true;
        playerInventory.weaponsInventory.Add(weapon);
        playerManager.IteminteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
        playerManager.IteminteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
        playerManager.IteminteractableGameObject.SetActive(true);
        Destroy(gameObject);

    }

}
