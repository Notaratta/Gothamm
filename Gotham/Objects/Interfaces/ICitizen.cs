using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects.Interface
{
    interface ICitizen
    {
        List<Item> Items { get; set; }

        Item? LooseRandomItem();
        void GetBroke();
        List<Item> ShowItems();
    }
}
