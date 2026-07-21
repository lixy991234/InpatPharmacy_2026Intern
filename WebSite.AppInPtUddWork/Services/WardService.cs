using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.AppInPtUddWork.Models;
using WebSite.AppInPtUddWork.ViewModels;

namespace WebSite.AppInPtUddWork.Services
{
    public static class WardService
    {
        public static List<StationViewModel> GetExpandData()
        {
            // 取得資料庫護理站展開時間
            var stationList = HisProxyService.GetWardExpandList();
            List<StationViewModel> stationModelList = new List<StationViewModel>();

            foreach (Station station in stationList)
            {
                // 展開時間尚未到，無資料需查詢
                if (DateTime.Now < Convert.ToDateTime(station.PreExpandHm)) { continue; }

                // Get Udd Work Mt Data for Count
                var expandDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd 00:00:00"));
                var uddMtData = HisProxyService.GetInPtUddWorkMt(station.Ward, expandDate);

                // 護理站有用藥資料才顯示
                if ((uddMtData == null) || (uddMtData.Count == 0)) { continue; }

                StationViewModel stationViewModel = new StationViewModel()
                {
                    Zone = station.Zone,
                    Ward = station.Ward,
                    PreExpandHm = station.PreExpandHm,
                    // Get TranCode From InpUddWorkMt and Check state
                    TranCode = GetTranCodeAndCheck(station.Ward, DateTime.Now)
                };
                stationModelList.Add(stationViewModel);

            }

            return stationModelList;
        }

        /// <summary>
        /// Get TranCode From InpUddWorkMt and Check state
        /// </summary>
        public static int GetTranCodeAndCheck(string ward, DateTime expandDate)
        {
            var workMtList = HisProxyService.GetInPtUddWorkMt(ward, expandDate);
            int finish = 0; //完成筆數

            foreach (InpUddWorkMt mt in workMtList) { if (mt.TranCode == 2) { finish++; } } 
            if (finish == 0) { return 0; }
            else if (finish == workMtList.Count) { return 2; }
            else { return 1; }
        }

    }
}