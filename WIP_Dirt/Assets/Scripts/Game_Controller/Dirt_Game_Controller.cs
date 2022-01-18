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
        //Initialise Block Coords
        Dirt_Inc_Settings.Initialise_Coords();

        //initiailise upgrades
        Upgrade_Manager.Setup_Upgrades();

        print(Dirt_Inc_Settings.Get_X_Ground_Distance());

        for (int i = 0; i < Dirt_Inc_Settings.Get_Ground_Count(); i++)
        {
            //Set each ground to the main settings
            Dirt_Inc_Settings.Set_Ground(i,
                                         Ground_Generator.Generate_Ground(Dirt_Inc_Settings.Get_World_Center_Pos()
                                         + (Vector3.right * (Dirt_Inc_Settings.Get_X_Ground_Distance() * i))));
        }

        //Initialise camera funtionality
        Camera_Functionality.Setup_Camera();
    }
}
