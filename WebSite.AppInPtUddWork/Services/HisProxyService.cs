using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WebSite.AppInPtUddWork.Models;
using WebSite.AppInPtUddWork.ViewModels;
using His;


namespace WebSite.AppInPtUddWork.Services
{
    public static class HisProxyService
    {
        /// <summary>
        /// Get Ward Expand List
        /// </summary>
        public static List<Station> GetWardExpandList()
        {
            List<Station> ScheduleData = HisProxy.Drug.ExpandDrugProxy.GetPreExpandSchedule().AsEnumerable()
                .OrderBy(dr => dr.Field<System.TimeSpan>("PreExpandHm")).CopyToDataTable().ToList<Station>();
            return ScheduleData;
        }

        /// <summary>
        /// GetWard Bed List Data from DataService for EPaper
        /// </summary>
        public static List<Bed> GetWardBedList(DateTime expandDate, string zone, string ward, string roomBed)
        {
            var _roomBed = String.IsNullOrEmpty(roomBed) ? "" : roomBed;
            var dbResult = HisProxy.Drug.WebAppDrugTranProxy.GetPatientForEPaperForNETRONIX(expandDate, zone, ward, _roomBed);
            var ePaper = dbResult.ToList<UDCardRoomBedEPaper>();
            List<Bed> bedList = new List<Bed>();
            foreach (UDCardRoomBedEPaper udBed in ePaper)
            {
                Bed bed = new Bed();
                bed.RoomBed = udBed.病床號;
                bed.ChartNo = udBed.病歷號;
                bed.PtName = udBed.病人名稱;
                bed.Sex = udBed.性別;
                bed.CtrlDrug = udBed.管制藥品;
                bed.HighAlert = udBed.高警訊藥品;
                bed.ExpandDate = udBed.展開日期;
                bed.IsOut = udBed.是否已出院;
                bed.IsDC = udBed.IsDC;
                bed.IsExpandRepair = udBed.IsExpandRepair;
                bed.IsFreezer = udBed.IsFreezer;
                bed.IsExpensive = udBed.IsExpensive;
                bed.ChangeBed = string.IsNullOrEmpty(udBed.轉床) ? udBed.轉床 : (string.IsNullOrEmpty(udBed.是否已出院) ? udBed.轉床 : "");
                bed.ChangeRoomBed = udBed.轉床床號;
                bed.EndTime = udBed.EndTime;
                bed.NowVisitNo = udBed.NowVisitNo;
                bedList.Add(bed);
            }
            return bedList;
        }

        /// <summary>
        /// Get InPtUddWorkMt Data for Check TranCode
        /// </summary>
        public static List<InpUddWorkMt> GetInPtUddWorkMt(string ward, DateTime expandDate)
        {
            //DataTable dtUddWorkData = HisProxy.Drug.WebAppDrugTranProxy.GetInpUddWork(ward, expandDate);
            List<InpUddWorkMt> mtList = new List<InpUddWorkMt>();
            var dtResult = HisProxy.Drug.WebAppDrugTranProxy.GetInpUddWork(ward, expandDate).AsEnumerable()
                .GroupBy(x => x.Field<string>("RoomBed")).Select(x => x.FirstOrDefault()).OrderBy(dr => dr.Field<string>("RoomBed"));

            if (dtResult.Any())
            {
                mtList = dtResult.CopyToDataTable().ToList<InpUddWorkMt>();
            }
            return mtList;
        }

        /// <summary>
        ///  Get InpUddWork Data From RoomBed
        /// </summary>
        public static List<InPtUddWorkData> GetInpUddWorkDataFromRoomBed(string ward, string roomBed, DateTime expandDate)
        {
            List<InPtUddWorkData> uddWorkList = new List<InPtUddWorkData>();

            var uddWorkData = HisProxy.Drug.WebAppDrugTranProxy.GetInpUddWork(ward, expandDate).AsEnumerable()
                              .Where(dr => dr.Field<string>("RoomBed").Trim() == roomBed).OrderBy(dr => dr.Field<Byte>("SeqNo"));
            if (uddWorkData.Any())
            {
                uddWorkList = uddWorkData.CopyToDataTable().ToList<InPtUddWorkData>();
            }
            return uddWorkList;
        }

