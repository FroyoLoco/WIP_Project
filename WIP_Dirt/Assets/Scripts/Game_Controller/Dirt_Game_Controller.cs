/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Overarching game controller
public class Dirt_Game_Controller : MonoBehaviour
{
    private void Start() => Begin_Game();

    private void Begin_Game()
    {
        Upgrade_Manager.Setup_Upgrades();
        Dirt_Inc_Settings.Set_Ground(0, Ground_Generator.Generate_Ground(Dirt_Inc_Settings.Get_World_Center_Pos()));
        Camera_Functionality.Setup_Camera();
        //Dirt_Numbers.Test_Dirt();
    }
}
