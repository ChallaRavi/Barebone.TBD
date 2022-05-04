using Barebone.TBD.Helpers;
using Barebone.TBD.Models;
using Contentful.Core;
using Contentful.Core.Images;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Contentful.Core.Search;
using Markdig;
using Markdig.Syntax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Barebone.TBD.Controllers
{
    public class HomeController : Controller
    {

        private ContentfulManagementClient _contentfulManagementClient;  
        public async Task<ActionResult> Index()
        {
            var httpClient = new HttpClient();
            //my personal space credentials
            var contentfulClient = new ContentfulClient(
                httpClient,

                String.Empty,
                String.Empty,
                String.Empty,
                false);


            var sandbox = new ContentfulClient(
                httpClient,

                String.Empty,
                String.Empty,
                String.Empty,
                false);


            //_contentfulManagementClient = new ContentfulManagementClient(httpClient,new Contentful.Core.Configuration.ContentfulOptions() 
            //{
            //    //DeliveryApiKey = "123",
            //    //ManagementApiKey = "564",
            //    //SpaceId = "666",
            //    //UsePreviewApi = false
            //});
            //_contentfulManagementClient = new ContentfulManagementClient(httpClient, System.Empty, System.Empty);
            //var res = await _contentfulManagementClient.GetEntriesForLocale<SomeData>(null, "en-US", System.Empty);

            var builder = new QueryBuilder<QAndA>().Include(2);
            var fields = sandbox.GetEntriesByType<QAndA>("qandA", builder).Result;


            //var locale = GetSupportedLocale();
            //var builder = QueryBuilder<T>.New.LocaleIs(locale.Code);

            var builder1 = new QueryBuilder<SupportPageContentfulModel>().Include(5).LocaleIs(locale:"en-US");
            var fields1 = contentfulClient.GetEntriesByType<SupportPageContentfulModel>("SupportPage", builder1).Result;

            var abc = await contentfulClient.GetEntriesByType<SupportPageContentfulModel>("SupportPage");

            // IEnumerable<SomeData> contentfulContent = null;


            var contentfulContent = await contentfulClient.GetEntriesByType<SomeData>("homePage");

            

            var htmlRenderer = new HtmlRenderer();
           // var builder100 = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
          //  MarkdownDocument markdownDocument = Markdown.Parse("", new MarkdownPipelineBuilder().UseAdvancedExtensions().Build());
            
            
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            contentfulContent.FirstOrDefault().LongTextContainer = Markdown.ToHtml(contentfulContent.FirstOrDefault().LongField, pipeline);

            //var richContent = htmlRenderer.ToHtml(fields1.FirstOrDefault().FAQQuestion1);
            //string richText = IterateAndReplaceLinkPlaceHolders(richContent.Result, fields1.FirstOrDefault().SupportLinks.ToString());
            //fields1.FirstOrDefault().RichTextContainer = new HtmlString(richText);


            //var lb = htmlRenderer.ToHtml(contentfulContent.FirstOrDefault().LC);

            //contentfulContent.FirstOrDefault().LongField1 = contentfulContent.FirstOrDefault().LongField;


            return View(contentfulContent);
        }

        private string ReplaceLinkPlaceholder(string richContentToHtml, KeyValuePair<string, string> link)
        {
            richContentToHtml = richContentToHtml
                .Replace(
                    "{{" + link.Key + "}}",
                    $"<a href='{link.Value}' target='_blank'>{link.Key}</a>"
                    );
            return richContentToHtml;
        }

        /// <summary>
        /// Deserialize the JSON Feild into Dictionary 
        /// </summary>
        /// <param name="supportLinks"></param>
        /// <returns></returns>
        private Dictionary<string, string> DeserializeTheLinks(string supportLinks)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(supportLinks);
        }

        /// <summary>
        /// Iterating the JSON Feild(SupportLinks) and process to replace {{link}} placeholders
        /// </summary>
        /// <param name="richContentToHtml"></param>
        /// <param name="supportLinks"></param>
        /// <returns></returns>
        private string IterateAndReplaceLinkPlaceHolders(string richContentToHtml, string supportLinks)
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


    }
}