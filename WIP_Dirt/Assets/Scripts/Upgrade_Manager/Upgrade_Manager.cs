/*
 * Copyright Tom Morgan 2022
 */

using UnityEngine;

//Functionality for upgrades
public class Upgrade_Manager
{
    //All possible upgrades here
    public enum UpgradeType {Blocks_Per_Tap, Taps_Per_Block, Blocks_Per_Digger, Block_Unlocks };

    //Functionality for the upgrade itself 
    public struct Upgrades
    {
        private ulong upgradeCount;
        private double upgradeCost;
        private UpgradeType upgrade;

        public Upgrades(ulong _upgradeCount, double _upgradeCost, UpgradeType _upgrade)
        {
            upgradeCount = _upgradeCount;
            upgradeCost = _upgradeCost;
            upgrade = _upgrade;
        }

        public ulong Get_Upgrade_Count() => upgradeCount;

        public UpgradeType Get_Upgrade_Type() => upgrade;

        public double Get_Upgrade_Cost() => upgradeCost;

        public void Increase_Upgrade()
        {
            upgradeCount += 1;
            Update_Upgrade_Cost();
            Debug.Log($"Upgrade success {upgrade} Cost: {upgradeCost} Count: {upgradeCount}");
        }

        //Temp
        private void Update_Upgrade_Cost()
        {
            upgradeCost = upgradeCount * 100;
        }
    }

    //A container to hold all of the upgrades and functionality for the upgrade list
    public class UpgradeContainer
    {
        private Upgrades[] upgradeList;

        public UpgradeContainer(int _upgradeCount)
        {
            upgradeList = new Upgrades[_upgradeCount];
           
            for(int i = 0; i < _upgradeCount; i++)
            {
                Upgrades temp = new Upgrades(0, 0, (UpgradeType)i);
                upgradeList[i] = temp;
            }
        }

        public int Get_Upgrade_Index(UpgradeType _upgrade) => (int)_upgrade;

        public bool Try_Upgrade(UpgradeType _upgrade)
        {
            int upgradeID = Get_Upgrade_Index(_upgrade);

            if (Dirt_Numbers.Spend_Dirt(upgradeList[upgradeID].Get_Upgrade_Cost()))
            {
                upgradeList[upgradeID].Increase_Upgrade();
                return true;
            }

            return false;
        }

        public string Print_Upgrade(int _id) => upgradeList[_id].Get_Upgrade_Type().ToString();

        public int Get_Upgrade_Count() => upgradeList.Length;

        public Upgrades Get_Upgrade_From_Type(UpgradeType _u) => upgradeList[Get_Upgrade_Index(_u)];
    }

    //For testing
    private static UpgradeContainer upgradeAccessor;
    public static UpgradeContainer Get_Upgrade_Container() => upgradeAccessor;
    public static void Setup_Upgrades()
    {
        upgradeAccessor = new UpgradeContainer(4);

        for (int i = 0; i < upgradeAccessor.Get_Upgrade_Count(); i ++)
        {
            Debug.Log(upgradeAccessor.Print_Upgrade(i));
        }
    }
}
