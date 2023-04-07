using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rkeeper.Model;

class FoodMenu
{

    public ObservableCollection<Food> Foods { get; set; }

    public FoodMenu()
    {
        Foods = new ObservableCollection<Food>()
        {
            new("Food1",1.0),
            new("Food2",1.0),
            new("Food3",1.0)
        };
    }
}
