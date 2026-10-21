using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums;

public enum ScreenCastResult
{
    InternalError = -1,
    NoError = 0,
    InitFailed = 1,
    SourceError = 2,
    NotInitialized = 3
}
