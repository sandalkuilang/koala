using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System.Collections.Generic;
using Krokot.Database;
using System;

namespace Koala.ViewModels.Report
{
    public class TopMaterialViewSource : BaseDashboardViewSource
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
 
        public TopMaterialViewSource()
        {
            
        }

        public override void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            List<DataValue> result = db.Query<DataValue>("GetDashboardTopMaterial", new { Year = this.Year });

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
