using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core.Credential
{
    public interface IEncryptionAgent
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
