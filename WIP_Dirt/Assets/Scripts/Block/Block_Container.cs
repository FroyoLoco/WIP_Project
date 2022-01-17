/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

public class Block_Container : MonoBehaviour
{
    private MeshRenderer currentMesh;

    //Get mesh renderer component
    private void OnEnable()
    {
        currentMesh = GetComponent<MeshRenderer>();
    }

    //Set the block material based in the input type
    public void Set_Block_Material(Dirt_Inc_Settings.BlockType _blockType)
    {
        if (currentMesh)
        {
            currentMesh.material = Prefab_Manager.Get_Material(_blockType);
        }
        else
        {
            Debug.LogError("Mesh renderer not found!");
        }
    }
}
