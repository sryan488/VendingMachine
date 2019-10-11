using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.Menus
{
    public class PurchaseMenu : CLIMenu
    {
        public VendingMachine vMachine { get; set; }

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public PurchaseMenu(VendingMachine vendingMachine ) : base()
        {
            vMachine = vendingMachine;
            this.Title = "*** Purchase Menu ***";
            this.menuOptions.Add("1", "Feed Money");
            this.menuOptions.Add("2", "Select Product");
            this.menuOptions.Add("3", "Finish Transaction");
        }

        override public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(this.Title);
                Console.WriteLine(new string('=', this.Title.Length));
                Console.WriteLine("\r\nPlease make a selection:");
                foreach (KeyValuePair<string, string> menuItem in menuOptions)
                {
                    if (menuItem.Key != "4")
                    {
                        Console.WriteLine($"({menuItem.Key}) {menuItem.Value}");
                    }
                }
                Console.WriteLine($"\nCurrent Money Provided: {vMachine.FedMoney:C}");
                string choice = GetString("Selection:").ToUpper();

                if (menuOptions.ContainsKey(choice))
                {
                    if (!ExecuteSelection(choice))
                    {
                        break;
                    }
                }

            }
        }

        /// <summary>
        /// The override of ExecuteSelection handles whatever selection was made by the user.
        /// This is where any business logic is executed.
        /// </summary>
        /// <param name="choice">"Key" of the user's menu selection</param>
        /// <returns></returns>
        protected override bool ExecuteSelection(string choice)
        {
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please insert money (Only accepts dollar amounts: $1, $2, $5, $10, etc.)");
                    decimal moneyInserted = (int)decimal.Parse(Console.ReadLine());
                    vMachine.AddMoney(moneyInserted);
                    vMachine.TransLog.LogFeedMoney(vMachine.FedMoney, moneyInserted);
                    return true;
                case "2":
                    foreach (string itemInList in vMachine.Inventory.ItemList())
                    {
                        Console.WriteLine(itemInList);
                    }
                    
                    Console.WriteLine("Please enter slot selection:");
                    string slotSelection = Console.ReadLine().ToUpper();
                    if(vMachine.Inventory.Contents[slotSelection].Price > vMachine.FedMoney)
                    {
                        Console.WriteLine($"You don't have enough money inserted. You need at least {vMachine.Inventory.Contents[slotSelection].Price:C} to purchase that item.");
                        Console.ReadLine();
                    }
                    else
                    {
                        vMachine.Purchase(slotSelection);
                        vMachine.TransLog.LogPurchase(vMachine.FedMoney, vMachine.Inventory.Contents[slotSelection]);
                    }
                    return true;
                case "3":
                    vMachine.TransLog.LogGiveChange(vMachine.FedMoney);
                    string coinsgiven = vMachine.DispenseChange();
                    Console.WriteLine(coinsgiven);
                    Console.ReadLine();
                    
                    return false;
            }
            return true;
        }
    }
}
