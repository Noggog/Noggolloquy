﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loqui.Generation
{
    public static class Utility
    {
        public static string MemberNameSafety(string str)
        {
            var ret = str.Replace(".", string.Empty);
            ret = ret.Replace("[]", "Arr");
            return ret;
        }
    }
}
