using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Barebone.TBD.ContentfulHelpers;
using Contentful.Core.Models;

namespace Barebone.TBD.Models
{
    public class SomeData
    {
        public string Banner { get; set; }

        public Document RichContent { get; set; }
        public object SupportingLinks { get; set; }

        public HtmlString RichTextContainer { get; set; }

        public string LongField { get; set; }

        public string LongField1 { get; set; }

        public Document LC { get; set; }

        public string lstring { get; set; }

        public string ChallaTest { get; set; }

        public Asset Image { get; set; }
        public Asset SvgName { get; set; }

        public string LongTextContainer { get; set; }

        public string GetText => RichTextProcessor.GetMarkDownText(LongField);


    }
}