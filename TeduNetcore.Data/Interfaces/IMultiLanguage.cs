using System;
using System.Collections.Generic;
using System.Text;

namespace TeduNetcore.Data.Interfaces
{
    interface IMultiLanguage<T>
    {
        T LanguageId { get; set; }
    }
}
