using UnityEngine;

public class Block_Settings : MonoBehaviour
{
    //The size of the blocks
    private const float BLOCK_SCALE_X = 1;
    private const float BLOCK_SCALE_Y = 1;
    private const float BLOCK_SCALE_Z = 1;
    public static float Get_Block_Scale_X() => BLOCK_SCALE_X;
    public static float Get_Block_Scale_Y() => BLOCK_SCALE_Y;
    public static float Get_Block_Scale_Z() => BLOCK_SCALE_Z;

    //Different block types
    public enum BlockType { dirt, stone, tin, copper, iron, lead, gold, emerald, diamond };

    //The block container and constructor
    public class Block
    {
        private Coord_Settings.Block_Coords blockCoords;
        private BlockType blockType;
        private GameObject blockObject;
        private Block_Container blockContainer;

        public Block(Coord_Settings.Block_Coords _blockCoords, BlockType _blockType, GameObject _blockObject, Block_Container _blockContainer)
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

        public Coord_Settings.Block_Coords Get_Block_Coords() => blockCoords;
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

        switch (_block)
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
        float totalChance = 101f;
        float randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= diamondChance)
        {
            return BlockType.diamond;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= emeraldChance)
        {
            return BlockType.emerald;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= goldChance)
        {
            return BlockType.gold;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= leadChance)
        {
            return BlockType.lead;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= ironChance)
        {
            return BlockType.iron;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= copperChance)
        {

            return BlockType.copper;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= tinChance)
        {
            return BlockType.tin;
        }


        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= tinChance)
        {
            return BlockType.tin;
        }

        randomBlockChance = Random.Range(0f, totalChance);

        if (randomBlockChance <= stoneChance)
        {
            return BlockType.stone;
        }

        return BlockType.dirt;
    }
}
