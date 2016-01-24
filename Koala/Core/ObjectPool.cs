using Texaco.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core
{
    internal class ObjectPool
    {
        private static IContainer instance;
        public static IContainer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Container("Koala-container");
                }
                return instance;
            }
        }
    }
}
