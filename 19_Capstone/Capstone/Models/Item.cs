using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    /// <summary>
    /// Constructor
    /// Set name, snackType, price, slot, count
    /// </summary>
    public class Item
    {
        public Item(string name, string snackType, decimal price, string slot, int count)
        {
            Name = name;
            SnackType = snackType;
            Price = price;
            Slot = slot;
            Count = count;
        }

        /// <summary>
        /// Properties
        /// Define fields Name, SnackType, Price, Slot, EatResponse
        /// </summary>
        public string Name { get; }
        public string SnackType { get; }
        public decimal Price { get; }
        public string Slot { get; }
        public string EatResponse
        {
            get
            {
                switch(SnackType)
                {
                    case "chips":
                        return "Crunch Crunch, Yum!";
                    case "candy":
                        return "Munch Munch, Yum!";
                    case "drink":
                        return "Glug Glug, Yum!";
                    case "gum":
                        return "Chew Chew, Yum!";
                    default:
                        throw new ArgumentException("Cannot get eat response from an invalid snack type.");
                }

            }
        }
        public int Count { get; set; }
    }
}
