﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SecondHandMarket.ViewModels;

namespace SecondHandMarket.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Buy()
        {
            return View();
        }
        public ActionResult Release(PageBarModel pageBar)
        {
            using (Db)
            {
                if (pageBar.PageSize == 0)
                {
                    pageBar.PageIndex = 1;
                    pageBar.PageSize = 15;
                }

                var userName = User.Identity.Name;
                var myReleases = Db.Releases.Where(r => r.UserName == userName);

                var pageData = myReleases.OrderByDescending(r => r.Id)
                                         .Skip(pageBar.SkipCount)
                                         .Take(pageBar.PageSize)
                                         .ToList()
                                         .Select(r => new ListItemReleaseModel(r))
                                         .ToList();

                ViewBag.MyReleases = pageData;
                ViewBag.PageBarModel = new PageBarModel()
                    {
                        PageIndex = pageBar.PageIndex,
                        PageSize = pageBar.PageSize,
                        Total = myReleases.Count(),
                        Url = Url.Action("Release")
                    };
                return View();
            }
        }
        public ActionResult Collect()
        {
            return View();
        }
        public ActionResult Evaluate()
        {
            return View();
        }
    }
}
