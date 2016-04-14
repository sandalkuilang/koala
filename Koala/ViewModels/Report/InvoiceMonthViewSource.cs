using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Texaco.Database;

using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;

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
        private int year;
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                NotifyIfChanged(ref year, value); 
            }
        }

        private double fontSize = 11.0;
        public double SelectedFontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                NotifyIfChanged(ref fontSize, value);
            }
        }

        private object selectedItem = null;
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                NotifyIfChanged(ref selectedItem, value);
            }
        }

        private bool darkLayout = false;
        public bool DarkLayout
        {
            get
            {
                return darkLayout;
            }
            set
            {
                darkLayout = value;
                OnPropertyChanged("DarkLayout");
                OnPropertyChanged("Foreground");
                OnPropertyChanged("Background");
                OnPropertyChanged("MainBackground");
                OnPropertyChanged("MainForeground");
            }
        }

        private object selectedPalette = null;
        public object SelectedPalette
        {
            get
            {
                return selectedPalette;
            }
            set
            {
                NotifyIfChanged(ref selectedPalette, value);
            }
        }

        public string Foreground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FFEEEEEE";
                }
                return "#FF666666";
            }
        }
        public string MainForeground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FFFFFFFF";
                }
                return "#FF666666";
            }
        }
        public string Background
        {
            get
            {
                if (darkLayout)
                {
                    return "#FF333333";
                }
                return "#FFF9F9F9";
            }
        }
        public string MainBackground
        {
            get
            {
                if (darkLayout)
                {
                    return "#FF000000";
                }
                return "#FFEFEFEF";
            }
        }

        private List<int> years;
        public List<int> Years
        {
            get
            {
                return years;
            }
            set
            {
                NotifyIfChanged(ref years, value);
            }
        }

        public InvoiceMonthViewSource()
        {
            this.Year = DateTime.Now.Year;
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            Years = db.Query<int>("GetDistinctYear");
            db.Close();
            Load();
        }

        public void Load()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            List<InvoiceDataValue> result = db.Query<InvoiceDataValue>("GetDashboardInvoiceMonth", new { Year = this.Year });

            MutableObservableCollection<SeriesData<InvoiceDataValue>> model = new MutableObservableCollection<SeriesData<InvoiceDataValue>>();
            InvoiceDataValue item;
            for (int i = 1; i <= 12; i++)
            {
                item = result.Where(x => x.Month == i.ToString()).SingleOrDefault();
                model.Add(new SeriesData<InvoiceDataValue>() 
                {
                    SeriesDisplayName  = DateTimeFormatInfo.CurrentInfo.GetMonthName(i)
                });
                
                if (item != null)
                {
                    model[i - 1].Items.Add(new InvoiceDataValue()
                    {
                        Month = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                        Number = item.Number
                    });
                }
                else
                {
                    model[i - 1].Items.Add(new InvoiceDataValue()
                    {
                        Month = DateTimeFormatInfo.CurrentInfo.GetMonthName(i),
                        Number = 0
                    });
                } 
            }
            this.Source = model;
            db.Close();
        }

    }
}
