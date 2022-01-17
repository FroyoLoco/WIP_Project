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

        for (int i = 0; i < Dirt_Inc_Settings.Get_Ground_Count(); i++)
        {
            //Figure out different positions for each ground
            Dirt_Inc_Settings.Set_Ground(i, Ground_Generator.Generate_Ground(Dirt_Inc_Settings.Get_World_Center_Pos() + new Vector3(30 * i,0,0)));
        }

        Camera_Functionality.Setup_Camera();
        //Dirt_Numbers.Test_Dirt();
    }
}
