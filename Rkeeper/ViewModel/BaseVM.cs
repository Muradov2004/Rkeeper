using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Rkeeper.ViewModel;

public abstract class BaseVM : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
    => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}
