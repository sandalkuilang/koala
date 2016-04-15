using Koala.Core;
using System;

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
