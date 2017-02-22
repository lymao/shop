using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Service;
using Web.Infrastructure.Core;
using AutoMapper;
using Model.Models;
using Web.Models;
using Web.Infrastructure.Extensions;
using System.Web.Script.Serialization;

namespace Web.Api
{
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        IPageService _pageService;
        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request,string keyword, int page,int pageSize=6)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _pageService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(query);

                var paginationSet = new PaginationSet<PageViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel pageVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newPage = new Page();
                    newPage.UpdatePage(pageVm);
                    newPage.CreatedDate = DateTime.Now;
                    newPage.CreatedBy = User.Identity.Name;
                    _pageService.Add(newPage);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(newPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel pageVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var dbPage = _pageService.GetById(pageVm.ID);

                    dbPage.UpdatePage(pageVm);
                    dbPage.UpdatedDate = DateTime.Now;
                    dbPage.UpdatedBy = User.Identity.Name;
                    _pageService.Update(dbPage);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(dbPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _pageService.GetById(id);
                var responseData = Mapper.Map<Page, PageViewModel>(model);
                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                var oldPage = _pageService.Delete(id);
                _pageService.Save();
                var responseData = Mapper.Map<Page, PageViewModel>(oldPage);
                response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPages)
        {
            HttpResponseMessage response = null;
            return CreateHttpResponse(request, () =>
            {
                var listPage = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);
                foreach (var item in listPage)
                {
                    _pageService.Delete(item);
                }

                _pageService.Save();
                response = request.CreateResponse(HttpStatusCode.OK, listPage.Count);
                return response;
            });
        }
    }
}
