using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Order
{
    public class CreateOrderDetailCollectionViewSource : BaseOrderCollectionViewModel<CreateOrderDetailModel>
    {
        private MutableObservableCollection<CreateOrderDetailModel> source;
        public delegate void OrderDetailEventHandler(object sender, MutableObservableCollection<CreateOrderDetailModel> e);

        public event OrderDetailEventHandler DataChanged;
        public override MutableObservableCollection<CreateOrderDetailModel> Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
                OnDataChanged(value);
            }
        } 
         
        public CreateOrderDetailCollectionViewSource()
        {
            Source = new MutableObservableCollection<CreateOrderDetailModel>();
        }

        private void OnDataChanged(object arg)
        {
            if (DataChanged != null)
                DataChanged(this, this.Source);
        }

        public override void OnEditCommand(dynamic obj)
        {
            throw new NotImplementedException();
        }

        public override void OnDelete(dynamic obj)
        {
            throw new NotImplementedException();
        }

        public override void OnPrint(dynamic obj)
        {
            throw new NotImplementedException();
        }
    }
}
