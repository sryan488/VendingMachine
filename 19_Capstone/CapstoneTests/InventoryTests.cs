using Capstone.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{
    [TestClass]
    public class InventoryTests
    {
        Item reeses = new Item("Reeses", "candy", 3.20M, "A1", 1);
        Item lays = new Item("Lays", "chips", 4.20M, "A2", 1);
        Item cherryCoke = new Item("Cherry Coke", "drink", 7.00m, "C6", 1);
        Item bigChew = new Item("Big League Chew", "gum", 3.40m, "D2", 1);

        [TestMethod]
        public void AddingItems()
        {
            Dictionary<string, Item> resultExpected = new Dictionary<string, Item>()
            {
                { "A1", reeses },
                { "A2", lays },
                { "C6", cherryCoke },
                { "D2", bigChew },
            };

            Inventory inventory = new Inventory();
            inventory.AddItem(reeses);
            inventory.AddItem(lays);
            inventory.AddItem(cherryCoke);
            inventory.AddItem(bigChew);

            CollectionAssert.AreEqual(resultExpected, inventory.Contents, "The inventory contents should be a dictionary with slots and items matching the input items.");

            // add 4 more of each for a total of 5
            for (int i=0; i<4; i++)
            {
                inventory.AddItem(reeses);
                inventory.AddItem(lays);
                inventory.AddItem(cherryCoke);
                inventory.AddItem(bigChew);
            }
            foreach(KeyValuePair<string, Item> kvp in inventory.Contents)
            {
                Assert.AreEqual(5, inventory.Contents[kvp.Key], $"There should be 5 {kvp.Value.Name} items in slot {kvp.Key}.");
            }
            
        }

        [TestMethod]
        public void RemoveItems()
        {
            Inventory inventory = new Inventory();
            for (int i = 0; i < 5; i++)
            {
                inventory.AddItem(reeses);
                inventory.AddItem(lays);
                inventory.AddItem(cherryCoke);
                inventory.AddItem(bigChew);
            }
            foreach (KeyValuePair<string, Item> kvp in inventory.Contents)
            {
                Assert.AreEqual(5, inventory.Contents[kvp.Key].Count, $"There should be 5 {kvp.Value.Name} items in slot {kvp.Key}.");
            }
            inventory.RemoveItem("D2");
            Assert.AreEqual(4, inventory.Contents["D2"].Count, "There should be 4 big chews left.");
            for (int i=0; i<4; i++)
            {
                inventory.RemoveItem("D2"); // remove the last 4 items from D2
            }
            Assert.AreEqual(0, inventory.Contents["D2"].Count, "There should be 0 big chews left.");
            try
            {
                inventory.RemoveItem("D2"); // cannot remove any items if the count is already 0
            }
            catch (ArgumentException)
            {

            }
            
            Assert.AreEqual(0, inventory.Contents["D2"].Count, "There should be 0 big chews left.");
        }
    }
}
