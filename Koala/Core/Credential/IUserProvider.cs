using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.Core.Credential
{
    public interface IUserProvider
    {
        User GetUser(string username);
        User GetUser(string username, string password);
        List<AuthorizationFeatureQualifier> GetAuthorization(User user);
    }
}
