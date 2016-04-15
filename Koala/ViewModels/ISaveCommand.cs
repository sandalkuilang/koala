using System.Windows.Input;

namespace Koala.ViewModels
{
    public interface ISaveCommand
    { 
        ICommand AddCommand { get; set; }
        ICommand UpdateCommand { get; set; }
    }
}
