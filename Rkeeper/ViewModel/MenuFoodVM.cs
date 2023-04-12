using Rkeeper.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.ViewModel;

internal class MenuFoodVM : BaseVM
{
    private readonly NavigationStore _navigationStore;

    public MenuFoodVM(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
}
