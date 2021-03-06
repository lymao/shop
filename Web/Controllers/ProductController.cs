﻿using AutoMapper;
using Common;
using Model.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Infrastructure.Core;
using Web.Models;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            this._productService = productService;
            this._productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Index(int id, int page = 1,string sort = "")
        {
            //Get ProductCategory
            var category = _productCategoryService.GetById(id);
            ViewBag.category = Mapper.Map<ProductCategory, ProductCategoryViewModel>(category);

            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var productModel = _productService.GetListProductByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)(totalRow / pageSize));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPage,
                TotalCount = totalRow,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Items = productViewModel
            };
            return View(paginationSet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")

        {
            //Get ProductCategory
            ViewBag.keyword = keyword;

            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var productModel = _productService.Search(keyword, page, pageSize, sort, out totalRow);
            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(productModel);
            int totalPage = (int)Math.Ceiling((double)(totalRow / pageSize));

            var paginationSet = new PaginationSet<ProductViewModel>()
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPage,
                TotalCount = totalRow,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Items = productViewModel
            };
            return View(paginationSet);
        }

        public JsonResult GetListProductByName(string keyword)
        {
            var model = _productService.GetListProductByName(keyword);
            return Json(new
            {
                data = model
            },JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detail(int productId)
        {
            var model = _productService.GetById(productId);
            var productViewModel = Mapper.Map<Product, ProductViewModel>(model);

            var relatedProduct = _productService.GetRelatedProduct(productId, 6);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProduct);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(productViewModel.MoreImages);
            ViewBag.MoreImages = listImages;

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_productService.GetListTagByProductId(productId));

            return View(productViewModel);
        }
        public ActionResult ListProductByTag(string tagId,int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            int totalRow = 0;
            var model = _productService.GetListProductByTag(tagId, page, pageSize, out totalRow);
            var viewModel = Mapper.Map<IEnumerable<Product>,IEnumerable<ProductViewModel>>(model);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_productService.GetTags(tagId));

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Page = page,
                PageSize = pageSize,
                TotalPages = totalPage,
                TotalCount = totalRow,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Items = viewModel
            };
            return View(paginationSet);
        }
    }
}