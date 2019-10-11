using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public class TransLog
    {
        public TransLog()
        {
            DirectoryForLogFilesPath = Directory.GetCurrentDirectory() + @"\..\..\..\..";
        }

        public string DirectoryForLogFilesPath { get; set; }
        public string LogPath
        {
            get
            {
                return DirectoryForLogFilesPath + "/Log.txt";
            }
        }
        public string ReportPath
        {
            get
            {
                return DirectoryForLogFilesPath + $"/Vending_Machine_Sales_Report_{Time.ToString(@"yyyyMMdd_HHmmss")}.txt";
            }
        }
        public DateTime Time
        {
            get
            {
                return DateTime.Now; //.ToString(@"MM/dd/yyyy_hh:mm:ss_tt");
            }
        }
        public decimal TotalSales { get; set; } = 0;
        public Dictionary<string, int> ItemsSold { get; set; } = new Dictionary<string, int>();

        public void Writer(string lineOutput, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(lineOutput);
            }
        }
        
        public void LogFeedMoney(decimal machineBalance, decimal moneyAdded)
        {
            string logMoneyIn = $"{Time} {"FEED MONEY:".PadRight(25)} {(machineBalance - moneyAdded):C} + {moneyAdded:C} = {machineBalance:C}";
            Writer(logMoneyIn, LogPath);
        }

        public void LogGiveChange(decimal machineBalance)
        {
            string logMoneyOut = $"{Time.ToString("G")} {"GIVE CHANGE:".PadRight(25)} {machineBalance:C} - {machineBalance:C} = {0:C}";
            Writer(logMoneyOut, LogPath);
        }

        public void LogPurchase(decimal machineBalance, Item itemBought)
        {
            string logItemBought = $"{Time} {itemBought.Name.PadRight(22)} {itemBought.Slot} {(machineBalance + itemBought.Price):C} - {itemBought.Price:C} = {machineBalance:C}";
            Writer(logItemBought, LogPath);
            ItemsSold[itemBought.Name]++; // Update number of items sold in dictionary
            TotalSales += itemBought.Price; // Update total sales amount
        }

        public void GenerateReport()
        {
            foreach(KeyValuePair<string, int> kvp in ItemsSold)
            {
                string line = $"{kvp.Key}|{kvp.Value}";
                Writer(line, ReportPath);
            }
            Writer($"\nTotal Sales: {TotalSales:C}", ReportPath);
        }
    }
}
