﻿using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Global;


namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string PageName, string ParentName)
        {
            if (String.IsNullOrEmpty(PageName))
            {
                SqlRepository repository = new SqlRepository();

                Page pageModel = repository.GetPage("Index", null);

                return View("Index", pageModel);
            }
            else
            {
                if (string.Compare(PageName, "Index", true) == 0)
                {
                    SqlRepository repository = new SqlRepository();

                    Page pageModel = repository.GetPage("Index", null);

                    return View("Index", pageModel);
                }
                else if (string.Compare(PageName, "Artist", true) == 0)
                {
                    return View("Artist");
                }
                else
                {
                    SqlRepository repository = new SqlRepository();

                    Page pageModel = repository.GetPage(PageName, ParentName);

                    if (pageModel != null)
                    {                       
                        return View("Page", pageModel);
                    }
                    else
                    {
                        Response.StatusCode = 404;
                        return View ("Error");
                    }
                }
            }
        }


        public ActionResult NavMenuPartial()
        {
            SqlRepository repository = new SqlRepository();
            
            List<NavMenuItem> model = repository.GetNavMenu();            

            return PartialView("_NavMenuPartial", model);  
        }
       
    }
}