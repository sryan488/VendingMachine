using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public class TransLog
    {
        public string DirectoryForLogFilesPath { get; set; } // TODO: find place to set directoryforlogfilespath
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
                return DirectoryForLogFilesPath + $"/{Time.ToString(@"yyyy/MM/dd_hh:mm:ss_tt")} - Vending Machine Cumulative Report.txt";
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
        
        public void LogFeedMoney(decimal moneyIn, int moneyAdded)
        {
            string logMoneyIn = $"{Time} FEED MONEY: {moneyIn:C} + {(decimal)moneyAdded:C} = {moneyIn + moneyAdded:C}";
            Writer(logMoneyIn, LogPath);
        }

        public void LogGiveChange(decimal moneyIn)
        {
            string logMoneyOut = $"{Time.ToString("G")} GIVE CHANGE: {moneyIn:C} - {moneyIn:C} = {0:C}";
            Writer(logMoneyOut, LogPath);
        }

        public void LogPurchase(decimal moneyIn, Item itemBought)
        {
            string logItemBought = $"{Time} {itemBought.Name} {itemBought.Slot} {moneyIn + itemBought.Price:C} - {itemBought.Price:C} = {moneyIn:C}";
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
        }
    }
}
