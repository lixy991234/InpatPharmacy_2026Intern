using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    public class DataHeadler
    {
        public string title { get; set; } //表頭
        public string ward { get; set; } //護理站
        public string expandDate { get; set; } //展開日期
        public int visitNo { get; set; } //就醫號
        public string roombed { get; set; } //床號
        public string cycleTime { get; set; } //用藥期間
        public int chartNo { get; set; } //病歷號
        public string ptName { get; set; } //患者姓名
        public string sex { get; set; } //性別
        public string birthday { get; set; } //生日
        public string age { get; set; } //年齡
        public double weight { get; set; } //體重
        public string inTime { get; set; } //入院時間
        public string division { get; set; } //科別
        public string doctor { get; set; } //主治醫師
        public string chtName { get; set; } //主診斷
        public string allergy { get; set; } //過敏記錄
        public string labReport { get; set; } //肝腎檢查
        public string doseModification { get; set; } // 肝腎劑量調整
    }
    public class DataDetail : DrugSet
    {
        public int SeqNo { get; set; } //序號
        public string ItemName { get; set; } //商品名
        public double Dose { get; set; } //劑量
        public string DoseUnit { get; set; } //劑量單位
        public string Usage { get; set; } //頻率
        public string Way { get; set; } //途徑
        public double TotQty { get; set; } //總量
        public DrugBatch BatchList { get; set; } //藥品批號
        public string SaleUnit { get; set; } //銷售單位
        public bool IsDC { get; set; } // 藥品DC
        public int IsExpandRepair { get; set; } //補展開
        public string IsMill { get; set; } //是否磨粉
        public string IsSelf { get; set; }//是否自費
        public string StartTime { get; set; } //開始時間
        public string EndTime { get; set; } //停用時間
        public string Remark { get; set; } //備註
        public string InvBarcode { get; set; } //庫存條碼
        public bool IsChecked { get; set; } //已確認
        //public string SmallPicUrl { get; set; } //縮圖連結
        //public string PicUrl { get; set; } //圖片連結
        //String.Format("http://www.csh.org.tw/into/pharm/Drugimages/{0}.jpg", item.ItemCode)
        public int UpdateUser { get; set; } //修改人員
        public string UpdateTime { get; set; } //修改時間
    }
    public class PRNOrder
    {
        public string ItemName { get; set; } //品項代碼
        public double Dose { get; set; } //預設次劑量
        public string DoseUnit { get; set; } //劑量單位
        public int UsageNo { get; set; } //使用頻率
        public int WayNo { get; set; } //途徑
        public string SaleUnit { get; set; } //銷售單位
        public double Qty { get; set; } //總量
        public double TotQty { get; set; } //總量
        public string QtyUnit { get; set; } //總量單位
        public string IsSelf { get; set; } //自購
        public string IsMill { get; set; } //磨粉
        public string Ward { get; set; } //護理站
        public string ADC { get; set; } //ADC藥品
        public string IsSchedule { get; set; } //預排
        public string IsPreExam { get; set; } //是否事前審查
        public double NhiPrice { get; set; } //劑量單位健保價
        public double GenPrice { get; set; } //劑量單位自費價
        public string AuthSheet { get; set; } //審查表單
        public string CtrlDrug { get; set; } //管制藥
        //1 :該藥品為管制藥第1級
        //2 :該藥品為管制藥第2級
        //3 :該藥品為管制藥第3級
        //4 :該藥品為管制藥第4級
        //A :該藥品為抗生素第1線
        //B :該藥品為抗生素第2線
        //C :該藥品為抗生素第3線
        //N :該藥品非管制藥
        public string PreExecTime { get; set; } //預定執行(排程)時間
        public int PreExecLoc { get; set; } //預定執行地點
        public int MedType { get; set; } //醫囑類別
        public int DrugType { get; set; } //醫囑類別
        public string IsOwn { get; set; } //獨立一張醫囑單
        public int OrderUser { get; set; }//開立人員
        public int UpdateUser { get; set; }//最後修改人員 
        public int VisitNo { get; set; } //就醫號碼
        public int OrderNo { get; set; } //醫囑編號
        public int ItemNo { get; set; } //項目號
        public double SaleRate { get; set; } //劑量與銷售之轉換比率 = 銷售量 / 次劑量
        public string HasSoa { get; set; } //是否有S.O.A
        public string InfoHint { get; set; }
        public string ExecTime { get; set; } //執行(預定停止)時間
        public string TreatTime { get; set; }//處置時間
        public string TreatStatus { get; set; }//處置情形
        public string MedSummary { get; set; }//醫囑摘要
        public string Advise { get; set; }//醫師指示
        public string HasUseRule { get; set; }//使用規範
        public string IsChecked { get; set; }
        public string HiddenAdvise { get; set; }
        public string ExecNo {get; set;}
        public string GenericName { get; set; }//學名及劑量  2016/11/30 新增
        public string OrganDosing { get; set; }//依器官調整劑量,肝異常需調整 
        public string Atc7Code { get; set; }//藥理分類代碼(ATC7)-主
        public string PureItemName { get; set; }//純商品名
        public string INPStorageCode { get; set; } //住院儲位碼
        public string AdultDosing { get; set; } //用法與劑量
        public string PediatricDosings { get; set; } //劑量備註
        public string IndicationsA1 { get; set; } //適應症(院外)
        public string AdverseEffectsA1 { get; set; } //副作用(院外)
        public string AdverseEffectsA2 { get; set; } //副作用(院內)
        public string IsFreezer { get; set; } //是否需要冷藏2021-09-06
        public string IsExpensive { get; set; }//是否高貴藥品2021-09-06
        public string ItemCode { get; set; }//項目代碼
    }
    public class UpdIsCheked
    {
        public string ExpandDate { get; set; }
        public int VisitNo { get; set; }
        public int SeqNo { get; set; }
        public bool IsChecked { get; set; }
        public int UpdateUser { get; set; }
    }
    
}