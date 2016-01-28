using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core.Credential
{
    public interface IAuthorization
    {
        bool IsAuthorize(string key, string qualifier);
    }
}
