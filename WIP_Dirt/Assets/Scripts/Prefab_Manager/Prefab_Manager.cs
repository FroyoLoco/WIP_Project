/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Contains the prefabs for use in the game
public class Prefab_Manager : MonoBehaviour
{
    private static Prefab_Manager instance;
    //Blocks
    public GameObject dirtBlockPrefab;
    public static GameObject GetDirtBlockPrefab() => instance.dirtBlockPrefab;
    public Material DIRT_MAT;
    public Material STONE_MAT;
    public Material TIN_MAT;
    public Material COPPER_MAT;
    public Material IRON_MAT;
    public Material LEAD_MAT;
    public Material GOLD_MAT;
    public Material EMERALD_MAT;
    public Material DIAMOND_MAT;

    private void OnEnable()
    {
        if (!instance)
            instance = this;
    }

    public static Material GetMaterial(Dirt_Inc_Settings.BlockType _blockType)
    {
        switch(_blockType)
        {
            case Dirt_Inc_Settings.BlockType.dirt:
                return instance.DIRT_MAT;
            case Dirt_Inc_Settings.BlockType.stone:
                return instance.STONE_MAT;
            case Dirt_Inc_Settings.BlockType.tin:
                return instance.TIN_MAT;
            case Dirt_Inc_Settings.BlockType.copper:
                return instance.COPPER_MAT;
            case Dirt_Inc_Settings.BlockType.iron:
                return instance.IRON_MAT;
            case Dirt_Inc_Settings.BlockType.lead:
                return instance.LEAD_MAT;
            case Dirt_Inc_Settings.BlockType.gold:
                return instance.GOLD_MAT;
            case Dirt_Inc_Settings.BlockType.emerald:
                return instance.EMERALD_MAT;
            case Dirt_Inc_Settings.BlockType.diamond:
                return instance.DIAMOND_MAT;
            default:
                return instance.DIRT_MAT;
        }
    }
}
