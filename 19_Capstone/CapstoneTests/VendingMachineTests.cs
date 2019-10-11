using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void AddMoneyTest()
        {
            // Arrange
            VendingMachine vm1 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm2 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm3 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm4 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm5 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");

            // Act
            vm1.AddMoney(1M);
            vm2.AddMoney(5M);
            try
            {
                vm3.AddMoney(-1M);
            }
            catch (ArgumentException e) when (e.Message == "Negative money feed exception.")
            {
                Assert.IsTrue(true);
            }

            try
            {
                vm4.AddMoney(1.11M);
            }
            catch (ArgumentException e) when (e.Message == "Non-integer money fed exception.")
            {
                Assert.IsTrue(true);
            }

            vm5.AddMoney(10M);

            // Assert
            Assert.AreEqual(vm1.FedMoney, 1M);
            Assert.AreEqual(vm2.FedMoney, 5M);
            // vm3 Catch Negative Money Exception
            // vm4 Catch Non-Integer Exception
            Assert.AreEqual(vm5.FedMoney, 10M);
        }

        [TestMethod]
        public void SubtractMoneyTest()
        {
            // Arrange
            VendingMachine vm1 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm2 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm3 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm4 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm5 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");

            // Act
            vm1.SubtractMoney(1M);
            vm2.SubtractMoney(5M);
            vm3.SubtractMoney(-1M);
            vm4.SubtractMoney(1.11M);
            vm5.SubtractMoney(1000000000000M);

            // Assert
            Assert.AreEqual(vm1.FedMoney, -1M);
            Assert.AreEqual(vm2.FedMoney, -5M);
            Assert.AreEqual(vm3.FedMoney, 1M);
            Assert.AreEqual(vm4.FedMoney, -1.11M);
            Assert.AreEqual(vm5.FedMoney, -1000000000000M);
        }

        [TestMethod]
        public void DispenseChangeTest()
        {
            // Arrange
            VendingMachine vm1 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm1.AddMoney(1M);
            VendingMachine vm2 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm2.AddMoney(1M);
            vm2.SubtractMoney(.20M);
            VendingMachine vm3 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm3.AddMoney(1M);
            vm3.SubtractMoney(.90M);
            VendingMachine vm4 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm4.AddMoney(0M);

            // Act
            string actualResult1 = vm1.DispenseChange();
            string actualResult2 = vm2.DispenseChange();
            string actualResult3 = vm3.DispenseChange();
            string actualResult4 = vm4.DispenseChange();

            // Assert
            Assert.AreEqual(actualResult1, "Quarters dispensed: 4\n");
            Assert.AreEqual(actualResult2, "Quarters dispensed: 3\nNickels dispensed: 1\n");
            Assert.AreEqual(actualResult3, "Dimes dispensed: 1\n");
            Assert.AreEqual(actualResult4, "No coins are dispensed");

        }

        //[TestMethod]
        //public void PurchaseTest()
        //{
        //    // Cannot test, only executes methods
        //}

        [TestMethod]
        public void DispenseItemTest()
        {
            // Arrange
            VendingMachine vm1 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm2 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm3 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            VendingMachine vm4 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");

            // Act
            Item item1 = vm1.Inventory.Contents["A1"];
            Item item2 = vm2.Inventory.Contents["B2"];
            Item item3 = vm3.Inventory.Contents["C3"];
            Item item4 = vm4.Inventory.Contents["D4"];

            // Assert
            // Assert.AreEqual(item1, new Item("Potato Crisps", "chipy", 3.05M, "A1", 5));
            Assert.AreEqual(item1.Name, "Potato Crisps");
            Assert.AreEqual(item1.SnackType, "chip");
            Assert.AreEqual(item1.Price, 3.05M);
            Assert.AreEqual(item1.Slot, "A1");
            Assert.AreEqual(item1.Count, 5);

            // Assert.AreEqual(item2, new Item("Cowtales", "candy", 1.50M, "B2", 5));
            Assert.AreEqual(item2.Name, "Cowtales");
            Assert.AreEqual(item2.SnackType, "candy");
            Assert.AreEqual(item2.Price, 1.50M);
            Assert.AreEqual(item2.Slot, "B2");
            Assert.AreEqual(item2.Count, 5);

            // Assert.AreEqual(item3, new Item("Mountain Melter", "drink", 1.50M, "C3", 5));
            Assert.AreEqual(item3.Name, "Mountain Melter");
            Assert.AreEqual(item3.SnackType, "drink");
            Assert.AreEqual(item3.Price, 1.50M);
            Assert.AreEqual(item3.Slot, "C3");
            Assert.AreEqual(item3.Count, 5);

            // Assert.AreEqual(item4, new Item("Triplemint", "gum", 0.75M, "D4", 5));
            Assert.AreEqual(item4.Name, "Triplemint");
            Assert.AreEqual(item4.SnackType, "gum");
            Assert.AreEqual(item4.Price, 0.75M);
            Assert.AreEqual(item4.Slot, "D4");
            Assert.AreEqual(item4.Count, 5);

        }

    }
}
