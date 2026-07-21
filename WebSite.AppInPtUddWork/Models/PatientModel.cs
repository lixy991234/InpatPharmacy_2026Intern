using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    public class LabReport
    {
        public string ALT { get; set; }
        public string AST { get; set; }
        public string CRE { get; set; }
        public string eGFR { get; set; }
        public string Blood { get; set; }
        public string IBW { get; set; }
        public string CrCl { get; set; }
        public string BSA { get; set; }
        public List<MedAllergy> ADR { get; set; }
    }

    public class MedAllergy
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemNameDesc { get; set; }
        public string IIIDesc { get; set; }
        public string OrderUserName { get; set; }
        public string OrderTime { get; set; }
    }
}