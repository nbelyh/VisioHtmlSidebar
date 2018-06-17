using System.Linq;
using System.Net;
using AngleSharp.Extensions;
using AngleSharp.Parser.Html;

namespace HtmlFormData
{
    public static class PostFormatHtmlReport
    {
        public static string PostProcess(string html)
        {
            var parser = new HtmlParser();
            var doc = parser.Parse(html);

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