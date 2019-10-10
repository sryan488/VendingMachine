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
        /// Adds an item to the Contents. If the item already exists in the slot, increments the item count by 1.
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
            foreach(Item item in Contents.Values)
            {
                //list.Add(item);
                string displayString = $"{item.Slot}: {item.Name} {item.Price.ToString():C} ({(item.Count > 0 ? item.Count.ToString() : "Sold Out")})";
                list.Add(displayString);
            }
            return list;
        }
    }
}
