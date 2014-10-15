﻿using System.Text.RegularExpressions;

namespace Comb
{
    public static class Utilities
    {
        public static string EncodeValue(string value)
        {
            /*
             * Both JSON and XML batches can only contain UTF-8 characters that are valid in XML.
             * Valid characters are the control characters tab (0009), carriage return (000D),
             * and line feed (000A), and the legal characters of Unicode and ISO/IEC 10646. FFFE,
             * FFFF, and the surrogate blocks D800–DBFF and DC00–DFFF are invalid and will cause
             * errors. (For more information, see Extensible Markup Language (XML) 1.0 (Fifth
             * Edition).) You can use the following regular expression to match invalid characters
             * so you can remove them: /[^\u0009\u000a\u000d\u0020-\uD7FF\uE000-\uFFFD]/ .
             * */
            value = Regex.Replace(value, "[^\u0009\u000a\u000d\u0020-\uD7FF\uE000-\uFFFD]", "");
            value = value.Replace(@"\", @"\\");
            value = value.Replace("'", @"\'");

            return value;
        }
    }
}