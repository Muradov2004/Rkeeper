using Rkeeper.Model;
using Rkeeper.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.ViewModel;

class OrderFoodVM : BaseVM
{
    public ObservableCollection<Food> OrderedFood { get; set; }

    private readonly NavigationStore _navigation;

    public OrderFoodVM(NavigationStore navigation)
    {
        _navigation = navigation;
        OrderedFood = new FoodMenu().MenuFoods;
    }
}
