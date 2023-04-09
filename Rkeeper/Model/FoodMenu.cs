using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class FoodMenu
{

    public ObservableCollection<Food> MenuFoods { get; set; }

    public FoodMenu()
    {
        MenuFoods = new ObservableCollection<Food>()
        {
            new("Chicken Burger" ,8.99 ,"Assets/MenuImages/ChickenBurger.jpg"),
            new("Caesar Salad" ,10.50 ,"Assets/MenuImages/CeaserSalad.jpg"),
            new("Margherita Pizza" ,12.95 ,"Assets/MenuImages/MargheritaPizza.jpg"),
            new("Fish and Chips" ,14.99 ,"Assets/MenuImages/FishChips.jpg"),
            new("Pepperoni Pizza" ,9.75 ,"Assets/MenuImages/PepperoniPizza.jpg"),
            new("Pad Thai" ,11.25 ,"Assets/MenuImages/PadThai.jpg"),
            new("Grilled Cheese Sandwich" ,6.50 ,"Assets/MenuImages/GrilledCheeseSandwich.jpg"),
            new("Spaghetti Bolognese" ,13.75 ,"Assets/MenuImages/SpaghettiBolognese.jpg"),
            new("Buffalo Wings" ,8.25 ,"Assets/MenuImages/BuffaloWings.jpg"),
            new("Sushi Roll" ,9.95 ,"Assets/MenuImages/SushiRoll.jpg"),
        };
    }

}
