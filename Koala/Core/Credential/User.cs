using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core.Credential
{
    public class User
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string IsActive { get; set; } 
        public List<AuthorizationFeatureQualifier> Role { get; set; }

        public User()
        {
            Role = new List<AuthorizationFeatureQualifier>();
        }
    }
}