        /// <summary>
        /// Get Drug Set Data
        /// </summary>
        /// <returns></returns>
        //public static DrugSet GetDrugSet(string itemCode, string zone)
        //{
        //    var dtResult = HisProxy.Drug.DrugSetProxy.GetDrugSetSingle(itemCode, zone);//.Tables[0].ToList<DrugSet>()[0];
        //    if (dtResult == null || (dtResult.Tables[0].Rows.Count == 0))
        //    {
        //        return new DrugSet();
        //    }

        //    return dtResult.Tables[0].ToList<DrugSet>()[0];
        //}
        private static readonly System.Runtime.Caching.MemoryCache _drugSetCache = System.Runtime.Caching.MemoryCache.Default;

        public static DrugSet GetDrugSet(string itemCode, string zone)
        {
            string cacheKey = $"DrugSet_{itemCode}_{zone}";
            if (_drugSetCache.Contains(cacheKey))
            {
                return (DrugSet)_drugSetCache.Get(cacheKey);
            }

            var dtResult = HisProxy.Drug.DrugSetProxy.GetDrugSetSingle(itemCode, zone);
            DrugSet drug = (dtResult == null || dtResult.Tables[0].Rows.Count == 0)
                           ? new DrugSet()
                           : dtResult.Tables[0].ToList<DrugSet>()[0];

            _drugSetCache.Set(cacheKey, drug, new System.Runtime.Caching.CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddHours(4)
            });
            return drug;
        }
        /// <summary>
        ///  PRN Order
        /// </summary>
        public static List<PRNOrder> GetPRNOrderByVisitNo(int VisitNo)
        {
            var result = HisProxy.Drug.DrugQueryProxy.GetUsingDrugByVisitNo(VisitNo);
            List<PRNOrder> prnOrder = new List<PRNOrder>();
            var order = result.AsEnumerable()
                .Where(dr => dr.Field<System.Int16>("UsageNo") >= 8000 && dr.Field<System.Int16>("UsageNo") <= 8999);
            if (order.Any())
            {
                prnOrder = order.CopyToDataTable().ToList<PRNOrder>();
            }
            return prnOrder;
        }

