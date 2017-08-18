// --------------------------------------------------------------------------------------------------------------------
// Copyright (c) 2017-2017
// for information on the creator and copyright owner, please see the author list bellow and the assembly 
// information file. 
// -
// All rights are reserved. Reproduction or transmission in whole or in part, any form or by any means, electronic, 
// mechanical or otherwise, is prohibited without the prior written consent of the copyright owner.
// -
// File:     StrSplitter.cs
// Part of:  Tranquility
// -
// Author:   Haiko Wick (Haiko Wick)
// Modified: 2017-08-17 20:59
// --------------------------------------------------------------------------------------------------------------------

namespace SharpTk
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class StrSplitter
    {
        public StrSplitter(string text, Func<SplitFormatItem, string> itemFormatter)
        {
            var offset = 0;
            var newString = new StringBuilder();

            do {
                var start = text.IndexOf(value: '{', startIndex: offset);
                if (start < 0) {
                    newString.Append(text.Substring(startIndex: offset, length: text.Length - offset));
                    break;
                }

                if (start > 1 && text[start - 1] == '\\') {
                    newString.Append(text.Substring(startIndex: offset, length: text.Length - offset));
                    offset = start + 1;
                    continue;
                }

                newString.Append(text.Substring(startIndex: offset, length: start - offset));

                var stop = text.IndexOf(value: '}', startIndex: start);
                if (stop < 0) {
                    Result = string.Empty;
                    break;
                }

                var format = text.Substring(start + 1, stop - start - 1);
                if (!string.IsNullOrEmpty(value: format)) {
                    var result = ProcessFormat(format: format, itemFormatter: itemFormatter);
                    newString.Append(value: result);
                }

                offset = stop + 1;
            } while (offset < text.Length);

            Result = newString.ToString();
        }

        public string Result { get; set; }

        public string ProcessFormat(string format, Func<SplitFormatItem, string> itemFormatter)
        {
            var split = format.Split(',');

            int key;
            var hasIntKey = int.TryParse(split[0], result: out key);

            var customKey = hasIntKey ? null : !string.IsNullOrWhiteSpace(split[0]) ? split[0] : null;

            if (!hasIntKey && string.IsNullOrEmpty(value: customKey))
                return string.Empty;

            var item = new SplitFormatItem {Key = hasIntKey ? key : -1, CustomKey = customKey};

            for (var i = 1; i < split.Length - 1; i++) {
                var expl = split[i].Split('=');
                if (expl.Length == 2)
                    item.Params.Add(new KeyValuePair<string, string>(expl[0], expl[1]));
            }

            return itemFormatter(arg: item);
        }

        public class SplitFormatItem
        {
            public string CustomKey { get; set; }

            public int Key { get; set; }

            public List<KeyValuePair<string, string>> Params { get; set; }
        }
    }
}