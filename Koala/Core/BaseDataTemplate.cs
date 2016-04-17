using Krokot.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Koala.Core
{
    public abstract class BaseDataTemplate<T> : DataTemplateSelector
    {

        public ViewLocator Templates { get; set; }

        public BaseDataTemplate()
        {
            this.Templates = new ViewLocator();
        }

    }
}
