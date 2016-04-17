using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using Krokot.Database;

namespace Koala.ViewModels.Report
{
    public abstract class BaseDashboardViewSource : BaseBindableModel
    {
        private int month;
        public int Month
        {
            get
            {
                return month;
            }
            set
            {
                NotifyIfChanged(ref month, value);
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

        private double fontSize = 14.0;
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

        private List<int> months;
        public List<int> Months
        {
            get
            {
                return months;
            }
            set
            {
                NotifyIfChanged(ref months, value);
            }
        }

        public BaseDashboardViewSource()
        {
            this.Year = DateTime.Now.Year;
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            Years = db.Query<int>("GetDistinctYear");
            db.Close();

            months = new List<int>();
            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            Month = DateTime.Now.Month; 
        }

        public abstract void Load();
    }
}
