using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;

namespace Koala.ViewModels
{
    public interface ISelectableGridCommand
    {
        //ICommand CheckedHeaderCommand { get; set; }
        //ICommand CheckedRowCommand { get; set; }
        ReactiveCommand<object> CheckedHeaderCommand { get; set; }
        ReactiveCommand<dynamic> CheckedRowCommand { get; set; }
    }
}
