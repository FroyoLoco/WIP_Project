/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;
using UnityEngine.InputSystem;

public class Dirt_Player_Controller : MonoBehaviour
{
    public PlayerInput currentInput;
    private InputAction digAction;
    private InputAction startGameAction;
    private InputAction orbitAction;

    private Vector2 orbitDirection;

    private const float CAMERA_DISTANCE = 12f;
    private Camera mainCamera;

    #region Enable/Disable/Start
    private void OnEnable() => SetupControls();
    private void Start() => AddControlEvents();
    private void OnDisable()
    {
        RemoveControlEvents();
        DisableControls();
    }
    #endregion

    //Get the control actions and enable them
    private void SetupControls()
    {
        mainCamera = Camera.main;
        if (!mainCamera)
            Debug.LogError("Failed to find main camera");

        digAction = currentInput.currentActionMap.FindAction("Dig");
        if (digAction != null)
            digAction.Enable();
        else
            Debug.LogError("Failed to find dig action");
        

        startGameAction = currentInput.currentActionMap.FindAction("Start_Game");
        if (startGameAction != null)
            startGameAction.Enable();
        else
            Debug.LogError("Failed to find start game action");

        orbitAction = currentInput.currentActionMap.FindAction("Orbit_Arrows");
        if (orbitAction != null)
            orbitAction.Enable();
        else
            Debug.LogError("Failed to find orbit action");

    }

    //Disable the control actions
    private void DisableControls()
    {
        if (digAction != null)
            digAction.Disable();

        if (startGameAction != null)
            startGameAction.Disable();

        if (orbitAction != null)
            orbitAction.Disable();
    }


    #region Control Events
    //Testing controls
    private void OnDigAction(InputAction.CallbackContext obj)
    {
        DigBlock();
    }

    private void DigBlock()
    {
        Dirt_Numbers.Add_Dirt(Dirt_Inc_Settings.GetBlockValue(Dirt_Inc_Settings.GetCurrentBlock().GetBlockType()));
        Dirt_Inc_Settings.Adjust_Current_Block(1);
    }

    //Testing Controls
    private void OnStartGameAction(InputAction.CallbackContext obj)
    {
        Upgrade_Manager.upgradeAccessor.TryUpgrade(Upgrade_Manager.UpgradeType.Blocks_Per_Tap);
    }

    private void OnOrbit(InputAction.CallbackContext obj)
    {
        orbitDirection = obj.ReadValue<Vector2>();
    }


    #endregion

    #region Adding and Removing Control Events
    private void AddControlEvents()
    {
        if (digAction != null)
            digAction.performed += OnDigAction;

        if (startGameAction != null)
            startGameAction.performed += OnStartGameAction;

        if (orbitAction != null)
            orbitAction.performed += OnOrbit;
    }

    private void RemoveControlEvents()
    {
        if (digAction != null)
            digAction.performed -= OnDigAction;

        if (startGameAction != null)
            startGameAction.performed -= OnStartGameAction;

        if (orbitAction != null)
            orbitAction.performed -= OnOrbit;
    }

    #endregion

    #region Control Functionality
    private void Update()
    {
        if(orbitDirection != Vector2.zero)
        {
            print(orbitDirection);
        }
    }
    #endregion
}
