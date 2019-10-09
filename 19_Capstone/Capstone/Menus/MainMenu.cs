﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Views
{
    /// <summary>
    /// The top-level menu in our Market Application
    /// </summary>
    public class MainMenu : CLIMenu
    {
        /// <summary>
        /// Constructor adds items to the top-level menu
        /// </summary>
        public MainMenu() : base()
        {
            this.Title = "*** Main Menu ***";
            this.menuOptions.Add("1", "Option One");
            this.menuOptions.Add("2", "Add two numbers");
            this.menuOptions.Add("3", "Display a sub-menu");
            this.menuOptions.Add("Q", "Quit");
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
                    // This is just a sample, does nothing
                    return true;
                case "2":
                    // Get some input form the user, and then do something
                    int someNumber = GetInteger("Please enter a whole number:");
                    int anotherNumber = GetInteger("Please enter another whole number:");
                    Console.WriteLine($"{someNumber} + {anotherNumber} = {someNumber + anotherNumber}.");
                    Pause("");
                    return true;
                case "3":
                    SubMenu sm = new SubMenu();
                    sm.Run();
                    break;
            }
            return true;
        }

    }
}
