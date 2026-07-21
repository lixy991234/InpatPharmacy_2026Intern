using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.AppInPtUddWork.Models;
using WebSite.AppInPtUddWork.ViewModels;
using WebSite.AppInPtUddWork.Helpers;

namespace WebSite.AppInPtUddWork.Services
{
    public static class UddWorkService
    {
        public static UddWorkViewModel GetInPtUddWorkDataByRoomBed(string ward, string roomBed, DateTime expandDate)
        {
            UddWorkViewModel uddWorkViewModel = new UddWorkViewModel();
            DataHeadler dataHeadler = new DataHeadler();
            LabReport labReport = new LabReport();

            List<Interaction> interList = new List<Interaction>();
            List<PRNOrder> prnOrderList = new List<PRNOrder>();
            List<DataDetail> dataDetailsLIst = new List<DataDetail>();

            int VisitNo = 0;
            int ChartNo = 0;

            var uddWorkDataList = HisProxyService.GetInpUddWorkDataFromRoomBed(ward, roomBed, expandDate);
            if (uddWorkDataList.Count == 0)
            {
                return null;
            }

            VisitNo = Convert.ToInt32(uddWorkDataList[0].VisitNo);

            #region DataHeadler
            // Set Udd Work Header
            dataHeadler.title = uddWorkDataList[0].TableHeader11;
            dataHeadler.expandDate = uddWorkDataList[0].ExpandDate;
            dataHeadler.ward = uddWorkDataList[0].Ward;
            dataHeadler.roombed = uddWorkDataList[0].RoomBed;
            dataHeadler.visitNo = VisitNo;
            dataHeadler.cycleTime = uddWorkDataList[0].TableHeader22;
            dataHeadler.labReport = uddWorkDataList[0].TableFooter11;
            dataHeadler.doseModification = uddWorkDataList[0].TableFooter21;

            //TableHeader31 nvarchar(120)  not null, --表首31(患者資訊)
            var heaser31Split = uddWorkDataList[0].TableHeader31.Split(' ');
            // 患者:2*2**36 史OO 男 20**/**/11(11 歲 10 個月) 體重:38.00 床號：1022 入院時間:2022/08/12 21:50 小兒科
            //chartNo, ptName, sex, birthday, age, weight, inTime, division

            ChartNo = Convert.ToInt32(heaser31Split[0].Split(':')[1]);
            dataHeadler.chartNo = ChartNo;

            int index = 2;
            //try
            //{
            //    dataHeadler.ptName = heaser31Split[1];
            //    while (heaser31Split[index].Trim() != "男" && heaser31Split[index].Trim() != "女")
            //    {
            //        dataHeadler.ptName += " ";
            //        dataHeadler.ptName += heaser31Split[index];
            //        index++;
            //    }

            //    dataHeadler.sex = heaser31Split[index];
            //    dataHeadler.birthday = heaser31Split[index + 1].Split('(')[0];

            //    dataHeadler.age = string.Format("{0}{1}{2}{3}",
            //                                     heaser31Split[index + 1].Split('(')[1],
            //                                     heaser31Split[index + 2],
            //                                     heaser31Split[index + 3],
            //                                     heaser31Split[index + 4].Split(')')[0]);

            //    dataHeadler.weight = Convert.ToDouble(heaser31Split[index + 5].Split(':')[1]);
            //    dataHeadler.inTime = string.Format("{0} {1}", heaser31Split[index + 7].Split(':')[1], heaser31Split[index + 8]);
            //    dataHeadler.division = heaser31Split[index + 9];
            //}
            //catch { dataHeadler.ptName = ", 以下資料取得發生錯誤，請洽資訊室確認"; }
            try
            {
                dataHeadler.ptName = heaser31Split[1];
                while (heaser31Split[index].Trim() != "男" && heaser31Split[index].Trim() != "女")
                {
                    dataHeadler.ptName += " " + heaser31Split[index];
                    index++;
                }
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("解析姓名失敗: " + ex); }

            try
            {
                dataHeadler.sex = heaser31Split[index];
                dataHeadler.birthday = heaser31Split[index + 1].Split('(')[0];
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("解析性別/生日失敗: " + ex); }

            try
            {
                dataHeadler.age = string.Format("{0}{1}{2}{3}",
                    heaser31Split[index + 1].Split('(')[1], heaser31Split[index + 2],
                    heaser31Split[index + 3], heaser31Split[index + 4].Split(')')[0]);
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("解析年齡失敗: " + ex); }

            try { dataHeadler.weight = Convert.ToDouble(heaser31Split[index + 5].Split(':')[1]); }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("解析體重失敗: " + ex); }

