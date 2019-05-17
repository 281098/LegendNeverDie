﻿using System.Web.Mvc;
using System.Web.Routing;

namespace LND.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            // BotDetect requests must not be routed
            // routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });
            routes.MapRoute(
     name: "Cart",
     url: "gio-hang",
     defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional },
     namespaces: new string[] { "LND.Web.Controllers" }

 );
            routes.MapRoute(
          name: "Search",
          url: "tim-kiem-html",
          defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
          namespaces: new string[] { "LND.Web.Controllers" }

      );
            routes.MapRoute(
        name: "dang nhap",
        url: "dang-nhap-html",
        defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional },
        namespaces: new string[] { "LND.Web.Controllers" }
        );
            routes.MapRoute(
      name: "dang xuat",
      url: "dang-xuat-html",
      defaults: new { controller = "Account", action = "LogOut", id = UrlParameter.Optional },
      namespaces: new string[] { "LND.Web.Controllers" }
      );

            routes.MapRoute(
          name: "Register",
          url: "dang-ky-html",
          defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
          namespaces: new string[] { "LND.Web.Controllers" }
          );

            routes.MapRoute(
          name: "Contact",
          url: "lien-he-html",
          defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
          namespaces: new string[] { "LND.Web.Controllers" }
      );
            routes.MapRoute(
        name: "About",
        url: "about",
        defaults: new { controller = "About", action = "About", id = UrlParameter.Optional },
        namespaces: new string[] { "LND.Web.Controllers" }
    );
            routes.MapRoute(
          name: "Home",
          url: "trang-chu-html",
          defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
          namespaces: new string[] { "LND.Web.Controllers" }
      );
            routes.MapRoute(
             name: "Product",
             url: "{alias}-p-{id}-html",
             defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                 namespaces: new string[] { "LND.Web.Controllers" }
        );
            routes.MapRoute(
            name: "Product Sale",
            url: "hang-giam-gia-html",
            defaults: new { controller = "Product", action = "Sale", id = UrlParameter.Optional },
                namespaces: new string[] { "LND.Web.Controllers" }
       );

            routes.MapRoute(
           name: "Product Category",
           url: "{alias}-pc-{id}-html",
           defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
             namespaces: new string[] { "LND.Web.Controllers" }
       );
            routes.MapRoute(
           name: "Product By Tag",
           url: "tag-{id}-html",
           defaults: new { controller = "Product", action = "ListByTag", id = UrlParameter.Optional },
               namespaces: new string[] { "LND.Web.Controllers" }
      );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}