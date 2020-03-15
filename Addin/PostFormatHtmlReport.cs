using System;
using System.Linq;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace VisioHtmlSidebar
{
    public static class PostFormatHtmlReport
    {
        private static void FixStyle(StyleParser parser, string selector, string name, string oldVal, string newVal)
        {
            var rule = parser
                .Stylesheet
                .FirstOrDefault(sel => sel.Name == selector);

            if (rule != null)
            {
                var val = rule.Properties
                    .FirstOrDefault(x => x.Name == name && x.Value == oldVal);

                if (val != null)
                    val.Value = newVal;
            }
        }

        private static void ProcessDocStyles(IHtmlDocument doc)
        {
            var parser = new StyleParser();

            var style = doc.QuerySelector("STYLE");
            
            parser.Parse(style.TextContent);
            
            FixStyle(parser, "TD", "text-align", "right", "left");
            FixStyle(parser, "TH", "text-align", "right", "left");

            style.TextContent = parser.ToString();
        }

        public static string PostProcess(string html)
        {
            var parser = new HtmlParser();
            var doc = parser.Parse(html);

            ProcessDocStyles(doc);

            var rows = doc.QuerySelectorAll("TABLE TR").ToList();

            var ths = rows.First().QuerySelectorAll("TH").Select(th => th.Text()).ToList();

            foreach (var row in rows.Skip(1))
            {
                var tds = row.QuerySelectorAll("TD").ToList();
                for (var i = 0; i < ths.Count; ++i)
                {
                    var td = tds[i];
                    var th = ths[i];

                    if (th == "Description")
                    {
                        td.InnerHtml = WebUtility.HtmlDecode(td.InnerHtml);
                    }
                }
            }


            return doc.ToHtml();
        }
    }
}