            try
            {
                dataHeadler.inTime = string.Format("{0} {1}", heaser31Split[index + 7].Split(':')[1], heaser31Split[index + 8]);
                dataHeadler.division = heaser31Split[index + 9];
            }
            catch (Exception ex) { System.Diagnostics.Debug.WriteLine("解析入院時間/科別失敗: " + ex); }

            //TableHeader41 nvarchar(180)  not null, --表首41(主治醫師 + 主診斷 + 過敏記錄)
            //主治醫師:潘OO　主診斷:腹痛　藥物過敏:Vancomycin 500mg/vial
            //doctor, chtName, allergy
            var header41Split = uddWorkDataList[0].TableHeader41.Split((char)12288);

            dataHeadler.doctor = header41Split[0].Split(':')[1];
            dataHeadler.chtName = header41Split[1].Split(':')[1];
            dataHeadler.allergy = "";
            if (header41Split.Count() >= 3)
            {
                dataHeadler.allergy = header41Split[2].Split(':')[1];
            }
            #endregion

            // Get LabReport Data
            labReport = HisProxyService.GetInPatientLabReportInfo(VisitNo, ChartNo);
            prnOrderList = HisProxyService.GetPRNOrderByVisitNo(VisitNo);

            #region DataDetail
            // Set Udd Work Detail
            foreach (InPtUddWorkData uddWork in uddWorkDataList)
            {
                DrugSet drugData = HisProxyService.GetDrugSet(uddWork.ItemCode, SiteHelper.GetZoneStr(ward));
                var batchList = HisProxyService.GetBatchNoByChartNo(ChartNo, uddWork.ItemCode, expandDate);
                DrugBatch drugBatch = new DrugBatch();
                drugBatch.ItemName = uddWork.ItemName;
                drugBatch.detail = batchList;

                DataDetail uddDetail = new DataDetail()
                {
                    SeqNo = uddWork.SeqNo,
                    ItemCode = uddWork.ItemCode,
                    ItemName = uddWork.ItemName,
                    GenericName = uddWork.GenericName,
                    Dose = uddWork.Dose,
                    DoseUnit = uddWork.DoseUnit,
                    Usage = uddWork.Usage,
                    Way = uddWork.Way,
                    TotQty = uddWork.TotQty,
                    SaleUnit = uddWork.SaleUnit,
                    IsMill = uddWork.IsMill,
                    IsSelf = uddWork.IsSelf,
                    StartTime = uddWork.StartTime,
                    EndTime = uddWork.EndTime,
                    Remark = uddWork.Remark,
                    IsChecked = uddWork.IsChecked,
                    UpdateUser = uddWork.UpdateUser,
                    UpdateTime = uddWork.UpdateTime,
                    AdultDosing = drugData.AdultDosing,
                    Manufactory = drugData.Manufactory,
                    TradeName = drugData.TradeName,
                    ChineseName = drugData.ChineseName,
                    LicenseNumber = drugData.LicenseNumber,
                    Form = drugData.Form,
                    GenericCode = drugData.GenericCode,
                    IndicationsA1 = drugData.IndicationsA1,
                    IndicationsA2 = drugData.IndicationsA2,
                    Contraindication = drugData.Contraindication,
                    AdverseEffectsA1 = drugData.AdverseEffectsA1,
                    AdverseEffectsA2 = drugData.AdverseEffectsA2,
                    Storage = drugData.Storage,
                    Precautions = drugData.Precautions,
                    Dispose = drugData.Dispose,
                    CtrlDrug = drugData.CtrlDrug,
                    HighAlert = drugData.HighAlert,
                    IsFreezer = drugData.IsFreezer,
                    IsExpensive = drugData.IsExpensive,
                    Pregnancy = drugData.Pregnancy,
                    NhiNorm = drugData.NhiNorm,
                    ExpireHour = drugData.ExpireHour,
                    Injection = drugData.Injection,
                    DrugImg = string.Format("{0}{1}.jpg", drugData.drugImgURL, uddWork.ItemCode),
                    DrugPkgImg = string.Format("{0}{1}.jpg", drugData.drugImgURL, uddWork.ItemCode),
                    InvBarcode = drugData.InvBarcode,
                    BatchList = drugBatch
                };

            dataDetailsLIst.Add(uddDetail);
        }
            #endregion


            uddWorkViewModel.headler = dataHeadler;
            uddWorkViewModel.labReport = labReport;
            uddWorkViewModel.interList = interList;
            uddWorkViewModel.prnOrderList = prnOrderList;
            uddWorkViewModel.dataDetailsList = dataDetailsLIst;
            return uddWorkViewModel;
        }
    }
}