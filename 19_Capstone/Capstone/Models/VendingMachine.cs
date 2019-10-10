using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    /// <summary>
    /// Properties
    /// </summary>
    public class VendingMachine
    {
        public Inventory Inventory { get; private set; } = new Inventory();
        public decimal FedMoney { get; private set; }

        /// <summary>
        /// Constructor of new Vending Machine object
        /// Needs an inventory file to build
        /// </summary>
        /// <param name="inventoryFile"></param>
        public VendingMachine(string inventoryFile)
        {
            using (StreamReader sr = new StreamReader(inventoryFile))
            {
                while (!sr.EndOfStream)
                {
                    try
                    {
                        string inventoryLine = sr.ReadLine();
                        string[] itemFields;
                        itemFields = inventoryLine.Split("|");
                        Item inputItem = new Item(itemFields[1], itemFields[3], decimal.Parse(itemFields[2]), itemFields[0], 5);
                        Inventory.AddItem(inputItem);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Inventory File Issue: {ex.Message}");
                    }
                }
            }
            
        }

        private void AddMoney(int moneyFed)
        {
            FedMoney += moneyFed;
            // TODO Add entry to log
        }

        private void SubtractMoney(int moneyUsed)
        {
            FedMoney -= moneyUsed;
        }

        private string DispenseChange()
        {
            string coinsGiven = "";
            Dictionary<string, int> changeDict = new Dictionary<string, int>()
            {
                {"Quarters", 0 },
                {"Dimes", 0 },
                {"Nickles", 0 },
            };
            while(FedMoney >= .25M)
            {
                changeDict["Quarters"]++;
                FedMoney -= .25M;
            }
            while(FedMoney >= .10M)
            {
                changeDict["Dimes"]++;
                FedMoney -= .10M;
            }
            while(FedMoney >= .05M)
            {
                changeDict["Nickles"]++;
                FedMoney -= .05M;
            }
            foreach(KeyValuePair<string, int> coin in changeDict)
            {
                if (changeDict[coin.Key] > 0)
                {
                    coinsGiven += $"{coin.Key} dispensed: {coin.Value}\n";
                }
            }

            if (coinsGiven == "")
            {
                coinsGiven = "No coins are dispensed";
            }
            // TODO Add entry to log
            return coinsGiven;
        }

    }
}
