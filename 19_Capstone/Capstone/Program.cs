using Capstone.Menus;
using System;
using System.IO;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            do
            {
                Console.WriteLine("Please input a text file with the fully qualified file path to stock the vending machine with. \n(Press Enter to use the default)");
                path = Console.ReadLine();
                if (path == "")
                {
                    // If default selected, attempt to find the default inventory stocking file
                    path = @"..\..\..\..\vendingmachine.csv";
                    if(!File.Exists(path))
                    {
                        Console.WriteLine("Error: Could not find default file. Please provide a direct file or contact your system administrator.\n");
                    }
                }
            } while (!File.Exists(path)); // continue until valid path is given or user closes the program

            // create a new menu
            MainMenu myMenu = new MainMenu(path);
            
            myMenu.Run();
        }
    }
}
