using LanguageExt.ClassInstances.Const;
using LanguageExt.TypeClasses;
using System.Diagnostics.Contracts;

namespace LanguageExt.ClassInstances.Pred
{
    public struct CharSatisfy<MIN, MAX> : Pred<char>
        where MIN : struct, Const<char>
        where MAX : struct, Const<char>
    {
        public static readonly CharSatisfy<MIN, MAX> Is = default;

        [Pure]
        public bool True(char value) =>
            Range<TChar, char, MIN, MAX>.Is.True(value);
    }

    public struct Char<CH> : Pred<char>
        where CH : struct, Const<char>
    {
        public static readonly Char<CH> Is = default;

        [Pure]
        public bool True(char value) =>
            default(CH).Value == value;
    }

    public struct Letter : Pred<char>
    {
        public static readonly Letter Is = default;

        [Pure]
        public bool True(char value) =>
            Exists<char,
                CharSatisfy<ChA, ChZ>,
                CharSatisfy<Cha, Chz>>.Is.True(value);
    }

    public struct Digit : Pred<char>
    {
        public static readonly Digit Is = default;

        [Pure]
        public bool True(char value) =>
            CharSatisfy<Ch0, Ch9>.Is.True(value);
    }

    public struct Space : Pred<char>
    {
        public static readonly Space Is = default;

        [Pure]
        public bool True(char value) =>
            Exists<char, Char<ChSpace>, Char<ChTab>, Char<ChCR>, Char<ChLF>>.Is.True(value);
    }

    public struct AlphaNum : Pred<char>
    {
        public static readonly AlphaNum Is = default;

        [Pure]
        public bool True(char value) =>
            Exists<char, Letter, Digit>.Is.True(value);
    }
}
