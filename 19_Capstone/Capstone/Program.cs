using Capstone.Menus;
using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            MainMenu myMenu = new MainMenu(@"C:\Users\KPartezana\Git\c-module-1-capstone-team-8\19_Capstone\vendingmachine.csv");
            myMenu.Run();
        }
    }
}
