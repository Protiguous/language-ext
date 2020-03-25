using LanguageExt.TypeClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageExt.ClassInstances.Pred
{
    public struct GreaterThan<ORD, A, CONST> : Pred<A>
        where ORD : struct, Ord<A>
        where CONST : struct, Const<A>
    {
        public static readonly GreaterThan<ORD, A, CONST> Is = default;

        [Pure]
        public bool True(A value) =>
            default(ORD).Compare(value, default(CONST).Value) > 0;
    }

    public struct LessThan<ORD, A, CONST> : Pred<A>
        where ORD : struct, Ord<A>
        where CONST : struct, Const<A>
    {
        public static readonly LessThan<ORD, A, CONST> Is = default;

        [Pure]
        public bool True(A value) =>
            default(ORD).Compare(value, default(CONST).Value) < 0;
    }

    public struct GreaterOrEq<ORD, A, CONST> : Pred<A>
        where ORD : struct, Ord<A>
        where CONST : struct, Const<A>
    {
        public static readonly GreaterOrEq<ORD, A, CONST> Is = default;

        [Pure]
        public bool True(A value) =>
            default(ORD).Compare(value, default(CONST).Value) >= 0;
    }

    public struct LessOrEq<ORD, A, CONST> : Pred<A>
        where ORD   : struct, Ord<A>
        where CONST : struct, Const<A>
    {
        public static readonly LessOrEq<ORD, A, CONST> Is = default;

        [Pure]
        public bool True(A value) =>
            default(ORD).Compare(value, default(CONST).Value) <= 0;
    }
}
