
using UnityEngine;

public class Digger_Settings : MonoBehaviour
{
    private static ulong BlocksPerDig;

    public static void Setup_Digger()
    {
        BlocksPerDig = Upgrade_Manager.Get_Upgrade_Container().
                       Get_Upgrade_From_Type(Upgrade_Manager.UpgradeType.Blocks_Per_Digger).Get_Upgrade_Count();

    }
}
