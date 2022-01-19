/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Creates the ground block grid
public class Ground_Generator : MonoBehaviour
{
    //Generate a grid of blocks in the world
    public static Block_Settings.Block[,,] Generate_Ground(int _groundID, Vector3 _groundCenter)
    {
        byte maxX = Ground_Settings.Get_Block_Count_X();
        byte maxY = Ground_Settings.Get_Block_Count_Y();
        byte maxZ = Ground_Settings.Get_Block_Count_Z();
        GameObject blockPrefab = Prefab_Manager.Get_Dirt_Block_Prefab();
        Vector3 blockStartPos = _groundCenter;
        float blockScaleX = Block_Settings.Get_Block_Scale_X();
        float blockScaleY = Block_Settings.Get_Block_Scale_Y();
        float blockScaleZ = Block_Settings.Get_Block_Scale_Z();

        //Update start pos to offset from center
        Calculate_Block_Start_Pos(ref blockStartPos, maxX, maxY, maxZ, blockScaleX, blockScaleY, blockScaleZ);
        //Create block list to return
        Block_Settings.Block[,,] generatedGround = new Block_Settings.Block[maxX, maxY, maxZ];
        //Create new gameobject to act as a parent
        GameObject newParent = new GameObject();
        Transform newParentTransform = newParent.transform;
        newParent.name = $"Ground ID: {_groundID}";
        newParentTransform.position = blockStartPos;

        for(byte y = 0; y < maxY; y++)
        {
            for (byte z = 0; z < maxZ; z++)
            {
                for (byte x = 0; x < maxX; x++)
                {
                    float posX = blockStartPos.x + (blockScaleX * x);
                    float posY = blockStartPos.y - (blockScaleY * y);
                    float posZ = blockStartPos.z + (blockScaleZ * z);
                    Vector3 spawnPos = new Vector3(posX, posY, posZ);

                    GameObject _block = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                    generatedGround[x, y, z] = new Block_Settings.Block(new Coord_Settings.Block_Coords(x, y, z),
                                                                         Block_Settings.Get_Random_BlockType(),
                                                                         _block,
                                                                         _block.GetComponent<Block_Container>());

                    _block.transform.parent = newParentTransform;

                    _block.name = $"{generatedGround[x, y, z].Get_Block_Type()} @ " +
                                  $"x: {generatedGround[x, y, z].Get_Block_Coords().x}, " +
                                  $"y: {generatedGround[x, y, z].Get_Block_Coords().y}, " +
                                  $"z: {generatedGround[x, y, z].Get_Block_Coords().z}";

                    if (!generatedGround[x, y, z].Get_Block_Container())
                        Debug.LogError("Failed to find block container!");

                    generatedGround[x, y, z].Get_Block_Container().Set_Block_Material(generatedGround[x, y, z].Get_Block_Type());
                                  
                }
            }
        }

        return generatedGround;
    }

    private static void Calculate_Block_Start_Pos(ref Vector3 _pos, int _xSize, int _ySize, int _zSize, float _scaleX, float _scaleY, float _scaleZ)
    {
        float xOffset = _xSize / 2f * _scaleX;
        float yOffset = _ySize / 2f * _scaleY;

        _pos.x -= xOffset;
        _pos.y += yOffset;
        _pos.z -= _zSize / 2 * _scaleZ;
    }

    public static void Update_Ground(ref Block_Settings.Block[,,] _ground)
    { 
        foreach (Block_Settings.Block _b in _ground)
        {
            _b.Set_Block_Type(Block_Settings.Get_Random_BlockType());
            _b.Toggle_Block(true);
        }
    }
}
