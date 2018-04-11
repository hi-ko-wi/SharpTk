// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     StringExt.cs
// Part of:  SharpTk
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-10-15 // 08:51
// --------------------------------------------------------------------------------------------------------------------

namespace SharpTk
{
    #region

    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    #endregion

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
            => string
                .Concat(
                    input.Select(
                        selector: (x, i) => i > 0 && char.IsUpper(c: x) ? "_" + x.ToString() : x.ToString()))
                .ToLower();

        /// <summary>
        ///     Securely hashes the given input string using HMAC
        /// </summary>
        /// <param name="input">Input string to compute a hash for.</param>
        /// <returns>The computed hash as a hex string.</returns>
        public static (string salt, string hash) SecureHash(this string input)
        {
            byte[] salt;
            using (var rng = new RNGCryptoServiceProvider()) {
                var rawSalt = new byte[128 / 8];
                rng.GetBytes(data: rawSalt);
                salt = rawSalt;
            }

            var hmac = new HMACSHA256(key: salt);
            var hash = hmac.ComputeHash(Encoding.Default.GetBytes(s: input));

            var saltString = Convert.ToBase64String(inArray: salt);
            var hashString = Convert.ToBase64String(inArray: hash);

            return (saltString, hashString);
        }

        /// <summary>
        ///     Compares an input string with an expected hashed secret.
        /// </summary>
        /// <param name="input">The input string to check.</param>
        /// <param name="salt">The salt used to create the hash previously.</param>
        /// <param name="expectedHash">The known hashed value to check the input with.</param>
        /// <returns></returns>
        public static bool SecureHashValidate(this string input, string salt, string expectedHash)
        {
            var saltBytes = Convert.FromBase64String(s: salt);

            var hmac = new HMACSHA256(key: saltBytes);
            var hash = hmac.ComputeHash(Encoding.Default.GetBytes(s: input));
            var secretBytes = Convert.FromBase64String(s: expectedHash);

            return secretBytes.SequenceEqual(second: hash);
        }
    }
}