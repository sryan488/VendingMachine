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
        
        public void AddItem(Item item)
        {
            if (!Contents.TryAdd(item.Slot, item))
            {
                Contents[item.Slot].Count++;
            }
            
        }

        /// <summary>
        /// Tries to remove an item from the given slot and return true. If no items were available, returns false.
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
        public List<Item> ItemList()
        {
            List<Item> list = new List<Item>();
            foreach(Item item in Contents.Values)
            {
                list.Add(item);
            }
            return list;
        }
    }
}
