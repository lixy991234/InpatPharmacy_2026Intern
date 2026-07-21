using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    public class Bed
    {
        public string RoomBed { get; set; } //床號
        public int ChartNo { get; set; } //病歷號
        public string PtName { get; set; } //患者姓名
        public string Sex { get; set; } //性別
        public string CtrlDrug { get; set; } //管制藥
        public string HighAlert { get; set; } //高警訊藥品
        public string ExpandDate { get; set; } //展開日期
        public string IsOut { get; set; } //出院
        public string IsDC { get; set; } //DC
        public string IsExpandRepair { get; set; } //補展開
        public string IsFreezer { get; set; } //冰
        public string IsExpensive { get; set; } //高貴藥
        public string ChangeBed { get; set; } //轉床
        public string ChangeRoomBed { get; set; } //轉床號
        public string EndTime { get; set; } //停用日期
        public string NowVisitNo { get; set; } //床號當下就醫號
        public int TranCode { get; set; } //交易狀態, 自定義欄位，與DB無關聯
        public Bed() { }
        public Bed(string roomBed, int chartNo, string ptName, string sex, string ctrlDrug, string highAlert, string expandDate, string isOut, string isDC, string isExpandRepair, string isFreezer, string isExpensive, string changeBed, string changeRoomBed, string endTime, string nowVisitNo)
        {
            this.RoomBed = roomBed;
            this.ChartNo = chartNo;
            this.PtName = ptName;
            this.Sex = sex;
            this.CtrlDrug = ctrlDrug;
            this.HighAlert = highAlert;
            this.ExpandDate = expandDate;
            this.IsOut = isOut;
            this.IsDC = isDC;
            this.IsExpandRepair = isExpandRepair;
            this.IsFreezer = isFreezer;
            this.IsExpensive = isExpensive;
            this.ChangeBed = changeBed;
            this.ChangeRoomBed = changeRoomBed;
            this.EndTime = endTime;
            this.NowVisitNo = nowVisitNo;
        }
    }
}