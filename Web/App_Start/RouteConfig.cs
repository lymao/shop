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
                     name: "Search",
                     url: "tim-kiem.html",
                     defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                     namespaces: new string[] { "Web.Controllers" });

            routes.MapRoute(
                      name: "About",
                      url: "gioi-thieu.html",
                      defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                      namespaces: new string[] { "Web.Controllers" });

            //Các trang có tham số truyền vào sau
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

            //Trang mặc định sau cùng
            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );
        }
    }
}