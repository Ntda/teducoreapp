using System;
using System.Collections.Generic;
using System.Text;

namespace TeduNetcore.Data.Interfaces
{
    public interface IHasOwner<T> where T:class
    {
        T OwnerId { get; set; }
    }
}
