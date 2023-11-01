using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class Food
{
    public string? Name { get; set; }

    public string? ImageSource { get; set; }

    public double Price { get; set; }

    public int Count { get; set; }

    public Food()
    {
        ImageSource += "Assets/MenuImages/DefaultMenuFoodImage.jpg";
    }

    public Food(string? name, double price, string imageSource = "Assets/MenuImages/DefaultMenuFoodImage.jpg", int count = 1)
    {
        Name = name;
        ImageSource = IsImageLink(imageSource) + imageSource;
        Price = price;
        Count = count;
    }

    private string IsImageLink(string ImageUrl)
    {
        Regex regex = new Regex(@"\b(?:https?://|www\.)\S+\.(?:jpg|jpeg|gif|png)\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        if (regex.IsMatch(ImageUrl)) return "";
        else return "pack://application:,,,/Rkeeper;component/";
    }

    public override string ToString() => $"{Name} - {Price} - {Count}";
}
