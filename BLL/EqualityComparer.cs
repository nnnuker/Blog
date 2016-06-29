using System.Collections.Generic;
using BLL.Entities;

namespace BLL
{
    public class EqualityComparer:IEqualityComparer<BllPost>
    {
        public bool Equals(BllPost x, BllPost y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(BllPost obj)
        {
            return obj.Content.GetHashCode() + obj.Title.GetHashCode() 
                + obj.Id.GetHashCode() + obj.BlogId.GetHashCode();
        }
    }
}