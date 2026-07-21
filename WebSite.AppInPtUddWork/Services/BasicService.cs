using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebSite.AppInPtUddWork.Models;

using His;
using HisProxy.HumanResource;

namespace WebSite.AppInPtUddWork.Services
{
    public static class BasicService
    {
        public static FormsAuthenticationTicket CheckAuthentication()
        {
            User user = new User();
            var cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
            {
                return null;
            }

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            return ticket;
        }

        public static User GetUserInfo(FormsAuthenticationTicket ticket)
        {
            User user = new User()
            {
                IdNo = "",
                EmpNo = -1,
                EmpCode = "",
                EmpName = ""
            };

            if (ticket == null) { return user; }

            var ticketName = ticket.Name;
            user.IdNo = ticketName.Split(',')[0];
            user.EmpNo = Convert.ToInt32(ticketName.Split(',')[1]);
            user.EmpName = ticketName.Split(',')[2];

            var dtEmp = ProFileProxy.GetEmpInfoByEmpNo(user.EmpNo).ToList<User>();
            user.EmpCode = dtEmp[0].EmpCode;
            return user;
        }
    }
}