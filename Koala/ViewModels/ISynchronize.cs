using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels
{
    public interface ISynchronize
    {
        void Pull();
        void Push();
        void Start();
        void Stop();
    }
}
