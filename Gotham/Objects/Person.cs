using Gotham.Objects.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects
{
    public class Person : IPerson
    {
        public int Id { get; set; }
        public (int X, int Y) Coordinates { get; set;}

        public (int X, int Y) Direction { get; set; }

        public void Move()
        {
            Coordinates = (Coordinates.X + Direction.X, Coordinates.Y + Direction.Y);
        }

        public Person(int id, (int X, int Y) coordinates, (int X, int Y) direction) 
        { 
            Coordinates = coordinates;
            Direction = direction;
            Id = id;

        }
    }
}
