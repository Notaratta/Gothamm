using System.Collections.Generic;

namespace Gotham.Objects
{
    public class Police : Person
    {
        public List<Item> ConfiscatedItems { get; set; }

        public Police(int id, (int X, int Y) coordinates, (int X, int Y) direction)
            : base(id, coordinates, direction)
        {
            ConfiscatedItems = new List<Item>();
        }

        public void CatchThief(Thief thief)
        {
            ConfiscatedItems.AddRange(thief.SurrenderStolenGoods());
            thief.IsInPrison = true;
        }
    }
}