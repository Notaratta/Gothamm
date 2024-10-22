using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects.Interface
{
    internal interface IPolice : IPerson
    {
        List<Item> ConfiscatedItems { get; set; }

        void CatchThief(Thief thief);
    }
}
