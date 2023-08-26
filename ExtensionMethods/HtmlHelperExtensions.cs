using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.ExtensionMethods
{
    public static class HtmlHelperExtensions
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string route)
        {
            var routeData = htmlHelper.ViewContext.RouteData;

            var pageRoute = routeData.Values["page"]!.ToString() ?? "";

            var index = pageRoute.IndexOf("/", 1);
            if (index == -1)
            {
                index = pageRoute.Length;
            }

            return route.ToLower() == pageRoute.Substring(0, index).ToLower() ? "active" : "";
        }
    }
}
