using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Enums
{
    public enum CustomerStatus
    {
        Prospective = 1,
        Current,
        [Description("Non-Active")]
        NonActive,
    }
}
