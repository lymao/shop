using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Các trang tĩnh trước
            routes.MapRoute(
                     name: "Login",
                     url: "dang-nhap.html",
                     defaults: new { controller = "Account", action = "Index", id = UrlParameter.Optional },
                     namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                    name: "Contact",
                    url: "lien-he.html",
                    defaults: new { controller = "ContactDetail", action = "Index", id = UrlParameter.Optional },
                    namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                     name: "Search",
                     url: "tim-kiem.html",
                     defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                     namespaces: new string[] { "Web.Controllers" });

            //Các trang có tham số truyền vào sau
            routes.MapRoute(
                     name: "Page",
                     url: "trang/{alias}.html",
                     defaults: new { controller = "Page", action = "Index", alias = UrlParameter.Optional },
                     namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                        name: "Product",
                        url: "{alias}.pc-{id}.html",
                        defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
                        namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                    name: "ProductDetail",
                    url: "{alias}.p-{productId}.html",
                    defaults: new { controller = "Product", action = "Detail", productId = UrlParameter.Optional },
                    namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                    name: "ProductByTag",
                    url: "tag/{tagId}.html",
                    defaults: new { controller = "Product", action = "ListProductByTag", tagId = UrlParameter.Optional },
                    namespaces: new string[] { "Web.Controllers" });

            //Trang mặc định sau cùng
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}