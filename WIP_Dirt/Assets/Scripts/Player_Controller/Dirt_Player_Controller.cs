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

    #region Enable/Disable/Start
    private void OnEnable() => Setup_Controls();
    private void Start() => Add_Control_Events();
    private void OnDisable()
    {
        Remove_Control_Events();
        Disable_Controls();
    }
    #endregion

    //Get the control actions and enable them
    private void Setup_Controls()
    {
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
    private void Disable_Controls()
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
    private void On_Dig_Action(InputAction.CallbackContext obj)
    {
        Dig_Block();
    }

    private void Dig_Block()
    {
        Dirt_Numbers.Add_Dirt(Dirt_Inc_Settings.Get_Block_Value(Dirt_Inc_Settings.Get_Current_Block(0).Get_Block_Type()));
        Dirt_Inc_Settings.Adjust_Current_Block(0, 1);
    }

    //Testing Controls
    private void On_Start_Game_Action(InputAction.CallbackContext obj)
    {
        Upgrade_Manager.upgradeAccessor.Try_Upgrade(Upgrade_Manager.UpgradeType.Blocks_Per_Tap);
    }

    private void On_Orbit(InputAction.CallbackContext obj)
    {
        if(obj.phase == InputActionPhase.Performed)
        {
            Camera_Functionality.Move_Camera(obj.ReadValue<Vector2>());
        }
    }
    #endregion

    #region Adding and Removing Control Events
    private void Add_Control_Events()
    {
        if (digAction != null)
            digAction.performed += On_Dig_Action;

        if (startGameAction != null)
            startGameAction.performed += On_Start_Game_Action;

        if (orbitAction != null)
            orbitAction.performed += On_Orbit;
    }

    private void Remove_Control_Events()
    {
        if (digAction != null)
            digAction.performed -= On_Dig_Action;

        if (startGameAction != null)
            startGameAction.performed -= On_Start_Game_Action;

        if (orbitAction != null)
            orbitAction.performed -= On_Orbit;
    }

    #endregion
}
