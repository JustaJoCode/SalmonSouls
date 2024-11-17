  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool b_Input;
    public bool a_Input;
    public bool x_input;
    public bool rb_Input;
    public bool rt_Input;
    public bool critical_Attack_Input;
    public bool inventory_Input;
    public bool right_Stick_Right_Input;
    public bool right_Stick_Left_Input;

    public bool d_Pad_Up;
    public bool d_Pad_Down;
    public bool d_Pad_Left;
    public bool d_Pad_Right;


    public bool rollFlag;
    public bool sprintFlag;
    public bool comboFlag;
    public bool inventoryFlag;
    public bool lockOnFlag;
    public bool lockOn_Input;
    public float rollInputTimer;

    public Transform criticalAttackRayCastStarterPoint;

    PlayerControls inputActions;
    PlayerAttacker playerAttacker;
    PlayerInventory playerInventory;
    PlayerManager playerManager;
    PlayerStats playerStats;
    UIManager uIManager;
    CameraHandler cameraHandler;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerEffectsManager playerEffectsManager;
    WeaponSlotManager weaponSlotManager;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Awake()
    { 
        playerAttacker = GetComponent<PlayerAttacker>();
        playerInventory = GetComponent<PlayerInventory>();
        playerManager = GetComponent<PlayerManager>();
        playerStats = GetComponent<PlayerStats>();
        uIManager = FindObjectOfType<UIManager>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
        playerEffectsManager = GetComponentInChildren<PlayerEffectsManager>();
        weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
    }

    public void OnEnable()
    {

        if (inputActions == null)
        {
            inputActions = new PlayerControls();
            inputActions.Player_Movement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
            inputActions.Player_Movement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            inputActions.Player_Movement.LockOnTargetRIght.performed += i => right_Stick_Right_Input = true;
            inputActions.Player_Movement.LockOnTargetLeft.performed += i => right_Stick_Left_Input = true;
            inputActions.PlayerActions.Light.performed += i => rb_Input = true;
            inputActions.PlayerActions.Heavy.performed += i => rt_Input = true;
            inputActions.PlayerActions.X.performed += i => x_input = true;
            inputActions.PlayerActions.Roll.performed += i => b_Input = true ;
            inputActions.PlayerActions.Roll.canceled += i => b_Input = false;
            inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;
            inputActions.PlayerActions.DPad_Right.performed += i => d_Pad_Right = true;
            inputActions.PlayerActions.DPad_Left.performed += i => d_Pad_Left = true;
            inputActions.PlayerActions.LockOn.performed += i => lockOn_Input = true;
            inputActions.PlayerActions.CritAttack.performed += i => critical_Attack_Input = true;
        }

        inputActions.Enable();
        inputActions.PlayerActions.Interact.performed += i => a_Input = true;

    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public void TickInput(float delta)
    {
        HandleMoveInput(delta);
        HandleRollingInput(delta);
        HandleAttackInput(delta);
        HandleQuickSlotInput();
        HandleInventoryInput();
        HandleLockOnInput();
        HandleCriticalAttackInput();
        HandleUseConsumableInput();
    }

    private void HandleMoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void HandleRollingInput(float delta)
    {
        if (b_Input)
        {
            rollInputTimer += delta;

            if (playerStats.currentStamina <= 0)
            {
                b_Input = false;
                sprintFlag = false;
            }

            if (moveAmount > 0.5f && playerStats.currentStamina > 0)
            {
                sprintFlag = true;
            }
        }

        else
        {
            sprintFlag = false;

            if (rollInputTimer > 0 && rollInputTimer < 0.5f)
            {
              
                rollFlag = true;

            }
            rollInputTimer = 0;
        }
    }

    public void HandleAttackInput(float delta)
    {
        if (rb_Input)
        {
            if(playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if (playerManager.isInteracting)
                    return;
                if (playerManager.canDoCombo)
                        return;

                playerAnimatorManager.anim.SetBool("isUsingRightHand", true);
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }
        }
        

        if (rt_Input)
        {

            if (playerManager.canDoCombo)
            {
                comboFlag = true;
                playerAttacker.HandleWeaponCombo(playerInventory.rightWeapon);
                comboFlag = false;
            }
            else
            {
                if(playerManager.isInteracting)
                    return;
                if (playerManager.canDoCombo)
                    return;
                
            }

        }
        
    }

    public void HandleQuickSlotInput()
    {
        if (d_Pad_Right)
        {
            playerInventory.ChangeRightWeapon();
        }

    }

    public void HandleInventoryInput()
    {
        if (inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                uIManager.OpenSelectWindow();
                uIManager.UpdateUI();
                uIManager.HUD.SetActive(false);
                
            }
            else
            {
                uIManager.CloseSelectWindow();
                uIManager.CloseAllInventoryWindows();
                uIManager.HUD.SetActive(true);
            }
        }

    }

    public void HandleLockOnInput()
    {
        if (lockOn_Input && lockOnFlag == false)
        {
            lockOn_Input = false;
            lockOnFlag = true;
            cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
            cameraHandler.HandleLockOn();
            if (cameraHandler.nearestLockOnTarget !=null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.nearestLockOnTarget;
                lockOnFlag = true;
            }

        }
        else if (lockOn_Input && lockOnFlag)
        {
            lockOn_Input = false;
            lockOnFlag = false;
            cameraHandler.ClearLockOnTargets();
        }

        if (lockOnFlag && right_Stick_Left_Input)
        {
            right_Stick_Left_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.leftLockTarget !=null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.leftLockTarget;
            }
        }

        if (lockOnFlag && right_Stick_Right_Input)
        {
            right_Stick_Right_Input = false;
            cameraHandler.HandleLockOn();
            if (cameraHandler.rightLockTarget != null)
            {
                cameraHandler.currentLockOnTarget = cameraHandler.rightLockTarget;
            }
        }

        cameraHandler.SetCameraHeight();

    }

    private void HandleCriticalAttackInput()
    {
        if (critical_Attack_Input)
        {
            critical_Attack_Input = false;
            playerAttacker.AttemptBackStabOrReposte();
        }
    }

    private void HandleUseConsumableInput()
    {
        if (x_input)
        {
            x_input = false;
            playerInventory.currentConsumable.ConsumableItemAttempt(playerAnimatorManager, weaponSlotManager, playerEffectsManager);
        }
    }

}