using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Rkeeper.Model;

class FoodMenu
{

    public ObservableCollection<Food> MenuFoods { get; set; } = new();

    public FoodMenu()
    {
        JsonToMenu();
    }

    public void MenuToJson()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Menu.json";
        string TableOrderedFoodJson = File.ReadAllText(path);
        string json = JsonConvert.SerializeObject(MenuFoods, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    public void JsonToMenu()
    {
        MenuFoods.Clear();
        string path = AppDomain.CurrentDomain.BaseDirectory[..^25] + @"JsonFiles\Menu.json";
        string MenuJson = File.ReadAllText(path);
        foreach (var item in JsonConvert.DeserializeObject<ObservableCollection<Food>>(MenuJson))
        {
            MenuFoods.Add(item);
        }

    }
}
