using System.Collections.Generic;
using System.Web;
using Barebone.TBD.ContentfulHelpers;
using Contentful.Core.Models;

namespace Barebone.TBD.Models
{
    public class SupportPageContentfulModel
    {
        public string SupportPageTitle { get; set; }

        public string SupportPageTitleParagraph { get; set; }

        public string ServiceHealthHeadingPerRegion { get; set; }

        public string ServiceHealthOutageHeading { get; set; }

        public string ServiceHealthOutageDefaultPara { get; set; }

        public string ServiceHealthAwarenessHeading { get; set; }

        public string ServiceHealthAwarenessDefaultPara { get; set; }

        public Document FAQQuestion1 { get; set; }

        public object SupportLinks { get; set; }

        public HtmlString RichTextContainer { get; set; }

        public string FAQQuestionTitle { get; set; }

        public string LongFeild { get; set; }

        public List<QAndA> Questionnaire { get; set; }

        public List<SupportPageAdditionalResourcesCard> SupportPageAdditionalResources { get; set; }

        public  Document RichContent { get; set; }

        public HtmlString GetText => RichTextProcessor.GetRichText(RichContent);
        public HtmlString GetSimpleRichTextContent() 
        {
            return RichTextProcessor.GetRichText(RichContent);
        }
        public HtmlString TestContent()
        {
             return RichTextProcessor.GetRichText(FAQQuestion1, SupportLinks);
        }

        public string LongTextContainer { get; set; }


    }
}