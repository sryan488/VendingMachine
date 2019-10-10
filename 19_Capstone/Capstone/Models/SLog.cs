using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Models
{
    public class TransLog
    {
        public string ReportPath { get; }
        public string LogPath { get; }
        public Dictionary<string, int> ItemsSold { get; set; } = new Dictionary<string, int>();

        string time = DateTime.Now.ToString(@"MM/dd/yyyy hh:mm:ss tt");
        decimal totalSales = 0;

        public void Writer (string lineOutput, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(lineOutput);
            }
        }
        
        public void LogFeedMoney(decimal moneyIn, int moneyAdded)
        {
            string logMoneyIn = $"{time} FEED MONEY: {moneyIn:C} + {(decimal)moneyAdded:C} = {moneyIn + moneyAdded:C}";
            Writer(logMoneyIn, LogPath);
        }

        public void LogGiveChange(decimal moneyIn)
        {
            string logMoneyOut = $"{time} GIVE CHANGE: {moneyIn:C} - {moneyIn:C} = {0:C}";
            Writer(logMoneyOut, LogPath);
        }

        public void LogPurchase(decimal moneyIn, Item itemBought)
        {
            string logItemBought = $"{time} {itemBought.Name} {itemBought.Slot} {moneyIn + itemBought.Price:C} - {itemBought.Price:C} = {moneyIn:C}";
            Writer(logItemBought, LogPath);
            ItemsSold[itemBought.Name]++; // Update number of items sold in dictionary
            totalSales += itemBought.Price; // Update total sales amount
        }


    }
}
