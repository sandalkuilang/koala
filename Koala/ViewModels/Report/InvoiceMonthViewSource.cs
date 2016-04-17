using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Krokot.Database;
using System.Reactive.Linq;

namespace Koala.ViewModels.Report
{
    public class InvoiceMonthViewSource : BaseDashboardViewSource
    {
        
        private MutableObservableCollection<SeriesData<DataValue>> source;
        public MutableObservableCollection<SeriesData<DataValue>> Source
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
 
        public InvoiceMonthViewSource()
        {
             
        }

        public override void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            List<DataValue> result = db.Query<DataValue>("GetDashboardInvoiceMonth", new { Year = this.Year });

            MutableObservableCollection<SeriesData<DataValue>> model = new MutableObservableCollection<SeriesData<DataValue>>();
            DataValue item;
            for (int i = 1; i <= 12; i++)
            {
                item = result.Where(x => x.Name == i.ToString()).SingleOrDefault();
                model.Add(new SeriesData<DataValue>() 
                {
                    SeriesDisplayName  = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
                
                if (item != null)
                {
                    model[i - 1].Items.Add(new DataValue()
                    {
                        Name = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                        Number = item.Number
                    });
                }
                else
                {
                    model[i - 1].Items.Add(new DataValue()
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
