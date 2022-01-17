/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Creates the ground block grid
public class Ground_Generator : MonoBehaviour
{
    //Generate a grid of blocks in the world
    public static void Generate_Ground()
    {
        byte maxX = Dirt_Inc_Settings.Get_Block_Count_X();
        byte maxY = Dirt_Inc_Settings.Get_Block_Count_Y();
        byte maxZ = Dirt_Inc_Settings.Get_Block_Count_Z();
        GameObject blockPrefab = Prefab_Manager.GetDirtBlockPrefab();
        Vector3 blockStartPos = Dirt_Inc_Settings.Get_World_Center_Pos();
        float blockScaleX = Dirt_Inc_Settings.Get_Block_Scale_X();
        float blockScaleY = Dirt_Inc_Settings.Get_Block_Scale_Y();
        float blockScaleZ = Dirt_Inc_Settings.Get_Block_Scale_Z();

        //Update start pos to offset from center
        Calculate_Block_Start_Pos(ref blockStartPos, maxX, maxY, maxZ, blockScaleX, blockScaleY, blockScaleZ);
        Dirt_Inc_Settings.Block[,,] generatedGround = new Dirt_Inc_Settings.Block[maxX, maxY, maxZ];

        for(byte y = 0; y < maxY; y++)
        {
            for (byte z = 0; z < maxZ; z++)
            {
                for (byte x = 0; x < maxX; x++)
                {
                    float posX = blockStartPos.x + (blockScaleX * x);
                    float posY = blockStartPos.y - (blockScaleY * y);
                    float posZ = blockStartPos.z - (blockScaleZ * z);
                    Vector3 spawnPos = new Vector3(posX, posY, posZ);

                    GameObject _block = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                    generatedGround[x, y, z] = new Dirt_Inc_Settings.Block(new Dirt_Inc_Settings.Block_Coords(x, y, z),
                                                                         Dirt_Inc_Settings.GetRandomBlockType(),
                                                                         _block,
                                                                         _block.GetComponent<Block_Container>());

                    _block.name = $"{generatedGround[x, y, z].GetBlockType()} @ " +
                                  $"{generatedGround[x, y, z].GetBlockCoords().x}," +
                                  $"{generatedGround[x, y, z].GetBlockCoords().y}," +
                                  $"{generatedGround[x, y, z].GetBlockCoords().z}";

                    if (!generatedGround[x, y, z].GetBlockContainer())
                        Debug.LogError("Failed to find block container!");

                    generatedGround[x, y, z].GetBlockContainer().SetBlockMaterial(generatedGround[x, y, z].GetBlockType());
                                  
                }
            }
        }

        if(!Dirt_Inc_Settings.Set_Ground(generatedGround))
        {
            print("failed");
        }
        else
        {
            print($"Complete X: {Dirt_Inc_Settings.Get_Ground().GetLength(0)}, " +
                  $"Y: {Dirt_Inc_Settings.Get_Ground().GetLength(1)}, " +
                  $"Z: {Dirt_Inc_Settings.Get_Ground().GetLength(2)}");
        }
    }

    private static void Calculate_Block_Start_Pos(ref Vector3 _pos, int _xSize, int _ySize, int _zSize, float _scaleX, float _scaleY, float _scaleZ)
    {
        float xOffset = (_xSize / 2f) * _scaleX;
        float yOffset = (_ySize / 2f) * _scaleY;
        float zOffset = (_zSize / 2f) * _scaleZ;

        _pos.x -= xOffset;
        _pos.y += yOffset;
        _pos.z += zOffset;
    }

    public static void Update_Ground(ref Dirt_Inc_Settings.Block[,,] _ground)
    { 
        foreach (Dirt_Inc_Settings.Block _b in _ground)
        {
            _b.Set_Block_Type(Dirt_Inc_Settings.GetRandomBlockType());
            _b.Toggle_Block(true);
        }
    }
}
