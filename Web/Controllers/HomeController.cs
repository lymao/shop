using AutoMapper;
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
        IProductService _productService;
        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService, IProductService productService)
        {
            this._productCategoryService = productCategoryService;
            this._commonService = commonService;
            this._productService = productService;
        }
        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel();

            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            homeViewModel.Slides = slideView;

            var lastestProductModel = _productService.GetLastest(3);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;

            var topSaleProductModel = _productService.GetHotProduct(3);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            return View(homeViewModel);
        }

        [ChildActionOnly]
        public ActionResult HeaderPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 300)]
        public ActionResult SidebarPartial()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategory = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategory);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 300)]
        public ActionResult FooterPartial()
        {
            var footer = _commonService.GetFooter();
            var listFooter = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(listFooter);
        }
    }
}