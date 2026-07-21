using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebSite.AppInPtUddWork.Models;
using WebSite.AppInPtUddWork.ViewModels;
using WebSite.AppInPtUddWork.Services;
using WebSite.AppInPtUddWork.Helpers;

using His;
using HisProxy.HumanResource;

namespace WebSite.AppInPtUddWork.Controllers
{
    public class InPtUddWorkController : Controller
    {
        //public static 

        private User UserInfo
        {
            get
            {
                return BasicService.GetUserInfo(BasicService.CheckAuthentication());
            }
        }

        // GET: InPtUddWork
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult StationList()
        {

#if DEBUG == false
            if (UserInfo.EmpNo == -1) { return RedirectToAction("Login", "System"); }
#endif

            var station = WardService.GetExpandData();
            return View(station);
        }

        [HttpPost]
        public ActionResult StationList(string zone, string wardId, string preExpandHm)
        {
            return RedirectToAction("RoomBedList", "InPtUddWork", new { zone = zone, wardId = wardId, preExpandHm = preExpandHm });
        }

        [HttpGet]
        public ActionResult RoomBedList(string zone, string wardId, string preExpandHm)
        {
            var expandDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd 00:00:00")); // new DateTime(2025, 12, 22); // 
            var roomBed = "";
            var bedList = HisProxyService.GetWardBedList(expandDate, zone, wardId, roomBed);
            if (bedList == null || bedList.Count == 0) { return RedirectToAction("StationList", "InPtUddWork"); }

            BedViewModel bedViewModel = new BedViewModel();
            bedViewModel.expandDate = string.Format("{0} {1}",expandDate.ToString("yyyy/MM/dd"), Convert.ToDateTime(preExpandHm).ToString("HH:mm:ss"));
            bedViewModel.station = wardId; //wardId
            bedViewModel.bedList = bedList;

            return View("RoomBedList", bedViewModel);
        }

        [HttpPost]
        public ActionResult RoomBedList(string ward, string roomBed)
        {
            return RedirectToAction("InPtUddWork", "InPtUddWork", new { ward = ward, roomBed = roomBed });
        }

        [HttpGet]
        public ActionResult InPtUddWork(string ward, string roomBed)
        {
            #if DEBUG == false
                if (UserInfo.EmpNo == -1) { return RedirectToAction("Login", "System"); }
            #endif
            var expandDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd 00:00:00")); // new DateTime(2025, 12, 22); // 

            UddWorkViewModel uddViewModel = UddWorkService.GetInPtUddWorkDataByRoomBed(ward, roomBed, expandDate);
            if (uddViewModel == null){ return RedirectToAction("StationList", "InPtUddWork"); }

            // Get Udd Work Mt Data for Count
            var uddMtData = HisProxyService.GetInPtUddWorkMt(ward, expandDate);
            InpUddWorkMt uddMt = uddMtData.Where(dr => dr.RoomBed.Trim() == roomBed).ToList()[0];
            var index = uddMtData.IndexOf(uddMt);

            //ViewBag.Date = uddViewModel; //Detail
            ViewBag.AllPatientsNum = uddMtData.Count();
            
            //-------------------
            ViewBag.lastPId = "";
            ViewBag.nextPId = "";

            if (index != 0)
            {
                if (index + 1 < uddMtData.Count) { ViewBag.nextPId = uddMtData[index + 1].RoomBed; }
                ViewBag.lastPId = uddMtData[index - 1].RoomBed;
            }
            else
            {
                if (index + 1 < uddMtData.Count) { ViewBag.nextPId = uddMtData[index + 1].RoomBed; }
                //ViewBag.nextPId = uddMtData[index + 1].RoomBed;
            }

            //-----------------------
            return View("InPtUddWork_掃描器", uddViewModel);
        }

        [HttpPost]
        public ActionResult InPtUddWork(string expandDate, string visitNo, string seqNo, string isChecked)
        {
            #if DEBUG == false
                if (UserInfo.EmpNo == -1) { return RedirectToAction("Login", "System"); }
            #endif
            UpdIsCheked upd = new UpdIsCheked
            {
                ExpandDate = expandDate,
                VisitNo = Convert.ToInt32(visitNo),
                SeqNo = Convert.ToInt32(seqNo),
                IsChecked = Convert.ToBoolean(isChecked),
                UpdateUser = UserInfo.EmpNo
            };
            //if (HisProxyService.UpdateDrugIsCheckInInpUddWorkDt(upd))
            //{ int j = 0; }
            //else { int k = 0; }
            //return null;

            bool success = HisProxyService.UpdateDrugIsCheckInInpUddWorkDt(upd);
            return Json(new { success = success });
        }
    }

    
}