﻿using System;
using LanguageExt.TypeClasses;

namespace LanguageExt
{
    public interface MonadTrans<OuterMonad, OuterType, InnerMonad, InnerType, A>
        where OuterMonad : struct, Monad<OuterType, InnerType>
        where InnerMonad : struct, Monad<InnerType, A>
    {
        NewOuterType Bind<NewOuterMonad, NewOuterType, NewInnerMonad, NewInnerType, B>(OuterType ma, Func<A, NewInnerType> f)
            where NewOuterMonad : struct, Monad<NewOuterType, NewInnerType>
            where NewInnerMonad : struct, Monad<NewInnerType, B>;

        NewOuterType Map<NewOuterMonad, NewOuterType, NewInnerMonad, NewInnerType, B>(OuterType ma, Func<A, B> f)
            where NewOuterMonad : struct, Monad<NewOuterType, NewInnerType>
            where NewInnerMonad : struct, Monad<NewInnerType, B>;

        S Fold<S>(OuterType ma, S state, Func<S, A, S> f);

        S FoldBack<S>(OuterType ma, S state, Func<S, A, S> f);
    }
}