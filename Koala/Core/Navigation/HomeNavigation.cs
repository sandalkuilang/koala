using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Koala.Core;

namespace Koala.Navigation
{
    public class HomeNavigation : BaseDataTemplate<ItemCollection>
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return (DataTemplate)Templates["Home"];
            }
            else
            {
                return (DataTemplate)item;
            }
        }
    }
}
