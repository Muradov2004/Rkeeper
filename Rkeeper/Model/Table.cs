using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Rkeeper.Model;

enum TableShape
{
    Rectangle,
    Circle,
    Square
}

class Table
{
    public string? Name { get; set; }
    public int NumberOfChairs { get; set; }
    public TableShape ShapeOfTable { get; set; }
}
