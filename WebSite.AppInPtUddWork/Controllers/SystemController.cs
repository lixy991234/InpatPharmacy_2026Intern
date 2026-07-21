using His;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSite.AppInPtUddWork.Models;
using WebSite.AppInPtUddWork.Services;

namespace WebSite.AppInPtUddWork.Controllers
{
    public class SystemController : Controller
    {
        private User UserInfo => BasicService.GetUserInfo(BasicService.CheckAuthentication());

        // GET: System
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            HttpCookie updateInfo = Request.Cookies.Get("UpdateInfo");
            if (updateInfo != null)
            {
                updateInfo.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(updateInfo);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            if ((account.Length == 0) || (password.Length == 0))
            {
                TempData["message"] = "請輸入帳號密碼";
                return View();
            }

            LoginArgument loginArgm = new LoginArgument()
            {
                idNo = account,
                pwdStr = password,
                appName = "AppPharmacy",
                deviceType = "PDA",
                memberType = "Employee",
                pkExpiredTime = "30",
                tknExpiredTime = "30"
            };

            string webAPIUrl = @"https://mdevws.csh.org.tw/WebAPI/HumanResource/HumanResource/CheckUserLoginInformation";
            string resultStr = string.Empty;
            resultStr = His.RestHttpWebRequest.InvokeHTTP_POST(string.Format(webAPIUrl), Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(loginArgm)));

            var apiResult = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResult>(resultStr);
            if (apiResult.resultCode != 0)
            {
                TempData["message"] = "登入錯誤，請確認帳號密碼";
                return View();
            }

            var cookieValue = string.Format("{0},{1},{2}", account, apiResult.resultMsg[0].EmpNo, apiResult.resultMsg[0].EmpName);
            Response.Cookies.Clear();
            Session.RemoveAll();

            var now = DateTime.Now;
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                //name: "B221840419,1656,周英香",
                name: cookieValue,
                issueDate: now,
                expiration: now.AddMinutes(30),
                isPersistent: false,
                userData: string.Empty,
                cookiePath: FormsAuthentication.FormsCookiePath
                );

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);

            cookie.Secure = false;
            cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.HttpOnly = true;  // 防止 JavaScript 存取 Cookie
            Response.Cookies.Add(cookie);

            return RedirectToAction("Portal", "System");
        }

        [HttpGet]
        public ActionResult Portal()
        {
            return View(UserInfo);
        }

        [HttpPost]
        public ActionResult Portal(int empNo)
        {
            return RedirectToAction("StationList", "InPtUddWork");
        }
    }
}