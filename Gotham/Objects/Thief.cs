using Gotham.Objects.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gotham.Objects
{
    public class Thief : Person
    {
        public bool IsInPrison { get; set; }
        public List<Item> StolenGoods { get; set; }

        public Thief(int id, (int X, int Y) coordinates, (int X, int Y) direction)
            : base(id, coordinates, direction)
        {
            StolenGoods = new List<Item>();
            IsInPrison = false;
        }

        public void LoseStolenGoods()
        {
            StolenGoods.Clear();
        }

        public List<Item> ShowStolenGoods()
        {
            return new List<Item>(StolenGoods);
        }

        public Item? RobCitizen(Citizen citizen)
        {
            if (IsInPrison) return null;

            Item? stolenItem = citizen.LooseRandomItem();

            if (stolenItem != null)
            {
                StolenGoods.Add(stolenItem);
            }
            return stolenItem;
        }

        public List<Item> SurrenderStolenGoods()
        {
            List<Item> surrenderedGoods = new List<Item>(StolenGoods);
            StolenGoods.Clear();
            return surrenderedGoods;
        }
    }
}
