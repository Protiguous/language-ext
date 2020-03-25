using LanguageExt.TypeClasses;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances.Pred
{
    public struct MaxCount<MAX> : Pred<ListInfo>
        where MAX : struct, Const<int>
    {
        public static readonly MaxCount<MAX> Is = default;

        [Pure]
        public bool True(ListInfo value) =>
            value.Count <= default(MAX).Value;
    }
}
