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


    private const float CAMERA_DISTANCE = 12f;

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
        digAction = currentInput.currentActionMap.FindAction("Dig");
        if (digAction != null)
            digAction.Enable();

        startGameAction = currentInput.currentActionMap.FindAction("Start_Game");
        if (startGameAction != null)
            startGameAction.Enable();

        orbitAction = currentInput.currentActionMap.FindAction("Orbit");
        if (orbitAction != null)
            orbitAction.Enable();

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
        //print("holding" + obj.action.activeControl.name);
        string n = obj.action.activeControl.name;

        switch (n)
        {
            case "leftArrow":
                print("move left");
                break;
            case "rightArrow":
                print("move right");
                break;
            case "upArrow":
                print("move up");
                break;
            case "downArrow":
                print("move down");
                break;
            default:
                print("control not found");
                break;
        }
        
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
}
