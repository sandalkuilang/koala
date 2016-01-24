using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using System.Reactive;

namespace Koala.ViewModels
{
    public interface IOrderDetailCommand
    {
        ICommand AddDetailCommand { get; set; } 
        ICommand DeleteDetailCommand { get; set; }
        ICommand EditDetailCommand { get; set; }
    }
}
