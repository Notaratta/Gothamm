using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects.Interface {

public interface IPerson
    {
        int Id { get; set; }
        (int X, int Y) Coordinates { get; set; }
        (int X, int Y) Direction { get; set; }

        void Move();
    } 
}
