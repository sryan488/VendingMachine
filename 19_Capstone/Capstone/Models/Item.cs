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
        /// <summary>
        /// Constructor for the item class.
        /// </summary>
        /// <param name="name">Name of the item.</param>
        /// <param name="snackType">Chips, drink, candy, gum.</param>
        /// <param name="price">Price of item (decimal).</param>
        /// <param name="slot">Slot holding item (string format of capital letter and number).</param>
        /// <param name="count">Count of item in slot.</param>
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
                    case "Chip":
                        return "Crunch Crunch, Yum!";
                    case "Candy":
                        return "Munch Munch, Yum!";
                    case "Drink":
                        return "Glug Glug, Yum!";
                    case "Gum":
                        return "Chew Chew, Yum!";
                    default:
                        throw new ArgumentException("Cannot get eat response from an invalid snack type.");
                }

            }
        }
        public int Count { get; set; }
    }
}
