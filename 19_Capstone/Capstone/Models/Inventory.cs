using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    public class Inventory
    {
        public Dictionary<string, Item> Contents { get; private set; } = new Dictionary<string, Item>();

        public bool SlotHasItems(string slot)
        {
            return Contents[slot].Count != 0;
        }
        
        /// <summary>
        /// Adds an item to the Contents.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            Contents.Add(item.Slot, item);
        }

        /// <summary>
        /// Tries to remove an item from the given slot.
        /// </summary>
        /// <param name="slot">The vending machine slot to try to remove.</param>
        /// <returns></returns>
        public void RemoveItem(string slot)
        {
            if(Contents[slot].Count > 0)
            {
                Contents[slot].Count--;
            }
            else
            {
                throw new ArgumentException("No items remaining in the given slot.");
            }
        }

        /// <summary>
        /// Returns a list of the Item objects remaining in the inventory.
        /// </summary>
        /// <returns></returns>
        public List<string> ItemList()
        {
            List<string> list = new List<string>();
            list.Add("Slot  Item Name            Price  Avail");

            foreach (Item item in Contents.Values)
            {
                //list.Add(item);
                string displayString = $"{item.Slot}:   {item.Name.PadRight(20)} {item.Price.ToString("C")}  ({(item.Count > 0 ? item.Count.ToString() : "Sold Out")})";
                list.Add(displayString);
            }
            list.Sort();
            return list;
        }
    }
}
