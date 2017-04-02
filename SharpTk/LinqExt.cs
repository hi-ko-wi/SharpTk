// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     LinqExt.cs
// Part of:  Tranquility
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-04-02 09:53
// --------------------------------------------------------------------------------------------------------------------

namespace SharpTk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using JetBrains.Annotations;

    public static class LinqExt
    {
        public static string PropertyNameFromMemberExpression<TClass, TRight>(
            [NotNull] Expression<Func<TClass, TRight>> selector,
            [CanBeNull] string propertyName = null) where TClass : class
        {
            if (propertyName == null)
                propertyName = ((MemberExpression) (selector.Body as UnaryExpression)?.Operand)?.Member.Name;
            return propertyName ?? ((MemberExpression) selector.Body)?.Member.Name;
        }

        public static List<TOut> SelectList<TIn, TOut>(this IEnumerable<TIn> input, Func<TIn, TOut> selector)
            where TIn : class where TOut : class => input.Select(selector: selector).ToList();
    }
}