        /// <summary>
        /// Get LabReport Data
        /// </summary>
        public static LabReport GetInPatientLabReportInfo(int visitNo, int chartNo)
        {
            // Allergy
            var medAllergyList = HisProxy.Drug.DispenseProxy.GetMedAllergy(chartNo).ToList<MedAllergy>();

            // LabReport
            LabReport labReport = new LabReport();
            labReport.ADR = medAllergyList;

            DataTable dtIBW = HisProxy.Drug.DrugCareProxy.GetIBWInfo(chartNo, visitNo);
            if (dtIBW.Rows.Count > 0)
            {
                labReport.IBW = dtIBW.Rows[0]["IBW"].ToString();
                labReport.CrCl = dtIBW.Rows[0]["CRCL"].ToString();
                labReport.BSA = string.Format("H:{0}  W:{1}  BSA:{2}", dtIBW.Rows[0]["PtHeight"].ToString().Trim(), dtIBW.Rows[0]["PtWeight"].ToString().Trim(), dtIBW.Rows[0]["BSA"].ToString().Trim());
            }

            // LabItem 參數 ( 肝ALT,肝AST,腎CRE,腎eGFR,血中濃度 )
            List<List<object>> lsTemp = new List<List<object>>();
            lsTemp.Add(new List<object>() { "肝ALT", 6 });
            lsTemp.Add(new List<object>() { "肝AST", 6 });
            lsTemp.Add(new List<object>() { "腎CRE", 6 });
            lsTemp.Add(new List<object>() { "腎eGFR", 6 });
            lsTemp.Add(new List<object>() { "血濃DIGOT", 6 });
            lsTemp.Add(new List<object>() { "血濃ALEVT", 6 });
            lsTemp.Add(new List<object>() { "血濃COUM", 6 });

            foreach (var temp in lsTemp)
            {
                DataTable dtTemp = HisProxy.Drug.DrugMonitorProxy.GetPtNearlyLabItemByMonth(visitNo, temp[0], temp[1]);
                if (dtTemp.Rows.Count > 0)
                {
                    string sValue = dtTemp.Rows[0]["ReportValue"].ToString().Trim();
                    if (sValue != String.Empty)
                    {
                        string sItemName = dtTemp.Rows[0]["ShortName"].ToString().Trim();
                        string sUnit = dtTemp.Rows[0]["Unit"].ToString().Trim();
                        string sReportTime = Convert.ToDateTime(dtTemp.Rows[0]["ConformTime"]).ToString("yyyy/MM/dd");

                        switch (temp[0])
                        {
                            case "血濃DIGOT":
                            case "血濃ALEVT":
                            case "血濃COUM":
                                labReport.Blood = string.Format("{0} {1} ,{2}", sItemName, sValue, sReportTime);
                                break;
                            case "肝ALT":
                                labReport.ALT = string.Format("{0} ({1}) ,{2}", sValue, sUnit, sReportTime);
                                break;
                            case "肝AST":
                                labReport.AST = string.Format("{0} ({1}) ,{2}", sValue, sUnit, sReportTime);
                                break;
                            case "腎CRE":
                                labReport.CRE = string.Format("{0} ({1}) ,{2}", sValue, sUnit, sReportTime);
                                break;
                            case "腎eGFR":
                                labReport.eGFR = string.Format("{0} ({1}) ,{2}", sValue, sUnit, sReportTime);
                                break;
                            default:
                                break;
                        }
                    }
                    else { }
                }else { }
            }

            return labReport;
        }

        /// <summary>
        /// Get Drug BatchNo By ChartNo
        /// </summary>
        public static List<BatchList> GetBatchNoByChartNo(int ChartNo, string ItemCode, DateTime ExpandDate)
        {
            var dtResult = HisProxy.Drug.WebAppDrugTranProxy.GetBatchNoByChartNo(ChartNo, ItemCode, ExpandDate);
            return dtResult.ToList<BatchList>();
        }

        /// <summary>
        /// Get Drug Interaction Data Using ATC7 Code
        /// </summary>
        public static string GetDrugInter(Object ItemCode1, Object ItemCode2)
        {
            //var drugInter = HisProxy.Drug.DrugInterProxy.GetDrugInter("LEXA", "Mesyrel");
            return null;
        }

        /// <summary>
        /// Update IsCheck Data to InpUddWorkDt
        /// </summary>
        /// <param name="upd"></param>
        /// <returns></returns>
        public static bool UpdateDrugIsCheckInInpUddWorkDt(UpdIsCheked upd)
        {
            DataTable dtUpd = new DataTable("dtUpd");
            dtUpd.Columns.Add("ExpandDate");
            dtUpd.Columns.Add("VisitNo");
            dtUpd.Columns.Add("SeqNo");
            dtUpd.Columns.Add("IsChecked");
            dtUpd.Columns.Add("UpdateUser");

            DataRow dr = dtUpd.NewRow();
            dr["ExpandDate"] = upd.ExpandDate;
            dr["VisitNo"] = upd.VisitNo;
            dr["SeqNo"] = upd.SeqNo;
            dr["IsChecked"] = upd.IsChecked;
            dr["UpdateUser"] = upd.UpdateUser;
            dtUpd.Rows.Add(dr);

            if (HisProxy.Drug.WebAppDrugTranProxy.UpdateInpUddWorkDt(dtUpd) == false)
            {
                return false;
            }
            return true;
        }
    }

}