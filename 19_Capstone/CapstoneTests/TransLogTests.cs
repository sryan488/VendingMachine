using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;
using System.IO;

namespace CapstoneTests
{
    [TestClass]
    public class TransLogTests
    {
        [TestMethod]
        public void CreateLogFile()
        {
            // Instantiate new log and clear any existing Log.txt file
            TransLog log = new TransLog();
            if (File.Exists(log.LogPath)) { File.Delete(log.LogPath); }
            Assert.IsFalse(File.Exists(log.LogPath), "No log file should exist yet at the target location.");

            // Create a new file
            string sampleTextA = "Hello, world!";
            log.Writer(sampleTextA, log.LogPath);

            // Was the file created?
            Assert.IsTrue(File.Exists(log.LogPath), "Log file should have been created.");

            // Cleanup
            File.Delete(log.LogPath);
        }

        [TestMethod]
        public void WritingToFile()
        {
            // Make a new log file and write to it
            TransLog log = new TransLog();
            if (File.Exists(log.LogPath)) { File.Delete(log.LogPath); }
            Assert.IsFalse(File.Exists(log.LogPath), "No log file should exist yet at the target location.");
            string sampleTextA = "Hello, world!";
            log.Writer(sampleTextA, log.LogPath);

            // Does it have the expected text?
            using (StreamReader sr = new StreamReader(log.LogPath))
            {
                while(!sr.EndOfStream)
                {
                    Assert.AreEqual(sampleTextA, sr.ReadLine(), "File should contain: Hello, world!");
                }
            }

            // Can new text append?
            string sampleTextB = "My name is Log.txt, nice to meet you.";
            log.Writer(sampleTextB, log.LogPath);
            using (StreamReader sr = new StreamReader(log.LogPath))
            {
                Assert.AreEqual(sampleTextA, sr.ReadLine(), "File should have sampleTextA on first line");
                Assert.AreEqual(sampleTextB, sr.ReadLine(), "File should have sampleTextB on second line");
            }

            File.Delete(log.LogPath);
        }

        [TestMethod]
        public void FeedMoneyLogging()
        {
            // Setup new log and the money amounts
            TransLog log = new TransLog();
            File.Delete(log.LogPath);
            decimal balance = 10.00m;
            decimal moneyAdded = 5.00m;

            // Log the MONEY FED operation
            DateTime time = log.Time;
            log.LogFeedMoney(balance, moneyAdded);
            using (StreamReader sr = new StreamReader(log.LogPath))
            {
                string expected = $"{time} {"FEED MONEY:".PadRight(25)} {(balance - moneyAdded):C} + {moneyAdded:C} = {balance:C}";
                Assert.AreEqual(expected, sr.ReadLine(), "Log text should match expected string.");
            }
            File.Delete(log.LogPath);
        }

        [TestMethod]
        public void DispenseChangeLogging()
        {
            // Setup new log and the money amounts
            TransLog log = new TransLog();
            File.Delete(log.LogPath);
            decimal balance = 10.00m;

            // Log the GIVE CHANGE operation
            DateTime time = log.Time;
            log.LogGiveChange(balance);
            using (StreamReader sr = new StreamReader(log.LogPath))
            {
                string expected = $"{time.ToString("G")} {"GIVE CHANGE:".PadRight(25)} {balance:C} - {balance:C} = {0:C}";
                Assert.AreEqual(expected, sr.ReadLine(), "Log text should match expected string.");
            }
            File.Delete(log.LogPath);
        }

        [TestMethod]
        public void PurchaseLogging()
        {
            // Setup new log and the money amounts
            TransLog log = new TransLog();
            File.Delete(log.LogPath);
            decimal balance = 10.00m;

            // Add a coke to the log inventory
            Item coke = new Item("Coca Cola", "drink", 2.35m, "A2", 1);
            log.ItemsSold.Add(coke.Name, 0); // starts with 0 sold

            // Log the GIVE CHANGE operation
            DateTime time = log.Time;
            log.LogPurchase(balance,coke);
            using (StreamReader sr = new StreamReader(log.LogPath))
            {
                string expected = $"{time} {coke.Name.PadRight(22)} {coke.Slot} {(balance + coke.Price):C} - {coke.Price:C} = {balance:C}";
                Assert.AreEqual(expected, sr.ReadLine(), "Log text should match expected string.");
            }
            File.Delete(log.LogPath);
        }

        [TestMethod]
        public void CumulativeReportGeneration()
        {
            // Create new log for a vending machine that sells only coke
            TransLog log = new TransLog();
            Item coke = new Item("Coca Cola", "drink", 2.35m, "A2", 1);
            log.ItemsSold.Add(coke.Name, 0); // starts with 0 sold

            // log a sale
            decimal balance = 10.00m;
            log.LogPurchase(balance, coke);

            // Clear files before writing
            File.Delete(log.LogPath); // don't need the log file for this test
            if (File.Exists(log.ReportPath)) { File.Delete(log.ReportPath); }

            // generate the report
            log.GenerateReport();

            // Does the report exist?
            Assert.IsTrue(File.Exists(log.ReportPath), "Report file should be created.");

            // Does it have the expected info and formatting inside?
            string expected = "Coca Cola|1";
            using (StreamReader sr = new StreamReader(log.ReportPath))
            {
                Assert.AreEqual(expected, sr.ReadLine(), "Report should contain the expected output.");
            }
            File.Delete(log.ReportPath);
        }
    }
}
