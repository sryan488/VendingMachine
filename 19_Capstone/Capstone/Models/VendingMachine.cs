using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    /// <summary>
    /// Constructor
    /// Get Inventory from inventory file 'vendingmachine.csv'
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
    }
}
