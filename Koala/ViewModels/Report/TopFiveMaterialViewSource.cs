using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class TopFiveMaterialViewSource : BaseDashboardViewSource
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

        public TopFiveMaterialViewSource()
        {
    
        }

    }
}
