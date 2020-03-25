using LanguageExt.ClassInstances;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.Serialization;
using static LanguageExt.Prelude;

namespace LanguageExt
{
    namespace SomeHelp
    {
        /// <summary>
        /// Extension method for Some T to help with the lack of covariance of generic
        /// parameters in structs (and therefore Some T)
        /// </summary>
        public static class SomeExt
        {
            /// <summary>
            /// Convert value to Some T.  Helps with the lack of covariance of generic
            /// parameters in structs (and therefore Some T)
            /// </summary>
            /// <typeparam name="T">Value type</typeparam>
            /// <param name="value">Value</param>
            /// <returns>Value wrapped in a Some T</returns>
            public static Some<T> ToSome<T>(this T value)
            {
                return new Some<T>(value);
            }
        }
    }

    [Serializable]
    public struct Some<A> :
        IEnumerable<A>,
        IOptional
    {
        private readonly bool initialised;
        private readonly A value;

        [Pure]
        public bool IsNone =>
            !initialised;

        [Pure]
        public bool IsSome =>
            initialised;

        [Pure]
        public A Value =>
            CheckInitialised(value);

        public Some(A value)
        {
            if (isnull(value))
            {
                throw new ValueIsNullException("Value is null when expecting Some(x)");
            }
            this.value = value;
            initialised = true;
        }

        /// <summary>
        /// Used to facilitate serialisation
        /// </summary>
        public Some(IEnumerable<A> someValue)
        {
            var first = someValue.Take(1).ToArray();
            initialised = first.Length == 1;
            this.value = initialised
                ? first[0]
                : default;
        }

        [Pure]
        private U CheckInitialised<U>(U value) =>
            initialised
                ? value
                : raise<U>(new SomeNotInitialisedException(typeof(A)));

        [Pure]
        public static implicit operator A(Some<A> value) =>
            value.Value;

        [Pure]
        public static implicit operator Option<A>(Some<A> value) =>
            default(MOption<A>).Return(value.Value);

        [Pure]
        public static implicit operator Some<A>(A value) =>
            new Some<A>(value);

        [Pure]
        public IEnumerable<A> AsEnumerable() =>
            initialised
                ? new[] { value }
                : new A[0];

        [Pure]
        public override bool Equals(object obj) =>
            Value.Equals(obj);

        [Pure]
        public IEnumerator<A> GetEnumerator() =>
            AsEnumerable().GetEnumerator();

        [Pure]
        public override int GetHashCode() =>
            Value.GetHashCode();

        [Pure]
        public Type GetUnderlyingType() =>
            typeof(A);

        [Pure]
        public R MatchUntyped<R>(Func<object, R> Some, Func<R> None) =>
            IsSome
                ? Check.NullReturn(Some(value))
                : Check.NullReturn(None());

        [Pure]
        public R MatchUntypedUnsafe<R>(Func<object, R> Some, Func<R> None) =>
            IsSome
                ? Some(value)
                : None();

        [Pure]
        public Seq<A> ToSeq() =>
            initialised
                ? Seq1(value)
                : Empty;

        [Pure]
        public override string ToString() =>
            Value.ToString();

        [Pure]
        IEnumerator IEnumerable.GetEnumerator() =>
            AsEnumerable().GetEnumerator();
    }

    public static class Some
    {
        [Pure]
        public static SomeCase<T> Create<T>(T x) =>
            new SomeCase<T>(x);
    }
}
