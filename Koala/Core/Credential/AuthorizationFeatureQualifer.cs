using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core.Credential
{
    public class AuthorizationFeatureQualifier
    {
        public string Id { get; set; }
        public string Key { get; set; }
        public string Qualifier { get; set; }
        public AuthorizationUIPolicy AuthorizationUIPolicy { get; set; }
    }
}
