﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class Food
{
    public string? Name { get; set; }
    public string? ImageSource { get; set; }
    public int Price { get; set; }
    public int Count { get; set; }
}