using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.AppInPtUddWork.Models
{
    /// <summary>
    /// Drug Set
    /// </summary>
    public class DrugSet
    {
        public string drugImgURL { get { return "http://www.csh.org.tw/into/pharm/Drugimages/"; } }
        // String.Format("http://www.csh.org.tw/into/pharm/Drugimages/{0}.jpg", item.ItemCode)
        public string Manufactory { get; set; } //製造商, 廠商名稱
        public string TradeName { get; set; } //商品名, 藥品英文名稱
        public string ChineseName { get; set; } //中文名, 藥品中文名稱
        public string GenericName { get; set; } //學名及劑量, 主成份及含量
        public string LicenseNumber { get; set; }//衛生署許可證號
        public string ItemCode { get; set; } //院內批價碼, 院內代碼
        public int Form { get; set; } //劑型分類
        public string GenericCode { get; set; } //學名碼
        public string IndicationsA1 { get; set; }//適應症(院外), 適應症(藥袋列印)
        public string IndicationsA2 { get; set; } //適應症(院內), 適應症
        public string AdultDosing { get; set; } //用法與劑量, 用法用量
        public string AdverseEffectsA1 { get; set; } //副作用(院外), 副作用(藥袋列印)
        public string AdverseEffectsA2 { get; set; } //副作用(院內), 副作用
        public string Contraindication { get; set; } //禁忌, 禁忌
        public string Precautions { get; set; } //須注意事項, 其他注意事項
        public string Dispose { get; set; } //配製, 配置方法
        public string Storage { get; set; } //儲存, 儲存方法
        public string CtrlDrug { get; set; } //管制藥品
                                             //┌──────────┐
                                             //│N: 非管制藥品       │
                                             //│1: 管制藥第1級      │
                                             //│2: 管制藥第2級      │
                                             //│3: 管制藥第3級      │
                                             //│4: 管制藥第4級      │
                                             //│A: 不需會診         │
                                             //│B: 需會診           │
                                             //│C: 限醫師使用       │ 
                                             //└──────────┘
        public int HighAlert { get; set; } //高警訊藥品, 警訊藥品
                                           //┌───────────┐
                                           //│0: 非高警訊藥品       │
                                           //│1: 高警訊藥品         │
                                           //│2: 化療注射劑         │
                                           //│3: 兒童專用藥         │
                                           //│4: 人體臨床試驗藥品   │
                                           //└───────────┘
        public bool IsFreezer { get; set; } //冷藏藥品
        public bool IsExpensive { get; set; } //高貴藥品
        public string Pregnancy { get; set; } //懷孕分級, 懷孕分級
                                              //┌──────┐
                                              //│N:N/A       │
                                              //│A           │
                                              //│B           │
                                              //│C           │
                                              //│D           │
                                              //│X           │
                                              //└──────┘
        public string NhiNorm { get; set; } //健保規範, 健保規範
        public double ExpireHour { get; set; } //開封後有效時數(小時), 開封後有效時數
        public string ExpireHourDesc { get; set; } //ExpireHour, 開封後有效時數(小時), 開封後有效時數
        public string Injection { get; set; } //輸注速率(針劑專用), 輸注速率
        public string DrugImg { get; set; } //藥品圖示, 藥品圖示
        public string DrugPkgImg { get; set; } //包裝圖示, 包裝圖示

        public string InvBarcode { get; set; } //庫存條碼
    }
    /// <summary>
    /// Drug Interaction
    /// </summary>
    public class Interaction
    {
        public string ItemCode1 { get; set; }  //藥品碼1
        public string ItemName1 { get; set; }  //藥品1
        public string ItemCode2 { get; set; }  //藥品碼2
        public string ItemName2 { get; set; }  //藥品2
        public int Significance { get; set; }  //危害等級
        public string Severity { get; set; }  //嚴重程度
        public string Mechanism { get; set; }  //機轉
        public string Management { get; set; }  //管理
    }
    /// <summary>
    /// Drug Batch Info List
    /// </summary>
    public class DrugBatch
    {
        public string ItemName { get; set; }
        public List<BatchList> detail { get; set; }
    }

    public class BatchList
    {
        public int ChartNo { get; set; }
        public string ItemCode { get; set; }
        public string BatchNo { get; set; }
        public double 用量 { get; set; }
        public string TranDate { get; set; }
    }
}