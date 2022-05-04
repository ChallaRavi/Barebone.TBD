using Contentful.Core.Models;
using Markdig;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace Barebone.TBD.ContentfulHelpers
{
    public class RichTextProcessor
    {
        private static readonly MarkdownPipeline _markdownPipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        /// <summary>
        /// Gets The Rich Text Content defined in Contentful
        /// </summary>
        /// <param name="document">The Rich Text Format</param>
        /// <param name="links">Json Object</param>
        /// <returns>HTML-encoded string</returns>
        public static HtmlString GetRichText(Document document , object links = null)
        {
            if (document is null)
            {
                throw new ArgumentNullException(nameof(document));
            }

            var htmlRender = new HtmlRenderer();
            var richContent = htmlRender.ToHtml(document).Result;

            if (!(links is null))
            {
                return new HtmlString(IterateAndReplaceLinkPlaceHolders(richContent, links.ToString()));
            }
            return new HtmlString(richContent);
        }

        public static string GetMarkDownText(string markdownText) 
        {
            return MarkDownToHtml(markdownText);
        }

        /// <summary>
        /// </summary>
        /// <param name="richContentToHtml"></param>
        /// <param name="link"></param>
        /// <returns></returns>
        private static string ReplaceLinkPlaceholder(string richContentToHtml, KeyValuePair<string, string> link)
        {
            richContentToHtml = richContentToHtml
                .Replace(
                    "{{" + link.Key + "}}",
                    $"<a href='{link.Value}' target='_blank'>{link.Key}</a>");
            return richContentToHtml;
        }

        /// <summary>
        /// Deserialize the JSON Feild into Dictionary
        /// </summary>
        /// <param name="supportLinks"></param>
        /// <returns></returns>
        private static Dictionary<string, string> DeserializeTheLinks(string supportLinks)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(supportLinks);
        }

        /// <summary>
        /// Iterating the JSON Feild(SupportLinks) and process to replace {{link}} placeholders
        /// </summary>
        /// <param name="richContentToHtml"></param>
        /// <param name="supportLinks"></param>
        /// <returns></returns>
        private static string IterateAndReplaceLinkPlaceHolders(string richContentToHtml, string supportLinks)
        {
            Dictionary<string, string> links = DeserializeTheLinks(supportLinks);
            if (links.Count > 0)
            {
                foreach (KeyValuePair<string, string> link in links)
                {
                    richContentToHtml = ReplaceLinkPlaceholder(richContentToHtml, link);
                }
            }

            return richContentToHtml;
        }

        private static string MarkDownToHtml(string str) 
        {
            return Markdown.ToHtml(str, _markdownPipeline);
        }

    }
}