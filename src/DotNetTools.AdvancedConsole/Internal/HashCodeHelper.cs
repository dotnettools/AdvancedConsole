using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetTools.AdvancedConsole.Internal
{
    internal static class HashCodeHelper
    {
        public static int GetHashCode(params object[] values)
        {
            unchecked
            {
                var hash = 17;
                foreach (var val in values)
                    hash = hash * 23 + val?.GetHashCode() ?? 0;
                return hash;
            }
        }
    }
}
