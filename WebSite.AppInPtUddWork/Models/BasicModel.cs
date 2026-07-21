using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    public class User
    {
        public int EmpNo { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string IdNo { get; set; }
        public string Password { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public User() { }
    }

    public class LoginArgument
    {
        public string idNo { get; set; }
        public string pwdStr { get; set; }
        public string appName { get; set; }
        public string deviceType { get; set; }
        public string memberType { get; set; }
        public string pkExpiredTime { get; set; }
        public string tknExpiredTime { get; set; }
    }

    public class LoginResult
    {
        public int resultCode { get; set; }
        public User[] resultMsg { get; set; }
    }
}