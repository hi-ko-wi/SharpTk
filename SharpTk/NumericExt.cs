// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     NumericExt.cs
// Part of:  Tranquility
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-04-02 08:53
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace SharpTk
{
    public static class NumericTk
    {
        public static bool TkIsOne(this decimal? input, bool defaultValue = false)
            => !input.HasValue ? defaultValue : Math.Abs(value: input.Value) - 1 == 0;
    }
}