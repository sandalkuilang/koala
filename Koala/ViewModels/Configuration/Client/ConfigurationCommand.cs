﻿using Koala.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Koala.ViewModels.Configuration.Client
{
    public class ConfigurationCommand : BaseBindableModel, INotifyPropertyChanged
    { 
        public ICommand SaveCommand { get; set; }
        public ICommand RouteConnectionCommand { get; set; }

        private string buttonContent;
        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            {
                NotifyIfChanged(ref this.buttonContent, value);
            }
        }
    }
}
