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
            vm3.AddMoney(-1M);
            vm4.AddMoney(1.11M);
            vm5.AddMoney(1000000000000M);

            // Assert
            Assert.AreEqual(vm1.FedMoney, 1M);
            Assert.AreEqual(vm2.FedMoney, 5M);
            Assert.AreEqual(vm3.FedMoney, -1M);
            Assert.AreEqual(vm4.FedMoney, 1.11M);
            Assert.AreEqual(vm5.FedMoney, 1000000000000M);
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
            vm2.AddMoney(1.15M);
            VendingMachine vm3 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm3.AddMoney(.80M);
            VendingMachine vm4 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm4.AddMoney(.10M);
            VendingMachine vm5 = new VendingMachine(@"..\..\..\..\vendingmachine.csv");
            vm5.AddMoney(0M);

            // Act
            string actualResult1 = vm1.DispenseChange();
            string actualResult2 = vm2.DispenseChange();
            string actualResult3 = vm3.DispenseChange();
            string actualResult4 = vm4.DispenseChange();
            string actualResult5 = vm5.DispenseChange();

            // Assert
            Assert.AreEqual(actualResult1, "Quarters dispensed: 4\n");
            Assert.AreEqual(actualResult2, "Quarters dispensed: 4\nDimes dispensed: 1\nNickels dispensed: 1\n");
            Assert.AreEqual(actualResult3, "Quarters dispensed: 3\nNickels dispensed: 1\n");
            Assert.AreEqual(actualResult4, "Dimes dispensed: 1\n");
            Assert.AreEqual(actualResult5, "No coins are dispensed");

        }

        //[TestMethod]
        //public void PurchaseTest()
        //{
        //    // Cannot test, only executes methods
        //}

        [TestMethod]
        public void DispenseItemTest()
        {

        }
    }
}
