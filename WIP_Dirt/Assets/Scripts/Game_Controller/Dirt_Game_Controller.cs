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
        Coord_Settings.Initialise_Coords();

        //initiailise upgrades
        Upgrade_Manager.Setup_Upgrades();

        SpawnGround(Ground_Settings.Get_Ground_Count());
        

        //Initialise camera funtionality
        Camera_Functionality.Setup_Camera();
    }

    private void SpawnGround(int _count)
    {
        Vector3 centre = Ground_Settings.Get_World_Center_Pos();
        float xDistance = Ground_Settings.Get_X_Ground_Distance();

        for (int _id = 0; _id < _count; _id++)
        {
            float newXDistance = xDistance * _id;
            Vector3 newCenter = Vector3.right * newXDistance;
            newCenter += centre;
        
            //Set each ground to the main settings
            Ground_Settings.Set_Ground(_id, Ground_Generator.Generate_Ground(newCenter));
        }
    }
}
