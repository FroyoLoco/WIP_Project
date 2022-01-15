/*
 * Copyright Tom Morgan 2022
 */

using System;
using UnityEngine;

//Base class to display the large numbers in the game correctly
public class Dirt_Numbers
{
    //Dirt Values
    private enum DirtCountSuffix {none, K, MILL, BILL, TRILL, QUAD, QUINT, SEXT, SEPT, OCT, NON, DEC };

    //Main dirt count is here
    private static double dirtCount = 0;
    public static double Get_Dirt_Count() => dirtCount;

    public static void Add_Dirt(double _add)
    {
        dirtCount += _add;
        Debug.Log(Get_Dirt_Count_As_String());
    }

    //Returns true if if the spend amount is affordable
    public static bool Spend_Dirt(double _toSpend)
    {
        if(dirtCount - _toSpend >= 0)
        {
            dirtCount -= _toSpend;
            return true;
        }

        return false;
    }

    //Converts the dirt count to a string for display purposes
    public static string Get_Dirt_Count_As_String()
    {
        int digitCount = Get_Dirt_Digit_Count();

        if(digitCount < 4)
        {
            return Get_Dirt_Count().ToString("f2");
        }
        else
        {
            return $"{Get_Prefix(digitCount)}{Get_Suffix(digitCount)}";
        }
    }

    //Calculate the current digit count in the dirt value
    private static int Get_Dirt_Digit_Count()
    {
        return (int)Math.Log10(Get_Dirt_Count()) + 1;
    }

    private static string Get_Prefix(int _digitCount)
    {
        double dirt = Get_Dirt_Count();
        float shortDirt = (float)(dirt / Math.Pow(10, 3*((_digitCount-1)/3)));

        return shortDirt.ToString("f2");
    }

    private static string Get_Suffix(int _digitCount)
    {
        return Convert_Int_To_Enum((_digitCount - 1) / 3).ToString();
    }

    private static DirtCountSuffix Convert_Int_To_Enum(int _toConvert)
    {
        return (DirtCountSuffix)_toConvert;
    }

    //Test funtion
    public static void Test_Dirt()
    {
        Add_Dirt(1000000);
        Debug.Log($"Added dirt {Get_Dirt_Count_As_String()}");
    }
        
}
