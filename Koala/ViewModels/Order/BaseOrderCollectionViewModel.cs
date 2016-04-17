using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Krokot.Database;

namespace Koala.ViewModels.Order
{
    public abstract class BaseOrderCollectionViewModel<T> : BaseCollectionViewModel<T>, IEditGridCommand, IInvoiceCommand where T : BaseGridRow
    {
        private bool enableContextMenu;
        public bool EnableContextMenu
        {
            get
            {
                enableContextMenu = Source.Count > 0;
                return enableContextMenu;
            }
            set
            {
                NotifyIfChanged(ref enableContextMenu, value);
            }
        }

        public System.Windows.Input.ICommand EditCommand { get; set; }

        public System.Windows.Input.ICommand DeleteCommand { get; set; }

        public System.Windows.Input.ICommand PrintCommand { get; set; }
          

        public BaseOrderCollectionViewModel()
        {
            EditCommand = new DelegateCommand<object>(new Action<object>(OnEditCommand));
            DeleteCommand = new DelegateCommand<object>(new Action<object>(OnDelete));
            PrintCommand = new DelegateCommand<object>(new Action<object>(OnPrint));  
        }

        public abstract void OnEditCommand(object obj);

        public virtual void OnDelete(object obj)
        {
            CheckedHeader = false;
            EnableContextMenu = true;
        }

        public abstract void OnPrint(object obj);

         
    }
}
