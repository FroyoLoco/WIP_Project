/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Contains the prefabs for use in the game
public class Prefab_Manager : MonoBehaviour
{
    //Block resources
    private static GameObject dirtBlockPrefab;
    private static Material DIRT_MAT;
    private static Material STONE_MAT;
    private static Material TIN_MAT;
    private static Material COPPER_MAT;
    private static Material IRON_MAT;
    private static Material LEAD_MAT;
    private static Material GOLD_MAT;
    private static Material EMERALD_MAT;
    private static Material DIAMOND_MAT;
    private readonly string MATERIAL_RESOURCE_PATH = $"Materials/Blocks/";
    private readonly string PREFAB_RESOURCE_PATH = "Prefabs/";

    //Load resources
    private void OnEnable()
    {
        if (!Load_Materials())
            Debug.LogError("Failed to load materials");
        if (!Load_Prefabs())
            Debug.LogError("Failed to load prefabs");
    }

    //Unload resources
    private void OnDisable()
    {
        Resources.UnloadUnusedAssets();
    }

    private bool Load_Prefabs()
    {
        if(!(dirtBlockPrefab = Resources.Load<GameObject>(PREFAB_RESOURCE_PATH+$"Dirt_Block/Dirt_Block")))
        {
            Debug.LogError("Failed to load dirt block prefab");
            return false;
        }

        return true;
    }

    private bool Load_Materials()
    {
        if (!(DIRT_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Dirt_MAT")))
        {
            Debug.LogError("Failed to load Dirt Mat");
            return false;
        }

        if (!(STONE_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Stone_MAT")))
        {
            Debug.LogError("Failed to load Stone Mat");
            return false;
        }
        if (!(TIN_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Tin_MAT")))
        {
            Debug.LogError("Failed to load Tin Mat");
            return false;
        }
        if (!(COPPER_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Copper_MAT")))
        {
            Debug.LogError("Failed to load Copper Mat");
            return false;
        }
        if (!(IRON_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Iron_MAT")))
        {
            Debug.LogError("Failed to load Iron Mat");
            return false;
        }
        if (!(LEAD_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Lead_MAT")))
        {
            Debug.LogError("Failed to load Lead Mat");
            return false;
        }
        if (!(GOLD_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Gold_MAT")))
        {
            Debug.LogError("Failed to load Gold Mat");
            return false;
        }
        if (!(EMERALD_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Emerald_MAT")))
        {
            Debug.LogError("Failed to load Emerald Mat");
            return false;
        }
        if (!(DIAMOND_MAT = Resources.Load<Material>(MATERIAL_RESOURCE_PATH + $"Diamond_MAT")))
        {
            Debug.LogError("Failed to load Diamond Mat");
            return false;
        }

        return true;
    }

    //Get the block prefab
    public static GameObject Get_Dirt_Block_Prefab() => dirtBlockPrefab;

    //Get the material based on the block type
    public static Material Get_Material(Block_Settings.BlockType _blockType)
    {
        switch(_blockType)
        {
            case Block_Settings.BlockType.dirt:
                return DIRT_MAT;
            case Block_Settings.BlockType.stone:
                return STONE_MAT;
            case Block_Settings.BlockType.tin:
                return TIN_MAT;
            case Block_Settings.BlockType.copper:
                return COPPER_MAT;
            case Block_Settings.BlockType.iron:
                return IRON_MAT;
            case Block_Settings.BlockType.lead:
                return LEAD_MAT;
            case Block_Settings.BlockType.gold:
                return GOLD_MAT;
            case Block_Settings.BlockType.emerald:
                return EMERALD_MAT;
            case Block_Settings.BlockType.diamond:
                return DIAMOND_MAT;
            default:
                return DIRT_MAT;
        }
    }
}
