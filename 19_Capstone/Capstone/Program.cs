using Capstone.Menus;
using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input a text file with the fully qualified file path to stock the vending machine with. \n(Press Enter to use the default)");
            string path = Console.ReadLine();
            if (path == "")
            {
                path = @"C:\Users\KPartezana\Git\c-module-1-capstone-team-8\19_Capstone\vendingmachine.csv";
            }
            MainMenu myMenu = new MainMenu(path);
            myMenu.Run();
        }
    }
}
