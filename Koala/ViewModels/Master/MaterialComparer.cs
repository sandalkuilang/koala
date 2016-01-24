using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Master
{
    public class MaterialComparer : IEqualityComparer<MaterialType>
    {
        public bool Equals(MaterialType x, MaterialType y)
        { 
            if (Object.ReferenceEquals(x, y)) return true;
             
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            return x.Id == y.Id && x.Description == y.Description;
        }

        public int GetHashCode(MaterialType obj)
        {
            if (Object.ReferenceEquals(obj, null)) return 0;
            int hashDescription = obj.Description == null ? 0 : obj.Description.GetHashCode();
            int hashId = obj.Id.GetHashCode();
            return hashDescription ^ hashId;
        }
    }
}
