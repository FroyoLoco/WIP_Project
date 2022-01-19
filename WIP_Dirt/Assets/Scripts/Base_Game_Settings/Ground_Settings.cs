using UnityEngine;

public class Ground_Settings : Block_Settings
{
    #region Base ground variables
    private const byte BLOCK_COUNT_X = 22;
    private const byte BLOCK_COUNT_Y = 12;
    private const byte BLOCK_COUNT_Z = 12;
    public static byte Get_Block_Count_X() => BLOCK_COUNT_X;
    public static byte Get_Block_Count_Y() => BLOCK_COUNT_Y;
    public static byte Get_Block_Count_Z() => BLOCK_COUNT_Z;
    private static readonly Vector3 worldCenterPos = new Vector3(0,-Get_Block_Count_Y(),0);
    public static Vector3 Get_World_Center_Pos() => worldCenterPos;
    private const int GROUND_COUNT = 3;
    private const float X_DISTANCE_BETWEEN_GROUND = BLOCK_COUNT_X;
    public static float Get_X_Ground_Distance() => X_DISTANCE_BETWEEN_GROUND * 2f;
    public static int Get_Ground_Count() => GROUND_COUNT;
    private static int activeGround;
    public static int Get_Active_Ground() => activeGround;

    public static bool Set_Active_Ground(int _i)
    {
        if (_i >= 0 && _i < Get_Ground_Count())
        {
            activeGround = _i;
            return true;
        }

        //Debug.LogWarning($"Cannot set active ground (should be 0 - {Get_Ground_Count() - 1} is: {_i}");
        return false;
    }
    //The list of blocks in each block grid
    private static Block[][,,] blockContainer = new Block[Get_Ground_Count()][,,];
    public static bool Set_Ground(int _groundID, Block[,,] _ground)
    {
        if (Check_Ground_ID(_groundID))
        {
            blockContainer[_groundID] = _ground;

            return blockContainer.GetLength(0) == BLOCK_COUNT_X
                && blockContainer.GetLength(1) == BLOCK_COUNT_Y
                && blockContainer.GetLength(2) == BLOCK_COUNT_Z;
        }
        else
            Debug.LogError($"Attempting to set a ground outside of range! Setting:{_ground} Max:{Get_Ground_Count() - 1}");

        return false;
    }

    public static Block[,,] Get_Ground(int _groundID)
    {
        if (Check_Ground_ID(_groundID))
            return blockContainer[_groundID];
        else
            return null;
    }

    public static bool Check_Ground_ID(int _id)
    {
        return _id >= 0 && _id < Get_Ground_Count();
    }

    #endregion

    #region Functionality to traverse and get the current position for each ground
    private static void Set_Current_Block_Coords(int _groundID, Coord_Settings.Block_Coords _new)
    {
        if (Check_Ground_ID(_groundID))
            Coord_Settings.Get_Ground_Current_Block_Coords()[_groundID] = _new;
        else
            Debug.LogError("Failed to set current block coords");
    }

    private static Block Get_Block(int _groundID, Coord_Settings.Block_Coords _coords)
    {

        if (Check_Ground_ID(_groundID))
            return Get_Ground(_groundID)[_coords.x, _coords.y, _coords.z];
        else
            return null;
    }

    public static Block Get_Current_Block(int _groundID)
    {
        if (Check_Ground_ID(_groundID))
            return Get_Block(_groundID, Coord_Settings.Get_Current_Block_Coords(_groundID));
        else
            return null;
    }

    //Functionlity to move through the blocks in the grid
    public static void Adjust_Current_Block(int _groundID, byte _adjustment)
    {
        Coord_Settings.Block_Coords newCoords = Coord_Settings.Get_Current_Block_Coords(_groundID);

        for (byte i = 0; i < _adjustment; i++)
        {
            Block temp = Get_Ground(_groundID)[newCoords.x, newCoords.y, newCoords.z];
            if (temp == null)
            {
                goto Failed;
            }

            if (newCoords.x + 1 >= BLOCK_COUNT_X)
            {
                temp.Toggle_Block(false);
                newCoords.x = 0;

                if (newCoords.z + 1 >= BLOCK_COUNT_Z)
                {
                    newCoords.z = 0;

                    if (newCoords.y + 1 >= BLOCK_COUNT_Y)
                    {
                        newCoords.y = 0;
                        Reset_Ground(_groundID);
                    }
                    else
                    {
                        newCoords.y += 1;
                    }
                }
                else
                {
                    newCoords.z += 1;
                }
            }
            else
            {
                temp.Toggle_Block(false);
                newCoords.x += 1;
            }
        }

        Set_Current_Block_Coords(_groundID, newCoords);
        return;

    Failed:
        Debug.LogError("Block not found!");

    }

    //Reset all the blocks back to visible
    private static void Reset_Ground(int _groundID)
    {
        if (Check_Ground_ID(_groundID))
            Ground_Generator.Update_Ground(ref blockContainer[_groundID]);
        else
            Debug.LogError($"Failed to reset ground ID:{_groundID}");
    }
    #endregion
}
