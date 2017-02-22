using AutoMapper;
using BotDetect.Web.Mvc;
using Common;
using Model.Models;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class ContactDetailController : Controller
    {
        IContactDetailService _contactDetailService;
        IFeedbackService _feedBackService;
        public ContactDetailController(IContactDetailService contactDetailService,IFeedbackService feedBackService)
        {
            this._contactDetailService = contactDetailService;
            this._feedBackService = feedBackService;
        }
        // GET: ContactDetail
        public ActionResult Index()
        {
            var newFeedbackViewModel = new FeedbackViewModel();
            newFeedbackViewModel.ContactDetail = GetDetail();
            return View(newFeedbackViewModel);
        }

        private ContactDetailViewModel GetDetail()
        {
            var model = _contactDetailService.GetDefaultContact();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(model);
            return contactDetailViewModel;
        }
        [HttpPost]
        [CaptchaValidation("CaptchaCode", "ContactCaptcha", "Mã xác nhận Captcha không đúng!")]
        public ActionResult SendFeedBack(FeedbackViewModel feedbackViewModel)
        {
            if (ModelState.IsValid)
            {
                var newFeedBack = new Feedback();
                newFeedBack.UpdateFeedback(feedbackViewModel);
                _feedBackService.Create(newFeedBack);
                _feedBackService.Save();

                ViewData["SuccessMsg"] = "Gửi phản hồi thành công";

                string content = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/contact_template.html"));
                content = content.Replace("{{Name}}", feedbackViewModel.Name);
                content = content.Replace("{{Email}}", feedbackViewModel.Email);
                content = content.Replace("{{Message}}", feedbackViewModel.Message);
                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                MailHelper.SendMail(adminEmail, "Thông tin liên hệ từ website", content);

                feedbackViewModel.Name = "";
                feedbackViewModel.Message = "";
                feedbackViewModel.Email = "";
            }
            feedbackViewModel.ContactDetail = GetDetail();
            return View("Index", feedbackViewModel);
        }
    }
}