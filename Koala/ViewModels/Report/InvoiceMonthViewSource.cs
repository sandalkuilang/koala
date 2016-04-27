using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Krokot.Database;
using System.Reactive.Linq;
using System;

namespace Koala.ViewModels.Report
{
    public class InvoiceMonthViewSource : BaseDashboardViewSource
    {
        
        private MutableObservableCollection<SeriesData<InvoiceDataValue>> source;
        public MutableObservableCollection<SeriesData<InvoiceDataValue>> Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }
        private int orderInstallment;
        public int OrderInstallment
        {
            get
            {
                return orderInstallment;
            }
            set
            {
                NotifyIfChanged(ref orderInstallment, value);
            }
        }

        private decimal totalInstallment;
        public decimal TotalInstallment
        {
            get
            {
                return totalInstallment;
            }
            set
            {
                NotifyIfChanged(ref totalInstallment, value);
            }
        }

        private decimal total;
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                NotifyIfChanged(ref total, value);
            }
        }

        private int order;
        public int Order
        {
            get
            {
                return order;
            }
            set
            {
                NotifyIfChanged(ref order, value);
            }
        }
        public InvoiceMonthViewSource()
        {
             
        }

        public override void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            List<InvoiceDataValue> result = db.Query<InvoiceDataValue>("GetDashboardInvoiceMonth", new { Year = this.Year });

            Total = 0;
            Order = 0;
            MutableObservableCollection<SeriesData<InvoiceDataValue>> model = new MutableObservableCollection<SeriesData<InvoiceDataValue>>();
            InvoiceDataValue item;
            for (int i = 1; i <= 12; i++)
            {
                item = result.Where(x => x.Name == i.ToString()).SingleOrDefault();
                model.Add(new SeriesData<InvoiceDataValue>() 
                {
                    SeriesDisplayName  = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
                
                if (item != null)
                { 
                    model[i - 1].Items.Add(new InvoiceDataValue()
                    {
                        Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                        Number = item.Number
                    });

                    Total += item.Number;  
                    Order += item.Order;
                    OrderInstallment = item.OrderInstallment;
                    TotalInstallment = item.TotalInstallment;
                }
                else
                {
                    model[i - 1].Items.Add(new InvoiceDataValue()
                    {
                        Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                        Number = 0
                    });
                } 
            }
            this.Source = model;
            db.Close();
        }

    }
}
