/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Contains all of the base values and functionality needed for the game
public class Dirt_Inc_Settings
{
    //hello
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
    public struct Block
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
            blockObject.SetActive(toggle);
        }

        public Block_Coords GetBlockCoords() => blockCoords;
        public BlockType GetBlockType() => blockType;
        public GameObject GetBlockObject() => blockObject;
        public Block_Container GetBlockContainer() => blockContainer;

        public void Set_Block_Type(BlockType _blockType)
        {
            blockType = _blockType;
        }
    }

    //The value of a block
    private static double dirtValue = 0.1d;
    public static double GetBlockValue(BlockType _block)
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

    public void UpdateDirtValue()
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

    public static BlockType GetRandomBlockType()
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
    //The list of blocks in the block grid
    private static Block[,,] Block_Container;
    public static bool Set_Ground(Block[,,] _ground)
    {
        Block_Container = _ground;

        return (Block_Container.GetLength(0) == BLOCK_COUNT_X
            && Block_Container.GetLength(1) == BLOCK_COUNT_Y
            && Block_Container.GetLength(2) == BLOCK_COUNT_Z);
    }

    public static Block[,,] Get_Ground() => Block_Container;

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
    private static Block_Coords currentBlockCoords = new Block_Coords(0,0,0);
    public static Block_Coords Get_Current_Block_Coords() => currentBlockCoords;
    private static void Set_Current_Block_Coords(Block_Coords _new)
    {
        currentBlockCoords = _new;
    }

    private static Block GetBlock(Block_Coords _coords)
    {
        return Get_Ground()[_coords.x, _coords.y, _coords.z];
    }
    
    public static Block GetCurrentBlock()
    {
        return GetBlock(Get_Current_Block_Coords());
    }

    //Functionlity to move through the blocks in the grid
    public static void Adjust_Current_Block(byte _adjustment)
    {
        Block_Coords newCoords = Get_Current_Block_Coords();

        for(byte i = 0; i < _adjustment; i++)
        {
            Block temp = Get_Ground()[newCoords.x, newCoords.y, newCoords.z];

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
                        Reset_Ground();
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

        Set_Current_Block_Coords(newCoords);
        Debug.Log($"We are at x: {currentBlockCoords.x} y: {currentBlockCoords.y} z: {currentBlockCoords.z}");
    }

    //Reset all the blocks back to visible
    private static void Reset_Ground()
    {
        Ground_Generator.Update_Ground(ref Block_Container);
    }
    #endregion
}
