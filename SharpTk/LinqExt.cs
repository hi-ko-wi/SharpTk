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
// Modified: 2017-04-02 09:01
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace SharpTk
{
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
    }
}