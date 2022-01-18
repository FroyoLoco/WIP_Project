public class Coord_Settings
{
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

    //The block which is currently in use by the game
    private static Block_Coords[] currentBlockCoords = new Block_Coords[Ground_Settings.Get_Ground_Count()];
    //Get external access to the full list of coords
    public static Block_Coords[] Get_Ground_Current_Block_Coords() => currentBlockCoords;
    //Gets the coords of the current block on a specific ground
    public static Block_Coords Get_Current_Block_Coords(int _groundID) => currentBlockCoords[_groundID];
    
    //Needs to be called to initialse the current coords
    public static void Initialise_Coords()
    {
        for (int i = 0; i < currentBlockCoords.Length; i++)
        {
            currentBlockCoords[i] = new Block_Coords(0, 0, 0);
        }
    }
}
