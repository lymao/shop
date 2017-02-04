﻿using AutoMapper;
using Model.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        ICommonService _commonService;
        public HomeController(IProductCategoryService productCategoryService,ICommonService commonService)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
        }
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult HeaderPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult SidebarPartial()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategory);
        }

        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            var footer = _commonService.GetFooter();
            var listFooter = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(listFooter);
        }
    }
}