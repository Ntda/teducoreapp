using System;
using System.Collections.Generic;
using System.Text;
using TeduNetcore.Data.Enums;

namespace TeduNetcore.Data.Interfaces
{
    public interface ISwitchAble
    {
        Status Status { get; set; }
    }
}
