using Koala.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.ValidationRules
{
    public class AuthorizationDelegateCommand : DelegateCommandBase
    {
        public AuthorizationDelegateCommand(Action executeMethod)
            : base((op) => executeMethod(), (op) => AuthorizationProvider.Instance.CheckAccess(op))
        {
            if (executeMethod == null)
                throw new ArgumentNullException("executeMethod");
        }

        public void Execute()
        {
            base.Execute(null);
        }
    }


}
