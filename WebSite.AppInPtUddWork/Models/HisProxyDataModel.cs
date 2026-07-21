using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    /// <summary>
    /// These Classes For Transfor Data to Object From DataBase
    /// </summary>
    public class UDCardRoomBedEPaper
    {
        public string Barcode { get; set; }
        public string 病床號 { get; set; }
        public int 病歷號 { get; set; }
        public string 病人名稱 { get; set; }
        public string 性別 { get; set; }
        public string 管制藥品 { get; set; }
        public string 高警訊藥品 { get; set; }
        public string 展開日期 { get; set; }
        public string 是否已出院 { get; set; }
        public string IsDC { get; set; }
        public string IsExpandRepair { get; set; }
        public string IsFreezer { get; set; }
        public string IsExpensive { get; set; }
        public string 轉床 { get; set; }
        public string 轉床床號 { get; set; }
        public string EndTime { get; set; }
        public string NowVisitNo { get; set; }
    }

    public class InPtUddWorkData
    {
        public string ExpandDate { get; set; }    // 展開日期
        public string Ward { get; set; }          //護理站
        public string RoomBed { get; set; }       //床號
        public string TableHeader11 { get; set; } //表首11(住院患者UD調劑總表)
        public string TableHeader21 { get; set; } //表首21(護理站)
        public string TableHeader22 { get; set; }  //表首22(用藥期間)
        public string TableHeader31 { get; set; } //表首31(患者資訊)
        public string TableHeader41 { get; set; } //表首41(主治醫師+主診斷+過敏記錄)
        public string TableFooter11 { get; set; } //表尾11(肝腎檢查：異常才存)(TDM檢查：有就存) 
        public string TableFooter21 { get; set; } //表尾21(肝腎劑量調整)
        public int TranCode { get; set; } //狀態碼
        public string VisitNo { get; set; }       //就醫號
        public int SeqNo { get; set; }         //序號
        public string ItemCode { get; set; }      //院內碼
        public string ItemName { get; set; }      //商品名
        public string GenericName { get; set; }   //學名
        public double Dose { get; set; }          //劑量
        public string DoseUnit { get; set; }      //劑量單位
        public string Usage { get; set; }         //頻率
        public string Way { get; set; }           //途徑
        public double TotQty { get; set; }         //總量
        public string SaleUnit { get; set; }      //銷售單位
        public string IsMill { get; set; }        //是否磨粉(空白/Y)
        public string IsSelf { get; set; }        //是否自費(空白/Y)
        public string StartTime { get; set; }     //開始時間
        public string EndTime { get; set; }       //停用時間
        public string Remark { get; set; }        //備註(公藥、餘量、管制、化前、化療)
        public bool IsChecked { get; set; }         //是否檢查過
        public int UpdateUser { get; set; }    //最後修改人員
        public string UpdateTime { get; set; }    //最後修改時間
    }

    public class InpUddWorkMt
    {
        public int VisitNo { get; set; }
        public string ExpandDate { get; set; }
        public string Ward { get; set; }
        public string RoomBed { get; set; }
        public int TranCode { get; set; }
        public string TableHeader11 { get; set; }
        public string TableHeader21 { get; set; }
        public string TableHeader22 { get; set; }
        public string TableHeader31 { get; set; }
        public string TableHeader41 { get; set; }
        public string TableFooter11 { get; set; }
        public string TableFooter21 { get; set; }
        public string TableFooter31 { get; set; }
    }

    public class Station
    {
        public string Zone { get; set; }
        public string Ward { get; set; }
        public string PreExpandHm { get; set; }
    }
}