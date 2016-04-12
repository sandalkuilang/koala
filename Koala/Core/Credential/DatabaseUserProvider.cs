using Koala.ViewModels.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Texaco.Database;

namespace Koala.Core.Credential
{
    public class DatabaseUserProvider : IUserProvider
    {
        private IDbManager dbManager;

        public DatabaseUserProvider()
        {
            this.dbManager = ObjectPool.Instance.Resolve<IDbManager>();
        }

        public DatabaseUserProvider(IDbManager dbManager)
        {
            this.dbManager = dbManager; 
        }

        public User GetUser(string username)
        {
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            //User user = db.Query<User>("GetUserByName", new 
            //{
            //    Username = username
            //});

            return null;
        }

        public User GetUser(string username, string password)
        {
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
            //User user = db.Query<User>("GetUserByName", new 
            //{
            //    Username = username
            //});
        }

        public List<AuthorizationFeatureQualifier> GetAuthorization(User user)
        {
            throw new NotImplementedException();
        }
    }
}
