using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.AppInPtUddWork.Models;

namespace WebSite.AppInPtUddWork.ViewModels
{
    public class StationViewModel: Station
    {
        public int TranCode { get; set; } //判斷使否完成交車, 非使用Dt(IsChecked)欄位

        public StationViewModel() { }
        public StationViewModel(string zone, string ward, string preExpandHm, int tranCode)
        {
            this.Zone = zone;
            this.Ward = ward;
            this.PreExpandHm = preExpandHm;
            this.TranCode = tranCode;
        }
    }
}