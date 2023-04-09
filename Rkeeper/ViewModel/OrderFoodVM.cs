using Rkeeper.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.ViewModel;

class OrderFoodVM
{
    private readonly NavigationStore _navigation;

    public OrderFoodVM(NavigationStore navigation)
    {
        _navigation = navigation;
    }
}
