/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Contains all of the base values and functionality needed for the game
public class Dirt_Inc_Settings
{
    #region Block Variables
    //Variables for the size of the block grid
    private const byte BLOCK_COUNT_X = 20;
    private const byte BLOCK_COUNT_Y = 10;
    private const byte BLOCK_COUNT_Z = 10;
    public static byte Get_Block_Count_X() => BLOCK_COUNT_X;
    public static byte Get_Block_Count_Y() => BLOCK_COUNT_Y;
    public static byte Get_Block_Count_Z() => BLOCK_COUNT_Z;

    //Where the blocks will begin
    private static readonly Vector3 worldCenterPos = Vector3.zero;
    public static Vector3 Get_World_Center_Pos() => worldCenterPos;

    //The size of the blocks
    private const float BLOCK_SCALE_X = 1;
    private const float BLOCK_SCALE_Y = 1;
    private const float BLOCK_SCALE_Z = 1;
    public static float Get_Block_Scale_X() => BLOCK_SCALE_X;
    public static float Get_Block_Scale_Y() => BLOCK_SCALE_Y;
    public static float Get_Block_Scale_Z() => BLOCK_SCALE_Z;

    //Different block types
    public enum BlockType {dirt, stone, tin, copper, iron, lead, gold, emerald, diamond};

    //The block container and constructor
    public class Block
    {
        private Block_Coords blockCoords;
        private BlockType blockType;
        private GameObject blockObject;
        private Block_Container blockContainer;

        public Block(Block_Coords _blockCoords, BlockType _blockType, GameObject _blockObject, Block_Container _blockContainer)
        {
            blockCoords = _blockCoords;
            blockType = _blockType;
            blockObject = _blockObject;
            blockContainer = _blockContainer;
        }

        public void Toggle_Block(bool toggle)
        {
            Debug.Log(blockObject);
            blockObject.SetActive(toggle);
        }

        public Block_Coords Get_Block_Coords() => blockCoords;
        public BlockType Get_Block_Type() => blockType;
        public GameObject Get_Block_Object() => blockObject;
        public Block_Container Get_Block_Container() => blockContainer;

        public void Set_Block_Type(BlockType _blockType)
        {
            blockType = _blockType;
        }
    }

    //The value of a block
    private static double dirtValue = 0.1d;
    public static double Get_Block_Value(BlockType _block)
    {
        double blockValue = dirtValue;

        switch(_block)
        {
            case BlockType.dirt:
                return blockValue;
            case BlockType.stone:
                return blockValue *= 2f;
            case BlockType.tin:
                return blockValue *= 3f;
            case BlockType.copper:
                return blockValue *= 4f;
            case BlockType.iron:
                return blockValue *= 5f;
            case BlockType.lead:
                return blockValue *= 10f;
            case BlockType.gold:
                return blockValue *= 100f;
            case BlockType.emerald:
                return blockValue *= 500f;
            case BlockType.diamond:
                return blockValue *= 1000;
            default:
                return blockValue;
        }
    }

    public void Update_Dirt_Value()
    {
        dirtValue = 1;
    }

    //Different block type chances
    private static readonly float stoneChance = 15;
    private static readonly float tinChance = 8;
    private static readonly float copperChance = 4;
    private static readonly float ironChance = 3;
    private static readonly float leadChance = 1;
    private static readonly float goldChance = 0.5f;
    private static readonly float emeraldChance = 0.2f;
    private static readonly float diamondChance = 0.05f;

    public static BlockType Get_Random_BlockType()
    {
        float totalChance = 101;
        float randomBlockChance = Random.Range(1, totalChance);

        if(randomBlockChance <= diamondChance)
        {
            return BlockType.diamond;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if(randomBlockChance <= emeraldChance)
        {
            return BlockType.emerald;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= goldChance)
        {
            return BlockType.gold;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= leadChance)
        {
            return BlockType.lead;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= ironChance)
        {
            return BlockType.iron;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= copperChance)
        {

            return BlockType.copper;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= tinChance)
        {
            return BlockType.tin;
        }


        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= tinChance)
        {
            return BlockType.tin;
        }

        randomBlockChance = Random.Range(1, totalChance);

        if (randomBlockChance <= stoneChance)
        {
            return BlockType.stone;
        }

        return BlockType.dirt; 
    }
    #endregion

    #region Block Grid and Coords
    private const int GROUND_COUNT = 4;
    private const float X_DISTANCE_BETWEEN_GROUND = BLOCK_COUNT_X;
    public static float Get_X_Ground_Distance() => X_DISTANCE_BETWEEN_GROUND * 2f;
    public static int Get_Ground_Count() => GROUND_COUNT;
    private static int activeGround;
    public static int Get_Active_Ground() => activeGround;
    public static bool Set_Active_Ground(int _i)
    {
        if(_i >= 0 && _i < Get_Ground_Count())
        {
            activeGround = _i;
            return true;
        }

        Debug.LogWarning($"Cannot set active ground (should be 0 - {Get_Ground_Count() - 1} is: {_i}");
        return false;
    }
    //The list of blocks in each block grid
    private static Block[][,,] blockContainer = new Block[Get_Ground_Count()][,,];
    public static bool Set_Ground(int _groundID, Block[,,] _ground)
    {
        if (Check_Ground_ID(_groundID))
        {
            blockContainer[_groundID] = _ground;

            return (blockContainer.GetLength(0) == BLOCK_COUNT_X
                && blockContainer.GetLength(1) == BLOCK_COUNT_Y
                && blockContainer.GetLength(2) == BLOCK_COUNT_Z);
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

    //Contain for the coordinates of a block
    public struct Block_Coords
    {
        public byte x;
        public byte y;
        public byte z;

        public Block_Coords(byte _x, byte _y, byte _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }
    #endregion

    #region Block Adjustments
    //The block which is currently in use by the game
    private static Block_Coords[] currentBlockCoords = new Block_Coords[blockContainer.Length];
    public static Block_Coords Get_Current_Block_Coords(int _groundID) => currentBlockCoords[_groundID];
    public static void Initialise_Coords()
    {
        for(int i = 0; i < currentBlockCoords.Length; i++)
        {
            currentBlockCoords[i] = new Block_Coords(0, 0, 0);
        }
    }
    private static void Set_Current_Block_Coords(int _groundID, Block_Coords _new)
    {
        if (Check_Ground_ID(_groundID))
            currentBlockCoords[_groundID] = _new;
        else
            Debug.LogError("Failed to set current block coords");
    }

    private static Block Get_Block(int _groundID, Block_Coords _coords)
    {

        if (Check_Ground_ID(_groundID))
            return Get_Ground(_groundID)[_coords.x, _coords.y, _coords.z];
        else
            return null;
    }
    
    public static Block Get_Current_Block(int _groundID)
    {
        if (Check_Ground_ID(_groundID))
            return Get_Block(_groundID, Get_Current_Block_Coords(_groundID));
        else
            return null;
    }

    //Functionlity to move through the blocks in the grid
    public static void Adjust_Current_Block(int _groundID, byte _adjustment)
    {
        Block_Coords newCoords = Get_Current_Block_Coords(_groundID);

        for(byte i = 0; i < _adjustment; i++)
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
