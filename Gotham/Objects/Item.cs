﻿using Gotham.Objects.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gotham.Objects
{
    public class Item : IItem
    {
        public string Name { get; set; }

        public Item(string name) {  Name = name; }
    }
}
