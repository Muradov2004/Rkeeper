using Rkeeper.ViewModel;
using System;

namespace Rkeeper.Stores;

public class NavigationStore
{

    public event Action? CurrentViewModelChanged;

    private BaseVM? _currentViewModel;

    public BaseVM? CurrentViewModel
    {
        get { return _currentViewModel; }
        set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }

}
