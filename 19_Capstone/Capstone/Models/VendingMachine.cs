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

                        // TODO: Update SLog.ItemsSold dictionary with item name and 0 sold
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Inventory File Issue: {ex.Message}");
                    }
                }
            }
            
        }

        public void AddMoney(decimal moneyFed)
        {
            FedMoney += moneyFed;
            //TransLog.LogFeedMoney(FedMoney, moneyFed);
        }

        public void SubtractMoney(decimal moneyUsed)
        {
            FedMoney -= moneyUsed;
        }

        public string DispenseChange()
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

            //TransLog.LogGiveChange(FedMoney);
            return coinsGiven;
        }

        public void Purchase(string slot)
        {
            if (!Inventory.Contents.ContainsKey(slot))
            {
                Console.WriteLine("That product code does not exist");
                Console.ReadLine();
            }
            else if (Inventory.Contents[slot].Count == 0)
            {
                Console.WriteLine("That item is SOLD OUT");
                Console.ReadLine();
            }
            else
            {
                DispenseItem(Inventory.Contents[slot]);
            }

        }

        public void DispenseItem(Item item)
        {
            Console.WriteLine($"Dispensing: {item.Name}");
            Console.WriteLine($"Spent: {item.Price}");
            Console.WriteLine($"Money remaining: {FedMoney - item.Price}");
            Console.WriteLine($"{item.EatResponse}");
            Console.ReadLine();

            item.Count--;
            SubtractMoney(item.Price);
        }
    }
}
