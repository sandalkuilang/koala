using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Koala.ViewModels
{
    public interface ISaveCommand
    { 
        ICommand AddCommand { get; set; }
        ICommand UpdateCommand { get; set; }
    }
}
