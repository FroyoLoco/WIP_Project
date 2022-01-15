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
    private struct Upgrades
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

        public UpgradeType GetUpgradeType() => upgrade;

        public double GetUpgradeCost() => upgradeCost;

        public void IncreaseUpgrade()
        {
            upgradeCount += 1;
            UpdateUpgradeCost();
            Debug.Log($"Upgrade success {upgrade} Cost: {upgradeCost} Count: {upgradeCount}");
        }

        //Temp
        private void UpdateUpgradeCost()
        {
            upgradeCost = upgradeCount * 100;
        }
    }

    //A container to hold all of the upgrades and functionality for the upgrade list
    public struct UpgradeContainer
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

        public int GetUpgradeIndex(UpgradeType _upgrade) => (int)_upgrade;

        public bool TryUpgrade(UpgradeType _upgrade)
        {
            int upgradeID = GetUpgradeIndex(_upgrade);

            if (Dirt_Numbers.Spend_Dirt(upgradeList[upgradeID].GetUpgradeCost()))
            {
                upgradeList[upgradeID].IncreaseUpgrade();
                return true;
            }

            return false;
        }

        public string PrintUpgrade(int _id) => upgradeList[_id].GetUpgradeType().ToString();

        public int GetUpgradeCount() => upgradeList.Length;
    }

    //For testing
    public static UpgradeContainer upgradeAccessor;
    public static void SetupUpgrades()
    {
        upgradeAccessor = new UpgradeContainer(4);

        for (int i = 0; i < upgradeAccessor.GetUpgradeCount(); i ++)
        {
            Debug.Log(upgradeAccessor.PrintUpgrade(i));
        }
    }
}
