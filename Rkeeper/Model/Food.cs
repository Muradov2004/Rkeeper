using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class Food
{

    public string? Name { get; set; }

    public string? ImageSource { get; set; } = "pack://application:,,,/Rkeeper;component/";

    public double Price { get; set; }

    public int Count { get; set; }

    public Food(string? name, double price, string imageSource = "Assets/MenuImages/DefaultMenuFoodImage.jpg")
    {
        Name = name;
        ImageSource += imageSource;
        Price = price;
    }

    public override string ToString() => $"{Name} - {Price} - {Count}";
}
