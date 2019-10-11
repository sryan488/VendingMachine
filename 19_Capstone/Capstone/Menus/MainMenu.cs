using System;
using System.Collections.Generic;
using System.Text;
using Capstone.Models;

namespace Capstone.Menus
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        public VendingMachine vMachine { get; set; }
        public PurchaseMenu pMenu { get; set; }

        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public MainMenu(string inventoryFilePath) : base()
        {
            this.Title = "*** Vendo-Matic 800 ***";
            this.menuOptions.Add("1", "Display Vending Machine Items");
            this.menuOptions.Add("2", "Purchase");
            this.menuOptions.Add("3", "Exit");
            this.menuOptions.Add("4", "Sales Report");
            vMachine = new VendingMachine(inventoryFilePath);
            pMenu = new PurchaseMenu(vMachine);
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
                    foreach (string itemInList in vMachine.Inventory.ItemList())
                    {
                        Console.WriteLine(itemInList);
                    }
                    Pause("");

                    return true;
                case "2":
                    // Get some input form the user, and then do something
                    pMenu.Run();
                    // Pause("");
                    return true;
                case "3":
                    PurchaseMenu sm = new PurchaseMenu(vMachine);
                    sm.Run();
                    break;
            }
            return true;
        }

    }
}
