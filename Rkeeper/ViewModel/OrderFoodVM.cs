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
        OrderedFood = new()
        {
            new("Pizza", 10) { Count = 1 },
            new("Pasta", 12) { Count = 1 },
            new("Steak", 20) { Count = 1 },
            new("Steak", 12) { Count = 1 },
            new("Steak", 11) { Count = 1 },
            new("Steak", 14) { Count = 1 }
        };
    }
}
