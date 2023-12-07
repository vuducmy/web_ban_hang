using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _19T1021142.DomainModels;
using _19T1021142.BusinessLayers;
using System.Web.Security;

namespace _19T1021142.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// trang đăng nhập vào hệ thống
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string userName = "", string password = "")
        {
            ViewBag.UserName = userName;
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Thông tin không đầy đủ");
                return View();
            }
            var userAccount = UserAccountService.Authorize(AccountTypes.Employee, userName, password);
            if(userAccount == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            //ghi nhận cookie cho phiên đăng nhập
            string cookieString = Newtonsoft.Json.JsonConvert.SerializeObject(userAccount);
            FormsAuthentication.SetAuthCookie(cookieString, false);
            return RedirectToAction("index","home");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ChangePass(string UserName="")
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return View();
            }
            ViewBag.UserName = UserName;
            return View();
        }

       
        [HttpPost]
        public ActionResult ChangePass(string UserName="", string oldPass = "", string newPass= "", string newPass2 = "")
        {
            ViewBag.UserName = UserName;
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(oldPass) || string.IsNullOrEmpty(newPass) || string.IsNullOrEmpty(newPass2))
            {
                ModelState.AddModelError("", "thông tin không đầy đủ");
                return View();
            }
            if(newPass != newPass2)
            {
                ModelState.AddModelError("", "Nhập lại mật khẩu không trùng khớp");
                return View();
            }
            var userAccount = UserAccountService.ChangePassword(AccountTypes.Employee, UserName, oldPass, newPass);
            if (!userAccount)
            {
                ModelState.AddModelError("", "Đổi mật khẩu thất bại");
                return View();
            }
            return RedirectToAction("index", "home");
        }
    }
}