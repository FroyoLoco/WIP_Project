/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Overarching game controller
public class Dirt_Game_Controller : MonoBehaviour
{
    private void Start() => BeginGame();

    private void BeginGame()
    {
        Upgrade_Manager.SetupUpgrades();
        Ground_Generator.Generate_Ground();
        Camera_Functionality.Setup_Camera();
        //Dirt_Numbers.Test_Dirt();
    }
}
