﻿using Rkeeper.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.ViewModel;

public class MainVM : BaseVM
{

    private readonly NavigationStore _navigationStore;
    public BaseVM? CurrentVM => _navigationStore.CurrentVM;

    public MainVM(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
        navigationStore.CurrentVMChanged += NavigationStore_CurrentVMChanged;
    }

    private void NavigationStore_CurrentVMChanged()
    => NotifyPropertyChanged(nameof(CurrentVM));

}
