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
        /// <summary>
        /// An inventory containing all the items in the vending machine.
        /// </summary>
        public Inventory Inventory { get; private set; } = new Inventory();
        /// <summary>
        /// A log of all vending machine transactions. Can update log text file and generate a cumulative report.
        /// </summary>
        public TransLog TransLog { get; private set; } = new TransLog();
        /// <summary>
        /// Amount of money currently fed into the vending machine.
        /// </summary>
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
                        // Read in the item properties from the input file
                        string inventoryLine = sr.ReadLine();
                        string[] itemFields;
                        itemFields = inventoryLine.Split("|");
                        
                        // Create a new item with those properties
                        Item inputItem = new Item(itemFields[1], itemFields[3], decimal.Parse(itemFields[2]), itemFields[0], 5);

                        // Add the item to the inventory
                        Inventory.AddItem(inputItem);

                        TransLog.ItemsSold.TryAdd(inputItem.Name, 0);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Inventory File Issue: {ex.Message}");
                    }
                }
            }
            
        }

        /// <summary>
        /// Add money to the "FedMoney" machine balance.
        /// </summary>
        /// <param name="moneyFed"></param>
        public void AddMoney(decimal moneyFed)
        {
            if ((decimal)(int)moneyFed != moneyFed) // Non-whole amount
            {
                throw new ArgumentException("Non-integer money fed exception.");
            }
            else if (moneyFed < 0) // Negative numbers
            {
                throw new ArgumentException("Negative money feed exception.");
            }
            else
            {
                FedMoney += moneyFed;
            }
        }
        /// <summary>
        /// Subtracts money from the "FedMoney" machine balance.
        /// </summary>
        /// <param name="moneyUsed"></param>
        public void SubtractMoney(decimal moneyUsed)
        {
            FedMoney -= moneyUsed;
        }
        /// <summary>
        /// Returns the user's change using the least number of coins.
        /// </summary>
        /// <returns></returns>
        public string DispenseChange()
        {
            string coinsGiven = "";
            Dictionary<string, int> changeDict = new Dictionary<string, int>()
            {
                {"Quarters", 0 },
                {"Dimes", 0 },
                {"Nickels", 0 },
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
                changeDict["Nickels"]++;
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

            return coinsGiven;
        }
        /// <summary>
        /// If the given slot is valid, makes transaction and dispenses item.
        /// </summary>
        /// <param name="slot"></param>
        /// <returns></returns>
        public Item Purchase(string slot)
        {
            if (!Inventory.Contents.ContainsKey(slot))
            {
                throw new ArgumentException("Invalid slot.");
            }
            else if (Inventory.Contents[slot].Count <= 0)
            {
                throw new Exception("Out of stock exception.");
            }
            else if (Inventory.Contents[slot].Price > FedMoney)
            {
                throw new Exception("You're broke!");
            }
            else
            {
                Item item = Inventory.Contents[slot];
                DispenseItem(item);
                TransLog.LogPurchase(FedMoney, item);
                return item;
            }
        }
        /// <summary>
        /// Dispenses an item from the inventory and removes the cost from the machine balance.
        /// </summary>
        /// <param name="item"></param>
        public void DispenseItem(Item item)
        {
            item.Count--;
            SubtractMoney(item.Price);
        }
    }
}
