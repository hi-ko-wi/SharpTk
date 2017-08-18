// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     ControllerExt.cs
// Part of:  Tranquility
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-08-17 22:11
// --------------------------------------------------------------------------------------------------------------------

namespace SharpTkAsp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;

    public static class ControllerExt
    {
        public static IActionResult OkOrNotFound<TIn>(this Controller ctl, TIn input)
            where TIn : class
            => input == null ? (IActionResult) ctl.NotFound() : ctl.Ok(value: input);

        public static IActionResult OkOrNotFound<TIn>(this Controller ctl, TIn input, Func<TIn, object> rspFac)
            where TIn : class
            => input == null ? (IActionResult) ctl.NotFound() : ctl.Ok(rspFac(arg: input));

        public static IActionResult OkOrNotFoundOrEmpty<TIn>(this Controller ctl, IEnumerable<TIn> input)
            => input == null ? ctl.NotFound() : (!input.Any() ? (IActionResult) ctl.NoContent() : ctl.Ok(value: input));
    }
}