// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     StringExt.cs
// Part of:  Tranquility
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-04-02 08:57
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;

namespace SharpTk
{
    public static class StringExt
    {
        /// <summary>
        ///     Returns the first (string) parameter which is not null and does not contain only whitespaces. If no matching
        ///     element is found, the function returns an empty string.
        /// </summary>
        /// <param name="items">One or more string parameters.</param>
        /// <returns>First non-null, non-whitespace element from.</returns>
        public static string FirstNonNull(params string[] items)
            => items.FirstOrDefault(predicate: i => !string.IsNullOrWhiteSpace(value: i)) ?? "";

        /// <summary>
        ///     Converts a given pascal-case string to a lowercase string with tokens separated by underscores.
        /// </summary>
        /// <param name="input">Pascal-case input string.</param>
        /// <returns>A lowercase, underscore separated string.</returns>
        public static string PascalToLowerUnderscore(string input)
            => string.Concat(
                    input.Select(
                        selector: (x, i) => i > 0 && char.IsUpper(c: x) ? "_" + x.ToString() : x.ToString()))
                .ToLower();
    }
}