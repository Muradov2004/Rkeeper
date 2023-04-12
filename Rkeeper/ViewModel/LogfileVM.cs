using Rkeeper.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.ViewModel;

internal class LogfileVM : BaseVM
{
    private readonly NavigationStore _navigationStore;

    public LogfileVM(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
    }
}
