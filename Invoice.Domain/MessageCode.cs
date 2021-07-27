using System;
using System.Collections.Generic;
using System.Text;

namespace Invoice.Domain
{
    public enum MessageCode
    {
        AppException = 1008,
        Timeout = 1000,
        GeneralException = 1001,
        RequieredField = 1002,
        InvalidLength = 1003,
        InvalidValue = 1004,
        ResourceNotFound = 404
    }
}
