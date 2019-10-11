using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        Item reeses = new Item("Reeses", "candy", 3.20M, "A1", 5);
        Item lays = new Item("Lays", "chip", 4.20M, "A2", 4);
        Item cherryCoke = new Item("Cherry Coke", "drink", 7.00m, "C6", 5);
        Item bigChew = new Item("Big League Chew", "gum", 3.40m, "D2", 0);

        [TestMethod]
        public void EatResponseTests()
        {
            Assert.AreEqual("Munch Munch, Yum!", reeses.EatResponse);
            Assert.AreEqual("Crunch Crunch, Yum!", lays.EatResponse);
            Assert.AreEqual("Glug Glug, Yum!", cherryCoke.EatResponse);
            Assert.AreEqual("Chew Chew, Yum!", bigChew.EatResponse);
        }
    }
}
