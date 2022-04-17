﻿using System.ComponentModel;

namespace Discount.API.Enum
{
    public enum Status
    {
        [Description("Active")]
        Active = 1,
        [Description("Passive")]
        Passive = 2,
    }
}
