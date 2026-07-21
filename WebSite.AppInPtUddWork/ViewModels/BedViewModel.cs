using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.AppInPtUddWork.Models;

namespace WebSite.AppInPtUddWork.ViewModels
{
    public class BedViewModel
    {
        public string expandDate { get; set; }
        public string station { get; set; }
        public List<Bed> bedList { get; set; }
    }
}