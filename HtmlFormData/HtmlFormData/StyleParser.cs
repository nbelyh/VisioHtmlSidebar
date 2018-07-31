
//-----------------------------------------------------------------------
// Copyright (c) 2017-2018 Nikolay Belykh unmanagedvisio.com All rights reserved.
// Nikolay Belykh, nbelyh@gmail.com
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HtmlFormData
{
    class StyleParser
    {
        private const String CSSGroups = @"(?<selector>(?:(?:[^,{]+),?)*?)\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\}";
        private const String CSSComments = @"(?<!"")\/\*.+?\*\/(?!"")";

        private const String SelectorKey = "selector";
        private const String NameKey = "name";
        private const String ValueKey = "value";

        private readonly Regex _rStyles = new Regex(CSSGroups, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public class Property
        {
            public string Name;
            public string Value;
        }

        public class Selector
        {
            public string Name;
            public List<Property> Properties;
        }

        public List<Selector> Stylesheet { get; private set; }

         /// <summary>
        /// Reads the specified cascading style sheet.
        /// </summary>
        /// <param name="text">The cascading style sheet.</param>
         public void Parse(String text)
         {
             Stylesheet = new List<Selector>();

            Char[] whiteSpace = { '\r', '\n', '\f', '\t', '\v', ' ' };
            //Remove comments before parsing the CSS. Don't want any comments in the collection. Don't know how iTextSharp would react to CSS Comments
            var matchList = _rStyles.Matches(Regex.Replace(text, CSSComments, String.Empty));
            foreach (Match item in matchList)
            {
                //Check for nulls
                if (item == null || item.Groups[SelectorKey] == null || String.IsNullOrEmpty(item.Groups[SelectorKey].Value)) 
                    continue;

                var strSelector = item.Groups[SelectorKey].Captures[0].Value.Trim(whiteSpace);

                var selector = new Selector { Name = strSelector, Properties = new List<Property>() };

                for (int i = 0; i < item.Groups[NameKey].Captures.Count; i++)
                {
                    var style = item.Groups[NameKey].Captures[i].Value.Trim(whiteSpace);
                    var value = item.Groups[ValueKey].Captures[i].Value.Trim(whiteSpace);

                    if (!String.IsNullOrEmpty(style) && !String.IsNullOrEmpty(value))
                        selector.Properties.Add(new Property {Name = style, Value = value});
                }

                Stylesheet.Add(selector);
            }
        }

       
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var item in Stylesheet)
            {
                sb.Append("\n\t");
                sb.Append(item.Name);
                sb.Append(" {");
                foreach (var property in item.Properties)
                {
                    sb.Append(property.Name).Append(":").Append(property.Value).Append(";");
                }
                sb.Append("}");
            }
            sb.Append("\n");

            return sb.ToString();
        }
    }
}
