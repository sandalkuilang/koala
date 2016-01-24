﻿using Koala.Core;
using Koala.ViewModels.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Koala.Views
{
    /// <summary>
    /// Interaction logic for Master.xaml
    /// </summary>
    public partial class MasterLayout : UserControl
    {
        private MasterCollaborator model;
        public MasterLayout()
        {
            InitializeComponent();
            model = ObjectPool.Instance.Resolve<MasterCollaborator>();
            if (this.DataContext == null && model != null)
            {
                this.DataContext = model;
            } 
        }
    }
}
