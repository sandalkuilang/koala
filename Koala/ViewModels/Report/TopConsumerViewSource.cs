using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System.Collections.Generic;
using Texaco.Database;

namespace Koala.ViewModels.Report
{
    public class TopConsumerViewSource : BaseDashboardViewSource
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

        public TopConsumerViewSource()
        {

        }

        public override void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            List<DataValue> result = db.Query<DataValue>("GetDashboardTopConsumer", new { Year = this.Year });

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
