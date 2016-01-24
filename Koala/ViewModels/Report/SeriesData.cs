using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Report
{
    public class SeriesData<T>
    {
        public string SeriesDisplayName { get; set; }

        public string SeriesDescription { get; set; }

        public MutableObservableCollection<T> Items { get; set; }

        public SeriesData()
        {
            Items = new MutableObservableCollection<T>();
        }
    }

}
