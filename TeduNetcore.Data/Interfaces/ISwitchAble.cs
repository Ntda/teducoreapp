using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Data.Enums;

namespace TeduNetcore.Data.Interfaces
{
    interface ISwitchAble
    {
        Status Status { get; set; }
    }
}
