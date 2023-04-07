using Rkeeper.ViewModel;
using System;

namespace Rkeeper.Stores;

public class NavigationStore
{

    public event Action? CurrentVMChanged;

    private BaseVM? _currentVM;

    public BaseVM? CurrentVM
    {
        get { return _currentVM; }
        set
        {
            _currentVM = value;
            CurrentVMChanged?.Invoke();
        }
    }

}
