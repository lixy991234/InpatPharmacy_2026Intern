using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.AppInPtUddWork.Models;

namespace WebSite.AppInPtUddWork.ViewModels
{
    public class UddWorkViewModel
    {
        public DataHeadler headler { get; set; }
        public LabReport labReport { get; set; }
        public List<Interaction> interList { get; set; }
        public List<PRNOrder> prnOrderList { get; set; }
        public List<DataDetail> dataDetailsList { get; set; }
    }
}