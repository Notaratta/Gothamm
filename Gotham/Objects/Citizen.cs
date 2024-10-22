using Gotham.Objects.Interface;
using Gotham.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects
{
    public class Citizen : Person, ICitizen
    {
        public List<Item> Items { get; set; }

        public Citizen(int id, (int X, int Y) coordinates, (int X, int Y) direction)
            : base(id, coordinates, direction)
        {
            Items = new List<Item>
            {
                new Item("Watch"),
                new Item("Phone"),
                new Item("Key"),
                new Item("Wallet")
            };
        }

        public List<Item> ShowItems()
        {
            return Items;
        }
         
        public Item? LooseRandomItem()
        {   
            if (Items.Count > 0){
                int index = new Random().Next(Items.Count);
                Item item = Items[index];
                Items.RemoveAt(index);
                return item; 
            }
            return null;
        }

        public void GetBroke()
        {
            throw new NotImplementedException();
        }

    }
}
