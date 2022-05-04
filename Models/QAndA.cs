using System.Web;
using Contentful.Core.Models;

namespace Barebone.TBD.Models
{
    public class QAndA
    {
        public string Question { get; set; }

        public Document Answer { get; set; }

        public object Links { get; set; }

        public string GetQuestion()
        {
            return Question;
        }

        public HtmlString GetAnswers()
        {
            //return RichTextProcessor.GetRichTextHavingLinks(Answer, Links);
            return new HtmlString("Hello");
        }
    }
}