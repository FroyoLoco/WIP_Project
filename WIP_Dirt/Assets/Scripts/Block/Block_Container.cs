/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

public class Block_Container : MonoBehaviour
{
    private MeshRenderer currentMesh;

    private void OnEnable()
    {
        currentMesh = GetComponent<MeshRenderer>();
    }

    public void SetBlockMaterial(Dirt_Inc_Settings.BlockType _blockType)
    {
        if (currentMesh)
        {
            currentMesh.material = Prefab_Manager.GetMaterial(_blockType);
        }
        else
        {
            Debug.LogError("Mesh renderer not found!");
        }
    }
}
