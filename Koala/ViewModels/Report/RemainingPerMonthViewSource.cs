using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;

namespace Koala.ViewModels.Report
{
    public class RemainingPerMonthViewSource : BaseDashboardViewSource
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

        public RemainingPerMonthViewSource()
        {

        }

        public override void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            List<DataValue> result = db.Query<DataValue>("GetDashboardRemainingPerMonth", new { Year = this.Year, Month = this.Month }); 

            MutableObservableCollection<SeriesData<DataValue>> model = new MutableObservableCollection<SeriesData<DataValue>>(); 
            for (int i = 0; i < result.Count; i++)
            {
                model.Add(new SeriesData<DataValue>()
                {
                    SeriesDisplayName = result[i].Name
                });

                model[i].Items.Add(new DataValue()
                {
                    Name = result[i].Name,
                    Number = result[i].Number
                });
            }
            this.Source = model;
            db.Close();
        }
    }
}
