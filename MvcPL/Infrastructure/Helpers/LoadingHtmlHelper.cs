using System.Web.Mvc;

namespace MvcPL.Infrastructure.Helpers
{
    public static class LoadingHtmlHelper
    {
        public static MvcHtmlString LoadingElement(this HtmlHelper helper, string id, string value, string styleClass = "")
        {
            var tag = new TagBuilder("div");
            tag.MergeAttribute("id", id, true);
            tag.MergeAttribute("style", "display:none", true);
            var pTag = new TagBuilder("p");
            pTag.SetInnerText(value);
            tag.InnerHtml = pTag.ToString();
            
            if (!string.IsNullOrEmpty(styleClass)) tag.AddCssClass(styleClass);
            return new MvcHtmlString(tag.ToString());
        }
    }
}